using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;
using NLog;
using System.DirectoryServices.ActiveDirectory;
using System.Media;

namespace Sea_Battle
{
    enum EnumPlayers
    {
        player,
        enemy
    }
    internal class Battle
    {
        public delegate void EndBattleDelegat();
        public event EndBattleDelegat EndBattleEvent;

        CreateFleetOfShips _playerFleet;
        CreatePlayingField _playerField;
        CreateFleetOfShips _enemyFleet;
        CreatePlayingField _enemyField;
        DrawImage _drawImage;
        MainForm _parent;
        AI _aI;
        Sound _sound;
        Timer _startEnemyShoots;
        int _index;
        int _row;
        int _col;
        public EnumPlayers Winner { get; set; }
        bool _isEndBattle;
        public EnumPlayers Shooter { get; set; }
        public Point HitLocation { get; set; }
        public bool IsCanPressed { get; set; }
        public Logger _logger;

        public Battle(MainForm parent,
            CreateFleetOfShips playerFleet,
            CreatePlayingField playerField,
            CreateFleetOfShips enemyFleet,
            CreatePlayingField enemyField,
            DrawImage drawImage,
            AI aI,
            Sound sound) 
        {
            _playerFleet = playerFleet;
            _playerField = playerField;
            _enemyFleet = enemyFleet;
            _enemyField = enemyField;
            _drawImage = drawImage;
            _parent = parent;
            _aI = aI;
            _sound = sound;

            IsCanPressed = true;

            _startEnemyShoots = new Timer();
            _startEnemyShoots.Interval = 1000;
            _startEnemyShoots.Tick += new EventHandler(EnemyShoots);

            _logger = LogManager.GetCurrentClassLogger();
        }

        public void EnemyShoots(object? sender, EventArgs e)
        {
            _startEnemyShoots.Stop();

            if (_aI.IsWounded) // если ранен, то пытается добить
            {
                _aI.SinkShip(ref _row, ref _col);
            }
            else // если убит, то AI ищет новые индексы для стрельбы
            {
                _aI.EnemyFiringIndexes(ref _row, ref _col);
            }

            Fire();
        }
        public bool IsConvertHitLocationToIndexes()
        {
            for (int i = 0; i < _enemyField.SizeField; i++)
            {
                for (int j = 0; j < _enemyField.SizeField; j++)
                {
                    if (_enemyField.ArrayField[i, j]._p1.X <= HitLocation.X &&
                        _enemyField.ArrayField[i, j]._p1.Y <= HitLocation.Y &&
                        _enemyField.ArrayField[i, j]._p2.X >= HitLocation.X &&
                        _enemyField.ArrayField[i, j]._p2.Y >= HitLocation.Y)
                    {
                        if (_enemyField.ArrayField[i, j]._value != -1)
                        {
                            _row = i;
                            _col = j;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public void Fire()
        {
            //_isEndBattle = true;

            // блокируем кнопку назад пока не будет ход игрока (в ChangeShooter())
            _parent.SetBtnBackState(false); 

            CreatePlayingField field;
            CreateFleetOfShips fleet;

            if (Shooter == EnumPlayers.player)
            {
                field = _enemyField;
                fleet = _enemyFleet;
            }
            else
            {
                field = _playerField;
                fleet = _playerFleet;
            }

            if (WhereDidHit(field) == 0) // промах
            {
                Point point = field.ArrayField[_row, _col]._p1;
                _drawImage.SetRocketAnimation(point);
                field.ArrayField[_row, _col]._value = -1;

                _sound.PlaySound("mimo");
            }
            else if (WhereDidHit(field) > 0) // попал
            {
                Point point = field.ArrayField[_row, _col]._p1;
                _drawImage.SetExplosionAnimation(point);

                field.ArrayField[_row, _col]._value = -1;
                fleet.ArrayShips[field.ArrayField[_row, _col]._index].Health -= 1;
                _index = field.ArrayField[_row, _col]._index;
                //TestSave();

                if (fleet.ArrayShips[_index].Health == 0)
                {
                    fleet.ArrayShips[field.ArrayField[_row, _col]._index].IsDead = true;

                    _drawImage.ShipIsDead(fleet, field, _index);
                    _drawImage.SetImageRocketAroundShip(fleet, field, _index);

                    // если враг потопил корабыль, то сбрасываем переменные в начальные значения
                    if (Shooter == EnumPlayers.enemy)
                    {
                        _aI.IsWounded = false;
                        _aI.ResetDirectionVariables();
                    }

                    _isEndBattle = IsEndBattle();
                }
                else if (Shooter == EnumPlayers.enemy) // если корабыль подбит и при это стпелял враг
                {
                    _aI.NumberOfHits++;

                    if (_aI.NumberOfHits == 1)
                    {
                        // сохраняем координаты первого попадания
                        _aI.RowFirstHit = _row;
                        _aI.ColFirstHit = _col;
                    }

                    _aI.IsWounded = true;
                    _aI.RowHit = _row;
                    _aI.ColHit = _col;
                }

                if (fleet.ArrayShips[field.ArrayField[_row, _col]._index].IsDead)
                {
                    _sound.PlaySound("ubit");
                }
                else
                {
                    _sound.PlaySound("ranen");
                }
            }
        }
        private int WhereDidHit(CreatePlayingField field) // определяем куда попали (в корабыль или пустое место)
        {
            return field.ArrayField[_row, _col]._value;
        }
        public void ChangeShooter() // смена игрока который будет стрелять
        {
            if (Shooter == EnumPlayers.player)
            {
                Shooter = EnumPlayers.enemy;
            }
            else
            {
                Shooter = EnumPlayers.player;
                // так как ход игрока кнопку разблокируем
                _parent.SetBtnBackState(true);
            }
        }
        public void RepeatedShoot() // повторный выстрел врага
        {
            if (_isEndBattle) // условие конца игры
            {
                Winner = Shooter; // определяем победителя
                EndBattle();
                return;
            }

            IsCanPressed = true;

            if (Shooter == EnumPlayers.enemy)
            {
                // враг стреляет с задержкой в 1 секунду
                _startEnemyShoots.Start();
            }
        }
        public void TransitionOfMoveInGame() // переход хода в игре после промаха любого игрока
        {
            if (Shooter == EnumPlayers.enemy && _aI.NumberOfHits >= 2)
            {
                // фиксируем промах после 2-х или более попаданий врага для смены направления стрельбы
                _aI.IsOutTarget = true;
            }

            ChangeShooter(); // смена игрока который будет стрелять

            IsCanPressed = true;
            _drawImage.SetImageWhoShooter(Shooter); // стрелка которая показывает чей ход

            // начало стрельбы врага
            if (Shooter == EnumPlayers.enemy)
            {
                _startEnemyShoots.Start();
            }
        }
        public EnumPlayers WhoFirstShoots() // кто начинает игру
        {
            Random random = new Random();

            if (random.Next(0, 2) == 0)
            {
                return EnumPlayers.player;
            }
            else
            {
                return EnumPlayers.enemy;
            }
        }
        private bool IsEndBattle() // проверка на завершение игры
        {
            CreateFleetOfShips fleet;

            if (Shooter == EnumPlayers.player)
            {
                fleet = _enemyFleet;
            }
            else
            {
                fleet = _playerFleet;
            }

            foreach (var ship in fleet.ArrayShips)
            {
                if (!ship.IsDead) // корабыль не убит
                {
                    return false;
                }
            }

            return true;
        }
        private void EndBattle() // диспетчер оповещающий что игра закончина
        {
            EndBattleEvent(); // вызов события

            if (Winner == EnumPlayers.player)
            {
                _sound.PlaySound("win");
            }
            else
            {
                _sound.PlaySound("lose");

                for (int i = 0; i < _enemyFleet.CountShips; i++)
                {
                    if (!_enemyFleet.ArrayShips[i].IsDead)
                    {
                        _drawImage.ShipIsDead(_enemyFleet, _enemyField, i);
                        _drawImage.InsertImageShipToList();
                    }
                }
            }
        }
        public void TestSave()
        {
            using (FileStream fs = new FileStream("array.txt", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    for (int i = 0; i < _enemyField.SizeField; i++)
                    {
                        for (int j = 0; j < _enemyField.SizeField; j++)
                        {
                            sw.Write(_enemyField.ArrayField[i, j]._value + " ");
                        }
                        sw.Write("\n");
                    }
                    sw.Write("\n");

                    foreach (var item in _enemyFleet.ArrayShips)
                    {
                        sw.Write(item.Health + "\n");
                    }
                }

            }
        }
    }
}
