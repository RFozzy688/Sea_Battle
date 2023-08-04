using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Sea_Battle
{
    struct Picture
    {
        public Image image;
        public Point point;
    }
    internal class DrawImage : Form
    {
        List<Picture> _drawPicture;
        MainForm _parent;
        public DrawImage(MainForm parent) 
        {
            _parent = parent;
            _parent.Paint += new PaintEventHandler(ImagesOnField_Paint);
            _drawPicture = new List<Picture>();
        }

        private void ImagesOnField_Paint(object? sender, PaintEventArgs e)
        {
            foreach (var item in _drawPicture)
            {
                Graphics g = e.Graphics;
                g.DrawImage(item.image, item.point);
            }
        }

        public void AddPlayerShipsToList(CreateFleetOfShips fleet, CreatePlayingField field)
        {
            for (int i = 0; i < fleet.CountShips; i++)
            {
                Picture picture = new Picture();
                picture.image = GetImageShip(fleet, i);
                picture.point = GetShipBeginPoint(field, fleet.ArrayShips[i].IndexRow, fleet.ArrayShips[i].IndexCol);
                
                fleet.ArrayShips[i].Hide();

                _drawPicture.Add(picture);
            }

            Invalidate();
        }
        public Image GetImageShip(CreateFleetOfShips fleet, int index)
        {
            return fleet.ArrayShips[index].Image;
        }
        public Point GetShipBeginPoint(CreatePlayingField field, int row, int col)
        {
            Point point = field.ArrayField[row, col]._p1;
            point.X += 1;
            point.Y += 1;

            return point;
        }
    }
}
