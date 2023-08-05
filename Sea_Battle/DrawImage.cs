using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;

namespace Sea_Battle
{
    struct Picture
    {
        public Image image;
        public Point point;
    }
    enum Shot
    {
        InTarget, 
        OutTarget
    }
    internal class DrawImage : Form
    {
        List<Picture> _drawPicture;
        List<Picture> _tempDrawPictureCross; // временное хранение картинки "крестик" при попадании в вражеский корабыль
        MainForm _parent;
        PictureBox _gifAnimation; // box для анимации промаха и попадания
        Timer _deleteGifAnimation; // удаляем box с анимацией по времени
        Shot _shot;
        Picture _picture;
        public DrawImage(MainForm parent) 
        {
            _parent = parent;
            _parent.Paint += new PaintEventHandler(ImagesOnField_Paint);
            _drawPicture = new List<Picture>();

            _deleteGifAnimation = new Timer();
            _deleteGifAnimation.Enabled = false;
            _deleteGifAnimation.Tick += new EventHandler(TimerDeleteGifAnimation);
        }

        private void TimerDeleteGifAnimation(object? sender, EventArgs e)
        {
            _deleteGifAnimation.Enabled = false;
            _deleteGifAnimation.Stop();

            _gifAnimation.Dispose();

            AddImageToList();
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
                _picture = new Picture();
                _picture.image = GetImageShip(fleet, i);
                _picture.point = GetShipBeginPoint(field, fleet.ArrayShips[i].IndexRow, fleet.ArrayShips[i].IndexCol);
                
                fleet.ArrayShips[i].Hide();

                _drawPicture.Add(_picture);
            }

            _parent.Invalidate();
        }
        private void AddImageToList()
        {
            _drawPicture.Add(_picture);
            _parent.Invalidate();
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
        public void SetMissAnimation(Point point)
        {
            point = GetPoint(point); // отодвинем от края сетки

            Bitmap bitmap = Sea_Battle.Properties.Resources.mimo_no_repit;
            _gifAnimation = new PictureBox();
            _gifAnimation.SizeMode = PictureBoxSizeMode.AutoSize;
            _gifAnimation.Image = bitmap;
            _gifAnimation.BackColor = Color.Transparent;
            _gifAnimation.Location = point;
            _parent.Controls.Add(_gifAnimation);

            _shot = Shot.OutTarget; // мимо цели

            _deleteGifAnimation.Enabled = true;
            _deleteGifAnimation.Interval = 1600;
            _deleteGifAnimation.Start();

            _picture.image = new Bitmap(Properties.Resources.mimo_finish);
            _picture.point = point;
        }
        public void SetHitAnimation(Point point)
        {
            point = GetPoint(point); // отодвинем от края сетки

            Bitmap bitmap = Sea_Battle.Properties.Resources.on_target015_no_repit;
            _gifAnimation = new PictureBox();
            _gifAnimation.SizeMode = PictureBoxSizeMode.AutoSize;
            _gifAnimation.Image = bitmap;
            _gifAnimation.BackColor = Color.Transparent;
            _gifAnimation.Location = new Point(point.X + 20 - bitmap.Width / 2, point.Y + 21 - bitmap.Height / 2);
            _parent.Controls.Add(_gifAnimation);

            _shot = Shot.InTarget; // попал в цель

            _deleteGifAnimation.Enabled = true;
            _deleteGifAnimation.Interval = 1050;
            _deleteGifAnimation.Start();

            _picture.image = new Bitmap(Properties.Resources.red_cross);
            _picture.point = point;
        }
        // координата изображения на поле увеличанная по X и Y
        public Point GetPoint(Point point)
        {
            return new Point(point.X + 4, point.Y + 1);
        }
    }
}
