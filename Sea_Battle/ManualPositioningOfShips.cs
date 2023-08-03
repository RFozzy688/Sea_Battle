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
    internal class ManualPositioningOfShips : AutomaticPositioningOfShips
    {
        MainForm _parent;
        PictureBox _backlightPositionShip; // предпоказ где можна или нельзя поставить корабыль
        PictureBox _backlightPositionWhenRotation; // подсветка позиции при не удачном вращении
        Timer _timer;
        public ManualPositioningOfShips(MainForm parent, 
            CreateFleetOfShips fleet, 
            CreatePlayingField field) : base(fleet, field)
        {
            _parent = parent;

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

            _parent.BtnRotation.Enabled = true;
            _parent.BtnAuto.Enabled = true;
            _parent.BtnNext.Enabled = true;
        }
        // если точка находится на игровом поле, то возращаем индексы этой ячейки
        public bool GetIndices(Point point)
        {
            for (int i = 0; i < _playingFieldRef.SizeField; i++)
            {
                for (int j = 0; j < _playingFieldRef.SizeField; j++)
                {
                    if (_playingFieldRef.ArrayField[i, j]._p1.X <= point.X + 21 && _playingFieldRef.ArrayField[i, j]._p1.Y <= point.Y + 21 &&
                        _playingFieldRef.ArrayField[i, j]._p2.X >= point.X + 21 && _playingFieldRef.ArrayField[i, j]._p2.Y >= point.Y + 21)
                    {
                        _indexRow = i;
                        _indexCol = j;
                        return true;
                    }
                }
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
                    Point p = new Point(_playingFieldRef.ArrayField[_indexRow, _indexCol]._p1.X + 1, _playingFieldRef.ArrayField[_indexRow, _indexCol]._p1.Y + 1);
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
                        _backlightPositionShip.Location = _playingFieldRef.ArrayField[_indexRow, _indexCol]._p1;
                    }
                    else
                    {
                        _backlightPositionShip.BackgroundImage = new Bitmap(Properties.Resources.red_square);
                        // привязываем боксы к сетке
                        _backlightPositionShip.Location = _playingFieldRef.ArrayField[_indexRow, _indexCol]._p1;
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

                    // что бы кнопка была визуально отжата
                    _parent.BtnRotationRelesed(null, null);
                    // блокируем кнопки пока не удалится подсветка
                    _parent.BtnRotation.Enabled = false;
                    _parent.BtnAuto.Enabled = false;
                    _parent.BtnNext.Enabled = false;

                    _timer.Enabled = true;
                    _timer.Start();
                }
            }
        }
        // возращаем корабыль в старую позицию на поле если не удалось установить корабыль согласно правилам игры
        public void ReturnShipToOldPosition(int i, int j)
        {
            Point p = new Point(_playingFieldRef.ArrayField[i, j]._p1.X + 1, _playingFieldRef.ArrayField[i, j]._p1.Y + 1);
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
                            sw.Write(_playingFieldRef.ArrayField[i, j]._ship + " ");
                        }
                        sw.Write("\n");
                    }
                    sw.Write("\n");

                    //for (int i = 0; i < 10; i++)
                    //{
                    //    sw.Write(_ships[i]._shipPositioning.ToString() + "\n");
                    //}
                }

            }
        }
    }
}
