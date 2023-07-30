﻿using System;
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
    enum ShipDirection
    {
        Horizontal,
        Vertical
    }
    internal class Ship : PictureBox
    {
        Point DownPoint;
        bool IsDragMode;
        MainForm _parent;
        public readonly Point _startPos;
        public readonly ShipType _shipType;
        public ShipDirection _shipDirection; // расположение корабля горизонтальное/вертикальное
        public bool IsOnField { get; set; }

        public PlayingField PlayingFieldRef { get; set; }

        public Ship(MainForm parent, Point startPos, ShipType type, ShipDirection shipDirection)
        {
            this._parent = parent;
            this._shipType = type;
            this._startPos = startPos;
            this.Location = _startPos;
            this._shipDirection = shipDirection;
            this.IsOnField = false;

            // настройка стилей для сглажевания мигания Background и удаления артефактов
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.StandardClick, true);

            this.BackgroundImageLayout = ImageLayout.Center;
            this.SizeMode = PictureBoxSizeMode.AutoSize;
            this.BackColor = Color.Transparent;

        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.BringToFront(); // Помещает элемент управления в начало z-порядка

            DownPoint = e.Location;
            IsDragMode = true;

            PlayingFieldRef.ShipRef = this;
            PlayingFieldRef.CreateDisplayBoxes();

            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            // привязываем корабыль к сетке
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

                PlayingFieldRef.SnapPositionHighlight(Location);
            }
            _parent.Text = Location.X + " " + Location.Y;
            base.OnMouseMove(e);
        }
    }
   
}
