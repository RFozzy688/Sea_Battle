using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;
using System.Reflection;

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
        Timer _deleteGifAnimationMiss; // удаляем box с анимацией промаха
        Timer _deleteGifAnimationExplosion; // удаляем box с анимацией взрыва
        Shot _shot;
        Picture _picture; // картинки промаха и попадания
        Picture _pictureShip; // картинки кораблей
        bool _isDead; // корабыль уничтожен
        public WhoShot WhoShot { get; set; }
        public DrawImage(MainForm parent)
        {
            _parent = parent;
            _parent.Paint += new PaintEventHandler(ImagesOnField_Paint);
            _drawPicture = new List<Picture>();
            _tempDrawPictureCross = new List<Picture>();

            _deleteGifAnimationMiss = new Timer();
            _deleteGifAnimationMiss.Enabled = false;
            _deleteGifAnimationMiss.Tick += new EventHandler(DeleteGifAnimationMiss);

            _deleteGifAnimationExplosion = new Timer();
            _deleteGifAnimationExplosion.Enabled = false;
            _deleteGifAnimationExplosion.Tick += new EventHandler(DeleteGifAnimationExplosion);

            _isDead = false;
        }

        private void DeleteGifAnimationMiss(object? sender, EventArgs e)
        {
            _deleteGifAnimationMiss.Enabled = false;
            _deleteGifAnimationMiss.Stop();

            _gifAnimation.Dispose();

            AddImageToList();
        }
        private void DeleteGifAnimationExplosion(object? sender, EventArgs e)
        {
            _deleteGifAnimationExplosion.Enabled = false;
            _deleteGifAnimationExplosion.Stop();

            _gifAnimation.Dispose();

            AddImageToList();

            if (_isDead)
            {
                InsertImageShipToList();
                _isDead = false;
            }
        }
        private void ImagesOnField_Paint(object? sender, PaintEventArgs e)
        {
            foreach (var item in _drawPicture)
            {
                Graphics g = e.Graphics;
                g.DrawImage(item.image, item.point);
            }

            if (_tempDrawPictureCross.Count > 0)
            {
                foreach (var item in _tempDrawPictureCross)
                {
                    Graphics g = e.Graphics;
                    g.DrawImage(item.image, item.point);
                }
            }
        }
        public void AddPlayerShipsToList(CreateFleetOfShips fleet, CreatePlayingField field)
        {
            for (int i = 0; i < fleet.CountShips; i++)
            {
                _pictureShip = new Picture();
                _pictureShip.image = GetImageShip(fleet, i);

                _pictureShip.point = GetShipBeginPoint(field, fleet.ArrayShips[i].IndexRow, fleet.ArrayShips[i].IndexCol);
                
                fleet.ArrayShips[i].Hide();

                _drawPicture.Add(_pictureShip);
            }

            _parent.Invalidate();
        }
        public void AddImageToList()
        {
            _drawPicture.Add(_picture);
            _parent.Invalidate();
        }
        public void InsertImageShipToList()
        {
            _drawPicture.Insert(0, _pictureShip);
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

            _deleteGifAnimationMiss.Enabled = true;
            _deleteGifAnimationMiss.Interval = 1600;
            _deleteGifAnimationMiss.Start();

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

            _deleteGifAnimationExplosion.Enabled = true;
            _deleteGifAnimationExplosion.Interval = 1050;
            _deleteGifAnimationExplosion.Start();

            _picture.image = new Bitmap(Properties.Resources.red_cross);
            _picture.point = point;
        }
        // координата изображения на поле увеличанная по X и Y
        public Point GetPoint(Point point)
        {
            return new Point(point.X + 4, point.Y + 1);
        }
        public void ShipIsDead(CreateFleetOfShips fleet, CreatePlayingField field, int index)
        {
            _pictureShip = new Picture();
            _pictureShip.image = GetImageShip(fleet, index);

            _pictureShip.point = GetShipBeginPoint(field, 
                fleet.ArrayShips[index].IndexRow, 
                fleet.ArrayShips[index].IndexCol);

            _isDead = true;
        }
    }
}
