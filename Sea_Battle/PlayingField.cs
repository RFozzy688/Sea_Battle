using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Timer = System.Windows.Forms.Timer;

namespace Sea_Battle
{
    struct Field
    {
        public Point _p1;
        public Point _p2;
        public int _ship;
    }
    internal class PlayingField
    {
        Field[,] _field;
        int _sizeField;
        Ship[] _ships;
        MainForm _parent;
        PictureBox _backlightPositionShip; // предпоказ где можна или нельзя поставить корабыль
        PictureBox _backlightPositionWhenRotation; // подсветка позиции при не удачном вращении
        int _indexRow; // индекс строки начала корабля
        int _indexCol; // индекс столбца начала корабля
        Timer _timer;
        public Ship ShipRef { get; set; }
        public int GetSizeField() { return _sizeField; }
        public int GetIndexRow() { return _indexRow; }
        public int GetIndexCol() { return _indexCol; }
        public PlayingField(MainForm parent)
        {
            _sizeField = 10;
            _field = new Field[_sizeField, _sizeField];

            _ships = new Ship[10];

            _parent = parent;

            CreateShips();
            CreateField(new Point(23, 140), new Point(66, 183));

            _timer = new Timer();
            _timer.Enabled = false;
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(RemoveRedHighlight);
        }
        // удаление PictureBox подсветки после вращения
        private void RemoveRedHighlight(object? sender, EventArgs e)
        {
            _parent.Controls.Remove(_backlightPositionWhenRotation);

            _timer.Stop();
            _timer.Enabled = false;
        }
        // создаём флот кораблей
        private void CreateShips()
        {
            int index = 0;

            // 4-х палубный
            _ships[index] = new Ship(new Point(540, 140), ShipType.Battleship, ShipPositioning.Horizontal);
            _ships[index].Name = "BattleShipBox";
            _ships[index].Image = new Bitmap(Properties.Resources.battleship);
            _parent.Controls.Add(_ships[index]);
            _ships[index].PlayingFieldRef = this;
            index++;

            Point tempPoint = new Point(540, 226);

            // 3-х палубные
            for (int i = 0; i < 2; i++)
            {
                _ships[index] = new Ship(tempPoint, ShipType.Cruiser, ShipPositioning.Horizontal);
                _ships[index].Name = "CruiserBox";
                _ships[index].Image = new Bitmap(Properties.Resources.cruiser);
                _parent.Controls.Add(_ships[index]);
                _ships[index].PlayingFieldRef = this;

                tempPoint.X += 43 * 4;
                index++;
            }

            // 2-х палубные
            tempPoint = new Point(540, 312);

            for (int i = 0; i < 3; i++)
            {
                _ships[index] = new Ship(tempPoint, ShipType.Destroyer, ShipPositioning.Horizontal);
                _ships[index].Name = "CruiserBox";
                _ships[index].Image = new Bitmap(Properties.Resources.destroyer);
                _parent.Controls.Add(_ships[index]);
                _ships[index].PlayingFieldRef = this;

                tempPoint.X += 43 * 3;
                index++;
            }

            // 1-о палубные
            tempPoint = new Point(540, 398);

            for (int i = 0; i < 4; i++)
            {
                _ships[index] = new Ship(tempPoint, ShipType.Boat, ShipPositioning.Horizontal);
                _ships[index].Name = "CruiserBox";
                _ships[index].Image = new Bitmap(Properties.Resources.boat);
                _parent.Controls.Add(_ships[index]);
                _ships[index].PlayingFieldRef = this;

                tempPoint.X += 43 * 2;
                index++;
            }
        }
        // разметка поля
        public void CreateField(Point p1, Point p2)
        {
            for (int i = 0; i < _sizeField; i++)
            {
                for (int j = 0; j < _sizeField; j++)
                {
                    _field[i, j]._p1 = p1;
                    _field[i, j]._p2 = p2;
                    p1.X += 43;
                    p2.X += 43;
                }

                p1.X = _field[0, 0]._p1.X;
                p2.X = _field[0, 0]._p2.X;

                p1.Y += 43;
                p2.Y += 43;
            }
        }
        // перемещаем корабль в стартовую позицию если не удалось установить корабыль на поле
        public void SetStartingPosition()
        {
            if (ShipRef._shipPositioning == ShipPositioning.Vertical)
            {
                Bitmap bitmap = (Bitmap)ShipRef.Image;
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
                ShipRef.Image = bitmap;

                ShipRef._shipPositioning = ShipPositioning.Horizontal;
            }

            // если мы вне игрового поля, то ставим корабыль в начальную позицию
            ShipRef.Location = ShipRef._startPos;
            // корабыль не на поле
            ShipRef.IsOnField = false;
            // сбрасываем индексы
            ShipRef.IndexRow = -1;
            ShipRef.IndexCol = -1;
        }
        // если точка находится на игровом поле, то возращаем индексы этой ячейки
        public bool GetIndices(Point point)
        {
            for (int i = 0; i < _sizeField; i++)
            {
                for (int j = 0; j < _sizeField; j++)
                {
                    if (_field[i, j]._p1.X <= point.X + 21 && _field[i, j]._p1.Y <= point.Y + 21 &&
                        _field[i, j]._p2.X >= point.X + 21 && _field[i, j]._p2.Y >= point.Y + 21)
                    {
                        _indexRow = i;
                        _indexCol = j;
                        return true;
                    }
                }
            }

            return false;
        }
        // проверяем не выходит ли корабыль за пределы игрового поля
        private bool IsOnPlayingField()
        {
            if (ShipRef._shipPositioning == ShipPositioning.Horizontal &&
                _indexCol + (int)ShipRef._shipType <= _sizeField)
            {
                return true;
            }
            else if ((ShipRef._shipPositioning == ShipPositioning.Vertical &&
                _indexRow + (int)ShipRef._shipType <= _sizeField))
            { 
                return true; 
            }

            return false;
        }
        // устанавливаем корабыль на поле
        public void SnapingToShipGrid(Point point)
        {
            if (GetIndices(point))
            {
                // проверяем не выходит ли корабыль за пределы игрового поля по горизонтали или по вертикали
                if (IsOnPlayingField()) // если да, то перемещаем в стариовую позицию
                {
                    // привязываем корабыль к сетке
                    Point p = new Point(_field[_indexRow, _indexCol]._p1.X + 1, _field[_indexRow, _indexCol]._p1.Y + 1);
                    ShipRef.Location = p;
                    // корабыль на поле
                    ShipRef.IsOnField = true;
                } 
                else
                {
                    SetStartingPosition();
                }
            }
            else
            {
                SetStartingPosition();
            }
        }
        // создеём PictureBox для подсветки позиции корабля на поле
        public void CreateDisplayBoxes()
        {
            _backlightPositionShip = new PictureBox();
            _backlightPositionShip.Size = new Size(ShipRef.Width, ShipRef.Height);
            _backlightPositionShip.BackColor = Color.Transparent;
            _backlightPositionShip.BackgroundImageLayout = ImageLayout.Tile;
            _backlightPositionShip.Location = ShipRef.Location;
            _parent.Controls.Add(_backlightPositionShip);
        }
        // создеём PictureBox для подсветки позиции корабля после вращения
        private void CreateBacklightWhenRotation()
        {
            _backlightPositionWhenRotation = new PictureBox();
            _backlightPositionWhenRotation.Size = new Size(ShipRef.Width, ShipRef.Height);
            _backlightPositionWhenRotation.BackColor = Color.Transparent;
            _backlightPositionWhenRotation.BackgroundImageLayout = ImageLayout.Tile;
            _backlightPositionWhenRotation.Location = ShipRef.Location;
            _parent.Controls.Add(_backlightPositionWhenRotation);
        }
        // подсвечиваем позицию где будет установлем корабыль
        public void PositionHighlight(Point point)
        {
            if (GetIndices(point))
            {
                _backlightPositionShip.Show();

                if (IsOnPlayingField())
                {
                    if (IsEmptyPositionsAroundShip())
                    {
                        _backlightPositionShip.BackgroundImage = new Bitmap(Properties.Resources.green_square);
                        // привязываем боксы к сетке
                        _backlightPositionShip.Location = _field[_indexRow, _indexCol]._p1;
                    }
                    else
                    {
                        _backlightPositionShip.BackgroundImage = new Bitmap(Properties.Resources.red_square);
                        // привязываем боксы к сетке
                        _backlightPositionShip.Location = _field[_indexRow, _indexCol]._p1;
                    }
                }
                else
                {
                    _backlightPositionShip.Hide();
                }
            }
            else
            {
                _backlightPositionShip.Hide();
            }
        }
        // удаляем PictureBox подсветки после того как корабыль стал на поле
        public void DeleteDisplayBoxes()
        {
            _parent.Controls.Remove(_backlightPositionShip);
        }
        // вращение картинки корабля
        private void RotationBitmap()
        {
            Bitmap bitmap = (Bitmap)ShipRef.Image;
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
            ShipRef.Image = bitmap;
        }
        // вращение корабля на 90/-90 градусов
        public void RotationShip()
        {
            if (ShipRef is not null && ShipRef.IsOnField)
            {
                DeleteShipToArray();

                RotationBitmap();

                ChangeShipPositioning();

                if (IsOnPlayingField() && IsEmptyPositionsAroundShip())
                {
                    SetShipToArray();
                }
                else
                {
                    CreateBacklightWhenRotation();
                    _backlightPositionWhenRotation.BackgroundImage = new Bitmap(Properties.Resources.red_square);
                    _backlightPositionWhenRotation.BringToFront();

                    RotationBitmap();

                    ChangeShipPositioning();

                    SetShipToArray();

                    _timer.Enabled = true;
                    _timer.Start();
                }
            }
        }
        // отмечаем позицию корабля вмассиве _field
        public void SetShipToArray()
        {
            switch (ShipRef._shipPositioning)
            {
                case ShipPositioning.Horizontal:
                    int col = _indexCol;
                    for (int n = 0; n < (int)ShipRef._shipType; n++)
                    {
                        _field[_indexRow, col++]._ship = (int)ShipRef._shipType;
                    }
                    break;
                case ShipPositioning.Vertical:
                    int row = _indexRow;
                    for (int n = 0; n < (int)ShipRef._shipType; n++)
                    {
                        _field[row++, _indexCol]._ship = (int)ShipRef._shipType;
                    }
                    break;
                default:
                    break;
            }
        }
        // удаление корабля из массива
        public void DeleteShipToArray()
        {
            switch (ShipRef._shipPositioning)
            {
                case ShipPositioning.Horizontal:
                    int col = _indexCol;
                    for (int n = 0; n < (int)ShipRef._shipType; n++)
                    {
                        _field[_indexRow, col++]._ship = 0;
                    }
                    break;
                case ShipPositioning.Vertical:
                    int row = _indexRow;
                    for (int n = 0; n < (int)ShipRef._shipType; n++)
                    {
                        _field[row++, _indexCol]._ship = 0;
                    }
                    break;
                default:
                    break;
            }
        }
        // проверяем можно ли разместить корабыль в данной позиции согласно правилам игры
        public bool IsEmptyPositionsAroundShip()
        {
            int temp_j;
            int i, j;
            int n, k;

            switch (ShipRef._shipPositioning)
            {
                case ShipPositioning.Horizontal:
                    i = (_indexRow - 1 < 0) ? 0 : _indexRow - 1;
                    j = (_indexCol - 1 < 0) ? 0 : _indexCol - 1;

                    if (_indexRow == 0) { n = 2; }
                    else if (i + 3 <= _sizeField) { n = i + 3; }
                    else { n = _sizeField; }

                    if (_indexCol == 0) { k = (int)ShipRef._shipType + 1; }
                    else if (j + (int)ShipRef._shipType + 2 <= _sizeField) { k = j + (int)ShipRef._shipType + 2; }
                    else { k = _sizeField; }

                    temp_j = j;

                    for (; i < n; i++)
                    {
                        for (; j < k; j++)
                        {
                            if (_field[i, j]._ship != 0) { return false; }
                        }
                        j = temp_j;
                    }

                    break;

                case ShipPositioning.Vertical:
                    i = (_indexRow - 1 < 0) ? 0 : _indexRow - 1;
                    j = (_indexCol - 1 < 0) ? 0 : _indexCol - 1;

                    if (_indexRow == 0) { n = (int)ShipRef._shipType + 1; }
                    else if (i + (int)ShipRef._shipType + 2 <= _sizeField) { n = i + (int)ShipRef._shipType + 2; }
                    else { n = _sizeField; }

                    if (_indexCol == 0) { k = 2; }
                    else if (j + 3 <= _sizeField) { k = j + 3; }
                    else { k = _sizeField; }

                    temp_j = j;

                    for (; i < n; i++)
                    {
                        for (; j < k; j++)
                        {
                            if (_field[i, j]._ship != 0) { return false; }
                        }
                        j = temp_j;
                    }

                    break;
            }

            return true;
        }
        // возращаем корабыль в старую позицию на поле если не удалось установить корабыль согласно правилам игры
        public void ReturnShipToOldPosition(int i, int j)
        {
            Point p = new Point(_field[i, j]._p1.X + 1, _field[i, j]._p1.Y + 1);
            ShipRef.Location = p;

            _indexRow = i;
            _indexCol = j;
        }
        // смена позиционирования корабля на поле вертикальное/горизонтальное
        private void ChangeShipPositioning()
        {
            if (ShipRef._shipPositioning == ShipPositioning.Horizontal)
            {
                ShipRef._shipPositioning = ShipPositioning.Vertical;
            }
            else
            {
                ShipRef._shipPositioning = ShipPositioning.Horizontal;
            }
        }
        // координата корабля на поле увеличанная на один по X и Y
        public Point GetPoint(int i, int j)
        {
            return new Point(_field[i, j]._p1.X + 1, _field[i, j]._p1.Y + 1);
        }
        // очистка поля
        public void ClearField()
        {
            for (int i = 0; i < 10; i++)
            {
                ShipRef = _ships[i];

                if (ShipRef.IsOnField)
                {
                    _indexRow = _ships[i].IndexRow;
                    _indexCol = _ships[i].IndexCol;

                    DeleteShipToArray();
                    SetStartingPosition();
                }
            }
        }
        // заполняем рандомна массив кораблями
        private bool RandomPositionShip(Ship ship)
        {
            ShipRef = ship;

            ship._shipPositioning = RandomDirection();
            RandomIndices();

            if (IsEmptyPositionsAroundShip() && IsOnPlayingField())
            {
                SetShipToArray();

                ship.IsOnField = true;
                ship.IndexRow = _indexRow;
                ship.IndexCol = _indexCol;

                return true;
            }

            return false;
        }
        // отрисовываем корабли на поле в PictureBox-сах
        public void SetImageShipOnField()
        {
            for (int i = 0; i < 10; i++)
            {
                _ships[i].Location = GetPoint(_ships[i].IndexRow, _ships[i].IndexCol);

                if (_ships[i]._shipPositioning == ShipPositioning.Vertical)
                {
                    ShipRef = _ships[i];
                    RotationBitmap();
                }
            }
        }
        // обход массива кораблей для рандомной растсновки
        public void SetShipOnField()
        {
            for (int i = 0; i < 10; i++)
            {
                if (!RandomPositionShip(_ships[i]))
                {
                    i--;
                }
            }
        }
        public void RandomIndices()
        {
            Random random = new Random();
            _indexRow = random.Next(0, 10);
            _indexCol = random.Next(0, 10);
        }
        public ShipPositioning RandomDirection()
        {
            Random random = new Random();
            int num = random.Next(0, 2);
            ShipPositioning direction = ShipPositioning.Horizontal;

            switch (num)
            {
                case 0:
                    direction = ShipPositioning.Horizontal;
                    break;
                case 1:
                    direction = ShipPositioning.Vertical;
                    break;
            }
            return direction;
        }

        public void TestSave()
        {
            using (FileStream fs = new FileStream("array.txt", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    for (int i = 0; i < _sizeField; i++)
                    {
                        for (int j = 0; j < _sizeField; j++)
                        {
                            sw.Write(_field[i, j]._ship + " ");
                        }
                        sw.Write("\n");
                    }
                    sw.Write("\n");

                    for (int i = 0; i < 10; i++)
                    {
                        sw.Write(_ships[i]._shipPositioning.ToString() + "\n");
                    }
                }

            }
        }
    }
}
