using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    struct Field
    {
        public Point _p1;
        public Point _p2;
    }
    internal class PlayingField
    {
        Field[,] _field;
        int _sizeField;
        MainForm _parent;
        PictureBox _previewShip; // предпоказ где можна или нельзя поставить корабыль
        public Ship ShipRef { get; set; }

        public int GetSizeField() { return _sizeField; }
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
        public void SnapingToShipGrid(Point point)
        {
            for (int i = 0; i < _sizeField; i++)
            {
                for (int j = 0; j < _sizeField; j++)
                {
                    if (_field[i, j]._p1.X <= point.X && _field[i, j]._p1.Y <= point.Y + 21 &&
                        _field[i, j]._p2.X >= point.X && _field[i, j]._p2.Y >= point.Y + 21)
                    {
                        // привязываем корабыль к сетке
                        ShipRef.Location = _field[i, j]._p1;
                    }
                }
            }
        }
        public void CreateDisplayBoxes()
        {
            _previewShip = new PictureBox();
            _previewShip.Size = new Size(ShipRef.Width, ShipRef.Height);
            _previewShip.BackColor = Color.Transparent;
            _previewShip.BackgroundImage = new Bitmap(Properties.Resources.green_square);
            _previewShip.BackgroundImageLayout = ImageLayout.Tile;
            _previewShip.Location = ShipRef.Location;
            _parent.Controls.Add(_previewShip);
            _previewShip.Hide();
        }
        public void ShowDisplayBoxes(Point point)
        {
            for (int i = 0; i < _sizeField; i++)
            {
                for (int j = 0; j < _sizeField; j++)
                {
                    if (_field[i, j]._p1.X <= point.X && _field[i, j]._p1.Y <= point.Y + 21 &&
                        _field[i, j]._p2.X >= point.X && _field[i, j]._p2.Y >= point.Y + 21)
                    {
                        // привязываем корабыль к сетке
                        _previewShip.Location = _field[i, j]._p1;
                        _previewShip.Show();
                    }
                }
            }
        }
        public void DeleteDisplayBoxes()
        {
            //_previewShip.Dispose();
            _parent.Controls.Remove(_previewShip);
        }
    }
}
