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

namespace Sea_Battle
{
    enum WhoShoot
    {
        player,
        enemy
    }
    internal class Battle
    {
        CreateFleetOfShips _playerFleet;
        CreatePlayingField _playerField;
        CreateFleetOfShips _enemyFleet;
        CreatePlayingField _enemyField;
        DrawImage _drawImage;
        MainForm _parent;
        Timer _startEnemyShoots;
        int _index;
        int _row;
        int _col;
        public bool IsBtnInBattlePressed { get; set; }
        public WhoShoot Shooter { get; set; }
        public Point HitLocation { get; set; }
        public bool IsCanPressed { get; set; }
        public Logger _logger;

        public Battle(MainForm parent,
            CreateFleetOfShips playerFleet,
            CreatePlayingField playerField,
            CreateFleetOfShips enemyFleet,
            CreatePlayingField enemyField,
            DrawImage drawImage) 
        {
            _playerFleet = playerFleet;
            _playerField = playerField;
            _enemyFleet = enemyFleet;
            _enemyField = enemyField;
            _drawImage = drawImage;
            _parent = parent;

            Shooter = WhoShoot.player;
            IsCanPressed = true;

            _startEnemyShoots = new Timer();
            _startEnemyShoots.Interval = 1000;
            _startEnemyShoots.Tick += new EventHandler(EnemyShoots);

            IsBtnInBattlePressed = false;

            _logger = LogManager.GetCurrentClassLogger();
        }

        public void EnemyShoots(object? sender, EventArgs e)
        {
            _startEnemyShoots.Stop();

            EnemyFiringIndexes();
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
                        if (_enemyField.ArrayField[i, j]._value >= 0)
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
            CreatePlayingField field;
            CreateFleetOfShips fleet;

            if (Shooter == WhoShoot.player)
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
            }
            else if (WhereDidHit(field) > 0) // попал
            {
                Point point = field.ArrayField[_row, _col]._p1;
                _drawImage.SetExplosionAnimation(point);

                field.ArrayField[_row, _col]._value = -1;
                fleet.ArrayShips[field.ArrayField[_row, _col]._index].Health -= 1;
                _index = field.ArrayField[_row, _col]._index;
                TestSave();

                if (fleet.ArrayShips[_index].Health == 0)
                {
                    fleet.ArrayShips[field.ArrayField[_row, _col]._index].IsDead = true;

                    _drawImage.ShipIsDead(fleet, field, _index);
                    _drawImage.SetImageRocketAroundShip(fleet, field, _index);
                }
            }
        }
        private int WhereDidHit(CreatePlayingField field)
        {
            return field.ArrayField[_row, _col]._value;
        }
        public void EnemyFiringIndexes()
        {
            Random random = new Random();

            while (true)
            {
                _row = random.Next(0, 10);
                _col = random.Next(0, 10);

                if (_playerField.ArrayField[_row, _col]._value >= 0)
                {
                    break;
                }
            }
        }
        public void ChangeShooter()
        {
            if (Shooter == WhoShoot.player)
            {
                Shooter = WhoShoot.enemy;
            }
            else
            {
                Shooter = WhoShoot.player;
            }
        }
        public void RepeatedShoot()
        {
            IsCanPressed = true;

            if (Shooter == WhoShoot.enemy)
            {
                _startEnemyShoots.Start();
            }
        }
        public void StartEnemyShoots()
        {
            ChangeShooter();
            IsCanPressed = true;
            _drawImage.SetImageWhoShooter(Shooter);

            if (Shooter == WhoShoot.enemy)
            {
                _startEnemyShoots.Start();
            }
        }
        public WhoShoot WhoFirstShoots()
        {
            Random random = new Random();

            if (random.Next(0, 2) == 0)
            {
                return WhoShoot.player;
            }
            else
            {
                return WhoShoot.enemy;
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
