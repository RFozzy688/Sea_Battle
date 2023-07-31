using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Field[,] _field;
        int _sizeField;
        MainForm _parent;
        PictureBox _previewShip; // предпоказ где можна или нельзя поставить корабыль
        int _indexRow; // индекс строки начала корабля
        int _indexCol; // индекс столбца начала корабля
        //public int n;
        //public int k;
        //public int i_i;
        //public int j_i;
        public Ship ShipRef { get; set; }

        public int GetSizeField() { return _sizeField; }
        public int GetIndexRow() { return _indexRow; }
        public int GetIndexCol() { return _indexCol; }
        public PlayingField(MainForm parent)
        {
            _sizeField = 10;
            _field = new Field[_sizeField, _sizeField];

            _parent = parent;
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
                    if (_field[i, j]._p1.X <= point.X && _field[i, j]._p1.Y <= point.Y + 21 &&
                        _field[i, j]._p2.X >= point.X && _field[i, j]._p2.Y >= point.Y + 21)
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
            if ((ShipRef._shipDirection == ShipDirection.Horizontal &&
                _indexCol + (int)ShipRef._shipType <= _sizeField) ||
                (ShipRef._shipDirection == ShipDirection.Vertical &&
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
            _previewShip = new PictureBox();
            _previewShip.Size = new Size(ShipRef.Width, ShipRef.Height);
            _previewShip.BackColor = Color.Transparent;
            _previewShip.BackgroundImageLayout = ImageLayout.Tile;
            _previewShip.Location = ShipRef.Location;
            _parent.Controls.Add(_previewShip);
        }
        // подсвечиваем позицию где будет установлем корабыль
        public void SnapPositionHighlight(Point point)
        {
            if (GetIndices(point))
            {
                _previewShip.Show();

                if (IsOnPlayingField())
                {
                    if (IsEmptyPositionsAroundShip())
                    {
                        _previewShip.BackgroundImage = new Bitmap(Properties.Resources.green_square);
                        // привязываем боксы к сетке
                        _previewShip.Location = _field[_indexRow, _indexCol]._p1;
                    }
                    else
                    {
                        _previewShip.BackgroundImage = new Bitmap(Properties.Resources.red_square);
                        // привязываем боксы к сетке
                        _previewShip.Location = _field[_indexRow, _indexCol]._p1;
                    }
                }
                else
                {
                    _previewShip.Hide();
                }
            }
            else
            {
                _previewShip.Hide();
            }
        }
        public void DeleteDisplayBoxes()
        {
            _parent.Controls.Remove(_previewShip);
        }
        public void RotationShip()
        {
            if (ShipRef is not null && ShipRef.IsOnField)
            {
                Bitmap bitmap = (Bitmap)ShipRef.Image;
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
                ShipRef.Image = bitmap;

                DeleteShipToArray();

                if (ShipRef._shipDirection == ShipDirection.Horizontal)
                {
                    ShipRef._shipDirection = ShipDirection.Vertical;
                }
                else
                {
                    ShipRef._shipDirection = ShipDirection.Horizontal;
                }

                SetShipToArray();
            }
        }
        public void SetShipToArray()
        {
            switch (ShipRef._shipDirection)
            {
                case ShipDirection.Horizontal:
                    int col = _indexCol;
                    for (int n = 0; n < (int)ShipRef._shipType; n++)
                    {
                        _field[_indexRow, col++]._ship = (int)ShipRef._shipType;
                    }
                    break;
                case ShipDirection.Vertical:
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
            switch (ShipRef._shipDirection)
            {
                case ShipDirection.Horizontal:
                    int col = _indexCol;
                    for (int n = 0; n < (int)ShipRef._shipType; n++)
                    {
                        _field[_indexRow, col++]._ship = 0;
                    }
                    break;
                case ShipDirection.Vertical:
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

            switch (ShipRef._shipDirection)
            {
                case ShipDirection.Horizontal:
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

                case ShipDirection.Vertical:
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
    }
}
