﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    internal class AutomaticPositioningOfShips
    {
        protected int _indexRow; // индекс строки начала корабля
        protected int _indexCol; // индекс столбца начала корабля
        protected CreateFleetOfShips _fleetRef; // ссылка на флот
        protected CreatePlayingField _playingFieldRef; // ссылка на игровое поле
        public Ship ShipRef { get; set; }
        public AutomaticPositioningOfShips(CreateFleetOfShips fleet, CreatePlayingField playingField)
        {
            _fleetRef = fleet;
            _playingFieldRef = playingField;
        }
        
        //public int GetSizeField() { return _sizeField; }
        public int GetIndexRow() { return _indexRow; }
        public int GetIndexCol() { return _indexCol; }

        // возращает индекс корабля в массиве кораблей
        public int FindIndexShip(int i, int j)
        {
            for (int n = 0; n < 10; n++)
            {
                if (_fleetRef.ArrayShips[n].IndexRow == i && _fleetRef.ArrayShips[n].IndexCol == j)
                {
                    return n;
                }
            }
            return -1;
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
                        _playingFieldRef.ArrayField[_indexRow, col]._value = (int)ShipRef._shipType;
                        _playingFieldRef.ArrayField[_indexRow, col]._index = FindIndexShip(ShipRef.IndexRow, ShipRef.IndexCol);

                        col++;
                    }
                    break;
                case ShipPositioning.Vertical:
                    int row = _indexRow;
                    for (int n = 0; n < (int)ShipRef._shipType; n++)
                    {
                        _playingFieldRef.ArrayField[row, _indexCol]._value = (int)ShipRef._shipType;
                        _playingFieldRef.ArrayField[row, _indexCol]._index = FindIndexShip(ShipRef.IndexRow, ShipRef.IndexCol);

                        row++;
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
                        _playingFieldRef.ArrayField[_indexRow, col++]._value = 0;
                    }
                    break;
                case ShipPositioning.Vertical:
                    int row = _indexRow;
                    for (int n = 0; n < (int)ShipRef._shipType; n++)
                    {
                        _playingFieldRef.ArrayField[row++, _indexCol]._value = 0;
                    }
                    break;
                default:
                    break;
            }
        }
        // проверяем не выходит ли корабыль за пределы игрового поля
        protected bool IsOnPlayingField()
        {
            if (ShipRef._shipPositioning == ShipPositioning.Horizontal &&
                _indexCol + (int)ShipRef._shipType <= _playingFieldRef.SizeField)
            {
                return true;
            }
            else if ((ShipRef._shipPositioning == ShipPositioning.Vertical &&
                _indexRow + (int)ShipRef._shipType <= _playingFieldRef.SizeField))
            {
                return true;
            }

            return false;
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
                    else if (i + 3 <= _playingFieldRef.SizeField) { n = i + 3; }
                    else { n = _playingFieldRef.SizeField; }

                    if (_indexCol == 0) { k = (int)ShipRef._shipType + 1; }
                    else if (j + (int)ShipRef._shipType + 2 <= _playingFieldRef.SizeField) { k = j + (int)ShipRef._shipType + 2; }
                    else { k = _playingFieldRef.SizeField; }

                    temp_j = j;

                    for (; i < n; i++)
                    {
                        for (; j < k; j++)
                        {
                            if (_playingFieldRef.ArrayField[i, j]._value != 0) { return false; }
                        }
                        j = temp_j;
                    }

                    break;

                case ShipPositioning.Vertical:
                    i = (_indexRow - 1 < 0) ? 0 : _indexRow - 1;
                    j = (_indexCol - 1 < 0) ? 0 : _indexCol - 1;

                    if (_indexRow == 0) { n = (int)ShipRef._shipType + 1; }
                    else if (i + (int)ShipRef._shipType + 2 <= _playingFieldRef.SizeField) { n = i + (int)ShipRef._shipType + 2; }
                    else { n = _playingFieldRef.SizeField; }

                    if (_indexCol == 0) { k = 2; }
                    else if (j + 3 <= _playingFieldRef.SizeField) { k = j + 3; }
                    else { k = _playingFieldRef.SizeField; }

                    temp_j = j;

                    for (; i < n; i++)
                    {
                        for (; j < k; j++)
                        {
                            if (_playingFieldRef.ArrayField[i, j]._value != 0) { return false; }
                        }
                        j = temp_j;
                    }

                    break;
            }

            return true;
        }
        // координата корабля на поле увеличанная на один по X и Y
        public Point GetPoint(int i, int j)
        {
            return new Point(_playingFieldRef.ArrayField[i, j]._p1.X + 1, _playingFieldRef.ArrayField[i, j]._p1.Y + 1);
        }
        // очистка поля
        public void ClearField()
        {
            for (int i = 0; i < 10; i++)
            {
                ShipRef = _fleetRef.ArrayShips[i];

                if (ShipRef.IsOnField)
                {
                    _indexRow = _fleetRef.ArrayShips[i].IndexRow;
                    _indexCol = _fleetRef.ArrayShips[i].IndexCol;

                    DeleteShipToArray();
                    SetStartingPosition();
                }
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
        // заполняем рандомна массив кораблями
        private bool RandomPositionShip(Ship ship)
        {
            ShipRef = ship;

            ship._shipPositioning = RandomDirection();
            RandomIndices();

            if (IsEmptyPositionsAroundShip() && IsOnPlayingField())
            {
                ship.IsOnField = true;
                ship.IndexRow = _indexRow;
                ship.IndexCol = _indexCol;

                SetShipToArray();

                return true;
            }

            return false;
        }
        // обход массива кораблей для рандомной растсновки
        public void SetShipOnField()
        {
            for (int i = 0; i < 10; i++)
            {
                if (!RandomPositionShip(_fleetRef.ArrayShips[i]))
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
        // вращение картинки корабля
        protected void RotationBitmap()
        {
            Bitmap bitmap = (Bitmap)ShipRef.Image;
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
            ShipRef.Image = bitmap;
        }
        // отрисовываем корабли на поле в PictureBox-сах
        public void SetImageShipOnField()
        {
            for (int i = 0; i < 10; i++)
            {
                _fleetRef.ArrayShips[i].Location = GetPoint(_fleetRef.ArrayShips[i].IndexRow, _fleetRef.ArrayShips[i].IndexCol);

                if (_fleetRef.ArrayShips[i]._shipPositioning == ShipPositioning.Vertical)
                {
                    ShipRef = _fleetRef.ArrayShips[i];
                    RotationBitmap();
                }
            }
        }


        public void TestSave()
        {
            using (FileStream fs = new FileStream("array.txt", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    for (int i = 0; i < _playingFieldRef.SizeField; i++)
                    {
                        for (int j = 0; j < _playingFieldRef.SizeField; j++)
                        {
                            sw.Write(_playingFieldRef.ArrayField[i, j]._value + " ");
                        }
                        sw.Write("\n");
                    }
                    sw.Write("\n");

                    for (int i = 0; i < 10; i++)
                    {
                        sw.Write(_fleetRef.ArrayShips[i]._shipPositioning.ToString() + "\n");
                    }
                }

            }
        }
    }
}
