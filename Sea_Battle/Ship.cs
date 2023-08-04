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
    enum ShipPositioning
    {
        Horizontal,
        Vertical
    }
    internal class Ship : PictureBox
    {
        Point DownPoint;
        bool IsDragMode;
        public readonly Point _startPos;
        public readonly ShipType _shipType;
        public ShipPositioning _shipPositioning; // расположение корабля горизонтальное/вертикальное
        public int Health { get; set; } // здоровье корабля
        public bool IsDead { get; set; } // подбит корабль или нет
        public int IndexRow { get; set; } // индекс строки в массиве начала корабля
        public int IndexCol { get; set; } // индекс столбца в массиве начала корабля
        public bool IsOnField { get; set; } // находится ли корабыль на поле
        public ManualPositioningOfShips PlayerShipRef { get; set; }
        public Ship(Point startPos, ShipType type, ShipPositioning shipPositioning)
        {
            this._shipType = type;
            this._startPos = startPos;
            this.Location = _startPos;
            this._shipPositioning = shipPositioning;
            this.IsOnField = false;
            this.IndexRow = -1;
            this.IndexCol = -1;
            this.Health = (int)type;
            this.IsDead = false;

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

            PlayerShipRef.ShipRef = this;
            PlayerShipRef.CreateDisplayBoxes();

            if (IsOnField)
            {
                PlayerShipRef.GetIndices(this.Location);
                PlayerShipRef.DeleteShipToArray();
            }

            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            // привязываем корабыль к сетке
            PlayerShipRef.SnapingToShipGrid(Location);
            PlayerShipRef.DeleteDisplayBoxes();

            if (IsOnField)
            {
                if (PlayerShipRef.IsEmptyPositionsAroundShip())
                {
                    IndexRow = PlayerShipRef.GetIndexRow();
                    IndexCol = PlayerShipRef.GetIndexCol();

                    PlayerShipRef.SetShipToArray();
                }
                else
                {
                    if (IndexRow == -1 && IndexCol == -1)
                    {
                        PlayerShipRef.SetStartingPosition();
                    }
                    else
                    {
                        PlayerShipRef.DeleteShipToArray();
                        PlayerShipRef.ReturnShipToOldPosition(IndexRow, IndexCol);
                        PlayerShipRef.SetShipToArray();
                    }
                }
            }

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

                PlayerShipRef.PositionHighlight(Location);
            }
            //_parent.Text = Location.X + " " + Location.Y;
            base.OnMouseMove(e);
        }
    }
   
}
