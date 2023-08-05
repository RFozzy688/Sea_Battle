using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    enum WhoShot
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
        int _row;
        int _col;
        int x = 0;
        public WhoShot Shot { get; set; }
        public Point HitLocation { get; set; }

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

            Shot = WhoShot.player;
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
                        _row = i;
                        _col = j;
                        return true;
                    }
                }
            }
            return false;
        }
        public void Fire()
        {
            x++;
            CreatePlayingField field;
            CreateFleetOfShips fleet;

            if (Shot == WhoShot.player)
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

                _drawImage.SetRockerAnimation(point);

                field.ArrayField[_row, _col]._value = -1;
            }
            else if (WhereDidHit(field) > 0) // попал
            {
                Point point = field.ArrayField[_row, _col]._p1;

                _drawImage.WhoShot = Shot;
                _drawImage.SetExplosionAnimation(point);

                field.ArrayField[_row, _col]._value = -1;

                fleet.ArrayShips[field.ArrayField[_row, _col]._index].Health -= 1;
                TestSave();

                if (fleet.ArrayShips[field.ArrayField[_row, _col]._index].Health == 0)
                {
                    fleet.ArrayShips[field.ArrayField[_row, _col]._index].IsDead = true;

                    _drawImage.ShipIsDead(fleet, field, field.ArrayField[_row, _col]._index);
                }
            }
        }
        private int WhereDidHit(CreatePlayingField field)
        {
            return field.ArrayField[_row, _col]._value;
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
