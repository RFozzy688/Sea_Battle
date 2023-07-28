using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_Battle
{
    internal class Ship : PictureBox
    {
        Point DownPoint;
        bool IsDragMode;
        MainForm _parent;
        Image _screenBackground;
        Image part;
        Graphics graphics;

        public PlayingField PlayingFieldRef { get; set; }

        public Ship(MainForm parent)
        {
            this._parent = parent;
            this.LocationChanged += new EventHandler(LocationEvent);

            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.StandardClick, true);

            this.BackgroundImageLayout = ImageLayout.Center;
            this.SizeMode = PictureBoxSizeMode.AutoSize;

            Rectangle rectangle = new Rectangle(0, 0, parent.Width, parent.Height);
            Bitmap bmp = new Bitmap(parent.Width, parent.Height);
            parent.DrawToBitmap(bmp, rectangle);
            _screenBackground = bmp;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            DownPoint = e.Location;
            IsDragMode = true;
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            PlayingFieldRef.ShipRef = this;
            PlayingFieldRef.SnapingToShipGrid(Location);
            //_parent.Text = Location.X + " " + Location.Y;
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
            }
            base.OnMouseMove(e);
        }
        private void LocationEvent(object? sender, EventArgs e)
        {
            part = new Bitmap(Width, Height);
            graphics = Graphics.FromImage(part);
            graphics.DrawImage(_screenBackground, 0, 0,
                new Rectangle(new Point(Location.X + 8, Location.Y + 31), new Size(Width, Height)), GraphicsUnit.Pixel);
            this.BackgroundImage = part;
        }
    }
   
}
