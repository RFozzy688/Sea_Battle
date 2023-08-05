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
        PictureBox _animation; // box для анимации промаха и попадания
        Timer _deleteRocketAnimation; // удаляем box с анимацией промаха
        Timer _deleteExplosionAnimation; // удаляем box с анимацией взрыва
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

            _deleteRocketAnimation = new Timer();
            _deleteRocketAnimation.Enabled = false;
            _deleteRocketAnimation.Tick += new EventHandler(DeleteRocketAnimation);

            _deleteExplosionAnimation = new Timer();
            _deleteExplosionAnimation.Enabled = false;
            _deleteExplosionAnimation.Tick += new EventHandler(DeleteExplosionAnimation);

            _isDead = false;
        }

        private void DeleteRocketAnimation(object? sender, EventArgs e)
        {
            _deleteRocketAnimation.Enabled = false;
            _deleteRocketAnimation.Stop();

            _animation.Dispose();

            AddImageToList();
        }
        private void DeleteExplosionAnimation(object? sender, EventArgs e)
        {
            _deleteExplosionAnimation.Enabled = false;
            _deleteExplosionAnimation.Stop();

            _animation.Dispose();

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
        public void SetRockerAnimation(Point point)
        {
            point = GetPoint(point); // отодвинем от края сетки

            Bitmap bitmap = Sea_Battle.Properties.Resources.mimo_no_repit;
            _animation = new PictureBox();
            _animation.SizeMode = PictureBoxSizeMode.AutoSize;
            _animation.Image = bitmap;
            _animation.BackColor = Color.Transparent;
            _animation.Location = point;
            _parent.Controls.Add(_animation);

            _deleteRocketAnimation.Enabled = true;
            _deleteRocketAnimation.Interval = 1600;
            _deleteRocketAnimation.Start();

            _picture.image = new Bitmap(Properties.Resources.mimo_finish);
            _picture.point = point;
        }
        public void SetExplosionAnimation(Point point)
        {
            point = GetPoint(point); // отодвинем от края сетки

            Bitmap bitmap = Sea_Battle.Properties.Resources.on_target015_no_repit;
            _animation = new PictureBox();
            _animation.SizeMode = PictureBoxSizeMode.AutoSize;
            _animation.Image = bitmap;
            _animation.BackColor = Color.Transparent;
            _animation.Location = new Point(point.X + 20 - bitmap.Width / 2, point.Y + 21 - bitmap.Height / 2);
            _parent.Controls.Add(_animation);

            _deleteExplosionAnimation.Enabled = true;
            _deleteExplosionAnimation.Interval = 1050;
            _deleteExplosionAnimation.Start();

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
        public void SetImageRocketAroundShip()
        {
            //int temp_j;
            //int i, j;
            //int n, k;

            //switch (ShipRef._shipPositioning)
            //{
            //    case ShipPositioning.Horizontal:
            //        i = (_indexRow - 1 < 0) ? 0 : _indexRow - 1;
            //        j = (_indexCol - 1 < 0) ? 0 : _indexCol - 1;

            //        if (_indexRow == 0) { n = 2; }
            //        else if (i + 3 <= _playingFieldRef.SizeField) { n = i + 3; }
            //        else { n = _playingFieldRef.SizeField; }

            //        if (_indexCol == 0) { k = (int)ShipRef._shipType + 1; }
            //        else if (j + (int)ShipRef._shipType + 2 <= _playingFieldRef.SizeField) { k = j + (int)ShipRef._shipType + 2; }
            //        else { k = _playingFieldRef.SizeField; }

            //        temp_j = j;

            //        for (; i < n; i++)
            //        {
            //            for (; j < k; j++)
            //            {
            //                if (_playingFieldRef.ArrayField[i, j]._value != 0) { return false; }
            //            }
            //            j = temp_j;
            //        }

            //        break;

            //    case ShipPositioning.Vertical:
            //        i = (_indexRow - 1 < 0) ? 0 : _indexRow - 1;
            //        j = (_indexCol - 1 < 0) ? 0 : _indexCol - 1;

            //        if (_indexRow == 0) { n = (int)ShipRef._shipType + 1; }
            //        else if (i + (int)ShipRef._shipType + 2 <= _playingFieldRef.SizeField) { n = i + (int)ShipRef._shipType + 2; }
            //        else { n = _playingFieldRef.SizeField; }

            //        if (_indexCol == 0) { k = 2; }
            //        else if (j + 3 <= _playingFieldRef.SizeField) { k = j + 3; }
            //        else { k = _playingFieldRef.SizeField; }

            //        temp_j = j;

            //        for (; i < n; i++)
            //        {
            //            for (; j < k; j++)
            //            {
            //                if (_playingFieldRef.ArrayField[i, j]._value != 0) { return false; }
            //            }
            //            j = temp_j;
            //        }

            //        break;
            //}

            //return true;
        }
    }
}
