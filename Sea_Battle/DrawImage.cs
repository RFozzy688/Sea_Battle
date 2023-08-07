using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;
using System.Reflection;
using System.Drawing;

namespace Sea_Battle
{
    struct Picture
    {
        public Image image;
        public Point point;
    }
    public delegate void FinishRocketAnimationDelegat();
    public delegate void FinishExplosionAnimationDelegat();
    internal class DrawImage : Form
    {
        public event FinishRocketAnimationDelegat FinishRocketAnimationEvent;
        public event FinishExplosionAnimationDelegat FinishExplosionAnimationEvent;
        List<Picture> _drawPicture;
        List<Picture> _tempPictureRocket; // временное хранение картинки "ракеты"
        MainForm _parent;
        PictureBox _animation; // box для анимации промаха и попадания
        Timer _deleteRocketAnimation; // удаляем box с анимацией промаха
        Timer _deleteExplosionAnimation; // удаляем box с анимацией взрыва
        Picture _picture; // картинки промаха и попадания
        Picture _pictureShip; // картинки кораблей
        bool _isDead; // корабыль уничтожен
        Point _imagePosition; // позиция отрисовки картинки промаха или попадания
        public WhoShoot WhoShot { get; set; }
        public Battle BattleRef { get; set; }
        public DrawImage(MainForm parent)
        {
            _parent = parent;
            _parent.Paint += new PaintEventHandler(ImagesOnField_Paint);
            _drawPicture = new List<Picture>();
            _tempPictureRocket = new List<Picture>();

            _deleteRocketAnimation = new Timer();
            _deleteRocketAnimation.Tick += new EventHandler(DeleteRocketAnimation);

            _deleteExplosionAnimation = new Timer();
            _deleteExplosionAnimation.Tick += new EventHandler(DeleteExplosionAnimation);

            _isDead = false;
        }

        public void FinishRocketAnimation()
        {
            if (FinishRocketAnimationEvent != null)
            {
                FinishRocketAnimationEvent();
            }
        }
        public void FinishExplosionAnimation()
        {
            if (FinishExplosionAnimationEvent != null)
            {
                FinishExplosionAnimationEvent();
            }
        }
        private void DeleteRocketAnimation(object? sender, EventArgs e)
        {
            _deleteRocketAnimation.Stop();

            _animation.Dispose();

            SetImageRocket(_imagePosition);
            AddImageToList();

            FinishRocketAnimation();
        }
        private void DeleteExplosionAnimation(object? sender, EventArgs e)
        {
            _deleteExplosionAnimation.Stop();

            _animation.Dispose();

            SetImageRedCross(_imagePosition);
            AddImageToList();

            if (_isDead)
            {
                _drawPicture.AddRange(_tempPictureRocket);
                _tempPictureRocket.Clear();

                InsertImageShipToList();

                _isDead = false;
            }

            //BattleRef.IsCanPressed = true;
            FinishExplosionAnimation();
        }
        private void ImagesOnField_Paint(object? sender, PaintEventArgs e)
        {
            foreach (var item in _drawPicture)
            {
                Graphics g = e.Graphics;
                g.DrawImage(item.image, item.point);
            }

            //if (_tempDrawPictureCross.Count > 0)
            //{
            //    foreach (var item in _tempDrawPictureCross)
            //    {
            //        Graphics g = e.Graphics;
            //        g.DrawImage(item.image, item.point);
            //    }
            //}
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
        public void AddImageRocketToTempList()
        {
            _tempPictureRocket.Add(_picture);
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
        public void SetRocketAnimation(Point point)
        {
            point = GetPoint(point); // отодвинем от края сетки
            _imagePosition = point;

            Bitmap bitmap = Sea_Battle.Properties.Resources.mimo_no_repit_1_04;
            _animation = new PictureBox();
            _animation.SizeMode = PictureBoxSizeMode.AutoSize;
            _animation.Image = bitmap;
            _animation.BackColor = Color.Transparent;
            _animation.Location = point;
            _parent.Controls.Add(_animation);

            _deleteRocketAnimation.Interval = 1040;
            _deleteRocketAnimation.Start();
        }
        public void SetExplosionAnimation(Point point)
        {
            point = GetPoint(point); // отодвинем от края сетки
            _imagePosition = point;

            Bitmap bitmap = Sea_Battle.Properties.Resources.on_target015_no_repit;
            _animation = new PictureBox();
            _animation.SizeMode = PictureBoxSizeMode.AutoSize;
            _animation.Image = bitmap;
            _animation.BackColor = Color.Transparent;
            _animation.Location = new Point(point.X + 20 - bitmap.Width / 2, point.Y + 21 - bitmap.Height / 2);
            _parent.Controls.Add(_animation);

            _deleteExplosionAnimation.Interval = 1050;
            _deleteExplosionAnimation.Start();
        }
        private void SetImageRocket(Point point)
        {
            _picture.image = new Bitmap(Properties.Resources.mimo_finish);
            _picture.point = point;
        }
        private void SetImageRedCross(Point point)
        {
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
        public void SetImageRocketAroundShip(CreateFleetOfShips fleet, CreatePlayingField field, int index)
        {
            ShipPositioning shipPositioning = fleet.ArrayShips[index]._shipPositioning;
            ShipType shipType = fleet.ArrayShips[index]._shipType;
            int row = fleet.ArrayShips[index].IndexRow;
            int col = fleet.ArrayShips[index].IndexCol;
            int sizeField = field.SizeField;

            int temp_j;
            int i, j;
            int n, k;

            switch (shipPositioning)
            {
                case ShipPositioning.Horizontal:
                    i = (row - 1 < 0) ? 0 : row - 1;
                    j = (col - 1 < 0) ? 0 : col - 1;

                    if (row == 0) { n = 2; }
                    else if (i + 3 <= sizeField) { n = i + 3; }
                    else { n = sizeField; }

                    if (col == 0) { k = (int)shipType + 1; }
                    else if (j + (int)shipType + 2 <= sizeField) { k = j + (int)shipType + 2; }
                    else { k = sizeField; }

                    temp_j = j;

                    for (; i < n; i++)
                    {
                        for (; j < k; j++)
                        {
                            if (field.ArrayField[i, j]._value == 0) 
                            { 
                                Point point = field.ArrayField[i, j]._p1;
                                point = GetPoint(point);
                                SetImageRocket(point);
                                AddImageRocketToTempList();

                                field.ArrayField[i, j]._value = -1;
                            }
                        }
                        j = temp_j;
                    }

                    break;

                case ShipPositioning.Vertical:
                    i = (row - 1 < 0) ? 0 : row - 1;
                    j = (col - 1 < 0) ? 0 : col - 1;

                    if (row == 0) { n = (int)shipType + 1; }
                    else if (i + (int)shipType + 2 <= sizeField) { n = i + (int)shipType + 2; }
                    else { n = sizeField; }

                    if (col == 0) { k = 2; }
                    else if (j + 3 <= sizeField) { k = j + 3; }
                    else { k = sizeField; }

                    temp_j = j;

                    for (; i < n; i++)
                    {
                        for (; j < k; j++)
                        {
                            if (field.ArrayField[i, j]._value == 0) 
                            {
                                Point point = field.ArrayField[i, j]._p1;
                                point = GetPoint(point);
                                SetImageRocket(point);
                                AddImageRocketToTempList();

                                field.ArrayField[i, j]._value = -1;
                            }
                        }
                        j = temp_j;
                    }

                    break;
            }
        }
    }
}
