using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_Battle
{
    enum ShipType
    {
        Boat = 1,
        Destroyer,
        Cruiser,
        Battleship
    }
    internal class Ship : PictureBox
    {
        Point DownPoint;
        bool IsDragMode;
        MainForm _parent;
        Image _screenBackground;
        Image part;
        Graphics graphics;
        public readonly Point _startPos;
        public readonly ShipType _shipType;

        public PlayingField PlayingFieldRef { get; set; }

        public Ship(MainForm parent, Point startPos, ShipType type)
        {
            this._parent = parent;
            this._shipType = type;
            this._startPos = startPos;
            this.Location = _startPos;
            this.LocationChanged += new EventHandler(LocationEvent);

            // настройка стилей для сглажевания мигания Background и удаления артефактов
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.StandardClick, true);

            this.BackgroundImageLayout = ImageLayout.Center;
            this.SizeMode = PictureBoxSizeMode.AutoSize;
            this.BackColor = Color.Transparent;

            // копируем BackgroundImage формы для отображения псевдо-прозрачности на PictureBox
            //Rectangle rectangle = new Rectangle(0, 0, parent.Width, parent.Height);
            //Bitmap bmp = new Bitmap(parent.Width, parent.Height);
            //parent.DrawToBitmap(bmp, rectangle);
            //_screenBackground = bmp;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            DownPoint = e.Location;
            IsDragMode = true;
            PlayingFieldRef.ShipRef = this;
            PlayingFieldRef.CreateDisplayBoxes();
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            // привязываем корабыль к сетке
            //PlayingFieldRef.ShipRef = this;
            PlayingFieldRef.SnapingToShipGrid(Location);
            PlayingFieldRef.DeleteDisplayBoxes();

            IsDragMode = false;
            base.OnMouseUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsDragMode)
            {
                Point point = e.Location;
                Point deltaPoint = new Point(point.X - DownPoint.X, point.Y - DownPoint.Y);
                Location = new Point(Location.X + deltaPoint.X, Location.Y + deltaPoint.Y);

                PlayingFieldRef.ShowDisplayBoxes(Location);
            }
            //_parent.Text = Location.X + " " + Location.Y;
            base.OnMouseMove(e);
        }
        // имитация прозрачности PictureBox при его перемещении
        public void TransparentBackground()
        {
            //part = new Bitmap(Width, Height);
            //graphics = Graphics.FromImage(part);
            //graphics.DrawImage(_screenBackground, 0, 0,
            //    new Rectangle(new Point(Location.X + 8, Location.Y + 31), new Size(Width, Height)), GraphicsUnit.Pixel);
            //this.BackgroundImage = part;
        }
        private void LocationEvent(object? sender, EventArgs e)
        {
            //TransparentBackground();
        }
    }
   
}
