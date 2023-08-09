using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sea_Battle
{
    enum Direction
    {
        Undefined,
        Right,
        Left,
        Up,
        Down
    }
    internal class AI
    {
        MainForm _parent;
        CreatePlayingField _field;
        public bool IsWounded { get; set; } // ранен
        public int RowHit { get; set; } // индекс строки куда можно стрелять
        public int ColHit { get; set; } // индекс столбца куда можно стрелять
        public int RowFirstHit { get; set; } // индекс строки первого попадания
        public int ColFirstHit { get; set; } // индекс столбца первого попадания
        public bool IsOutTarget { get; set; } // промах
        public Direction DirectionOfFire { get; set; } // направление в котором будет производится выстрел
        public int NumberOfHits { get; set; } // количество попаданий в корабыль

        bool _isCanRight;
        bool _isCanLeft;
        bool _isCanUp;
        bool _isCanDown;

        public Logger _logger;

        public AI(MainForm parent, CreatePlayingField field)
        {
            _parent = parent;
            _field = field;

            IsWounded = false;

            _isCanRight = true;
            _isCanLeft = true;
            _isCanUp = true;
            _isCanDown = true;

            DirectionOfFire = Direction.Undefined;

            _logger = LogManager.GetCurrentClassLogger();
        }
        public void EnemyFiringIndexes(ref int row, ref int col) // рандомный поиск индексов для стрельбы
        {
            Random random = new Random();

            while (true)
            {
                row = random.Next(0, 10);
                col = random.Next(0, 10);

                if (_field.ArrayField[row, col]._value != -1)
                {
                    break;
                }
            }
        }
        public void SinkShip(ref int row, ref int col) // потопить раненый корабль
        {
            // определяем вертикальное или горизонтальное направление стрельбы
            if (NumberOfHits == 2)
            {
                CloseFiringDirections(); // закрываем перпендикулярные направления для стрельбы
            }

            // если после попадания от 2-х раз в корабыль был промах то меняем
            // направление стрельбы на противоположенное
            if (IsOutTarget)
            {
                ChangeDirectionShooting();
            }

            Direction localVarDirection;

            while (true)
            {
                if (DirectionOfFire == Direction.Undefined) 
                {
                    localVarDirection = GetDirectionForFire(); // выбираем направление стрельбы в рандомном порядке
                }
                else
                {
                    localVarDirection = DirectionOfFire; // стреляем в ранее выбранном направлении
                }

                // если условие выполняется, то можно стрелять в этом направлении
                if (localVarDirection == Direction.Right && _isCanRight)
                {
                    // проверяем доступность соседней ячейки для стрельбы
                    if (ColHit + 1 < _field.SizeField && _field.ArrayField[RowHit, ColHit + 1]._value != -1)
                    {
                        // производим выстрел
                        row = RowHit;
                        col = ColHit + 1;
                        // фиксируем направление для последующих выстрелов
                        DirectionOfFire = localVarDirection;
                        return;
                    }

                    // если условие выше не выполняется, то в этом направление стрелять больше не будем
                    _isCanRight = false;
                    DirectionOfFire = Direction.Undefined;

                    // если было от 2-х попаданий и в этом направлении стрелять дальше нельзя
                    // то меняем направление на противоположенное и возращаемся к индексам первого попадания
                    if (NumberOfHits >= 2) { ChangeDirectionShooting(); }
                } // следующая логика стрельбы такая же только меняются направления
                else if (localVarDirection == Direction.Left && _isCanLeft)
                {
                    if (ColHit - 1 >= 0 && _field.ArrayField[RowHit, ColHit - 1]._value != -1)
                    {
                        row = RowHit;
                        col = ColHit - 1;
                        DirectionOfFire = localVarDirection;
                        return;
                    }

                    _isCanLeft = false;
                    DirectionOfFire = Direction.Undefined;

                    if (NumberOfHits >= 2) { ChangeDirectionShooting(); }
                }
                else if (localVarDirection == Direction.Up && _isCanUp)
                {
                    if (RowHit - 1 >= 0 && _field.ArrayField[RowHit - 1, ColHit]._value != -1)
                    {
                        row = RowHit - 1;
                        col = ColHit;
                        DirectionOfFire = localVarDirection;
                        return;
                    }

                    _isCanUp = false;
                    DirectionOfFire = Direction.Undefined;

                    if (NumberOfHits >= 2) { ChangeDirectionShooting(); }
                }
                else if (localVarDirection == Direction.Down && _isCanDown)
                {
                    if (RowHit + 1 < _field.SizeField && _field.ArrayField[RowHit + 1, ColHit]._value != -1)
                    {
                        row = RowHit + 1;
                        col = ColHit;
                        DirectionOfFire = localVarDirection;
                        return;
                    }

                    _isCanDown = false;
                    DirectionOfFire = Direction.Undefined;

                    if (NumberOfHits >= 2) { ChangeDirectionShooting(); }
                }
            }
        }
        private Direction GetDirectionForFire()
        {
            Random random = new Random();
            Direction direction = Direction.Right;

            switch (random.Next(1, 5))
            {
                case 1:
                    direction = Direction.Right;
                    break;
                case 2:
                    direction = Direction.Left;
                    break;
                case 3:
                    direction = Direction.Up;
                    break;
                case 4:
                    direction = Direction.Down;
                    break;
            }
            _logger.Debug("G.D.F.F. direction {0}", direction.ToString());
            return direction;
        }
        public void ResetDirectionVariables() // сбрасываем переменные в начальное состояние
        {
            _isCanDown = true;
            _isCanUp = true;
            _isCanRight = true;
            _isCanLeft = true;

            NumberOfHits = 0;
            IsOutTarget = false;
        }

        // если попали как минимум 2 раза, то расположение корабля становится понятным
        // вертикально/горизонтально заничит перпендикулярные направления для стрельбы можно закрыть
        private void CloseFiringDirections()
        {
            if (DirectionOfFire == Direction.Right || DirectionOfFire == Direction.Left)
            {
                _isCanDown = false;
                _isCanUp = false;
            }
            else if (DirectionOfFire == Direction.Down || DirectionOfFire == Direction.Up)
            {
                _isCanLeft = false;
                _isCanRight = false;
            }
        }
        private void ChangeDirectionShooting() // меняем направления стрельбы на противоположенные после промаха
        {
            if (DirectionOfFire == Direction.Right) { DirectionOfFire = Direction.Left; }
            else if (DirectionOfFire == Direction.Left) { DirectionOfFire = Direction.Right; }
            else if (DirectionOfFire == Direction.Down) { DirectionOfFire = Direction.Up; }
            else if (DirectionOfFire == Direction.Up) { DirectionOfFire = Direction.Down; }

            // востанавливаем координаты первого попадания
            RowHit = RowFirstHit;
            ColHit = ColFirstHit;

            IsOutTarget = false;
        }
    }
}
