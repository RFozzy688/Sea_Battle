using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
        Ship _battleship;
        Ship[] _cruiser;
        Ship[] _destroyer;
        Ship[] _boat;
        MainForm _parent;
        PictureBox _backlightPositionShip; // предпоказ где можна или нельзя поставить корабыль
        PictureBox _backlightPositionWhenRotation;
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

            _parent = parent;

            CreateShips();
            CreateField(new Point(23, 140), new Point(66, 183));

            _timer = new Timer();
            _timer.Enabled = false;
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(RemoveRedHighlight);
        }
        private void RemoveRedHighlight(object? sender, EventArgs e)
        {
            _parent.Controls.Remove(_backlightPositionWhenRotation);

            _timer.Stop();
            _timer.Enabled = false;
        }
        private void CreateShips()
        {
            // 4-х палубный
            _battleship = new Ship(new Point(540, 140), ShipType.Battleship, ShipPositioning.Horizontal);
            _battleship.Name = "BattleShipBox";
            _battleship.Image = new Bitmap(Properties.Resources.battleship);
            _parent.Controls.Add(_battleship);
            _battleship.PlayingFieldRef = this;

            Point tempPoint = new Point(540, 226);

            // 3-х палубные
            _cruiser = new Ship[2];

            for (int i = 0; i < 2; i++)
            {
                _cruiser[i] = new Ship(tempPoint, ShipType.Cruiser, ShipPositioning.Horizontal);
                _cruiser[i].Name = "CruiserBox";
                _cruiser[i].Image = new Bitmap(Properties.Resources.cruiser);
                _parent.Controls.Add(_cruiser[i]);
                _cruiser[i].PlayingFieldRef = this;

                tempPoint.X += 43 * 4;
            }

            // 2-х палубные
            _destroyer = new Ship[3];
            tempPoint = new Point(540, 312);

            for (int i = 0; i < 3; i++)
            {
                _destroyer[i] = new Ship(tempPoint, ShipType.Destroyer, ShipPositioning.Horizontal);
                _destroyer[i].Name = "CruiserBox";
                _destroyer[i].Image = new Bitmap(Properties.Resources.destroyer);
                _parent.Controls.Add(_destroyer[i]);
                _destroyer[i].PlayingFieldRef = this;

                tempPoint.X += 43 * 3;
            }

            // 1-о палубные
            _boat = new Ship[4];
            tempPoint = new Point(540, 398);

            for (int i = 0; i < 4; i++)
            {
                _boat[i] = new Ship(tempPoint, ShipType.Boat, ShipPositioning.Horizontal);
                _boat[i].Name = "CruiserBox";
                _boat[i].Image = new Bitmap(Properties.Resources.boat);
                _parent.Controls.Add(_boat[i]);
                _boat[i].PlayingFieldRef = this;

                tempPoint.X += 43 * 2;
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
        private bool IsOnPlayingField()
        {
            if ((ShipRef._shipPositioning == ShipPositioning.Horizontal &&
                _indexCol + (int)ShipRef._shipType <= _sizeField) ||
                (ShipRef._shipPositioning == ShipPositioning.Vertical &&
                _indexRow + (int)ShipRef._shipType <= _sizeField))
            {
                return true;
            }
            else
            { 
                return false; 
            }
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
        public void CreateDisplayBoxes()
        {
            _backlightPositionShip = new PictureBox();
            _backlightPositionShip.Size = new Size(ShipRef.Width, ShipRef.Height);
            _backlightPositionShip.BackColor = Color.Transparent;
            _backlightPositionShip.BackgroundImageLayout = ImageLayout.Tile;
            _backlightPositionShip.Location = ShipRef.Location;
            _parent.Controls.Add(_backlightPositionShip);
        }
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
        public void DeleteDisplayBoxes()
        {
            _parent.Controls.Remove(_backlightPositionShip);
        }
        public void RotationShip()
        {
            if (ShipRef is not null && ShipRef.IsOnField)
            {
                DeleteShipToArray();

                Bitmap bitmap = (Bitmap)ShipRef.Image;
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
                ShipRef.Image = bitmap;

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

                    bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
                    ShipRef.Image = bitmap;

                    ChangeShipPositioning();

                    SetShipToArray();

                    _timer.Enabled = true;
                    _timer.Start();
                }
            }
        }
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
        public void ReturnShipToOldPosition(int i, int j)
        {
            Point p = new Point(_field[i, j]._p1.X + 1, _field[i, j]._p1.Y + 1);
            ShipRef.Location = p;

            _indexRow = i;
            _indexCol = j;
        }
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
                }

            }
        }
    }
}
