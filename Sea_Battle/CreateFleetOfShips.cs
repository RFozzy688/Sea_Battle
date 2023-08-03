using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    internal class CreateFleetOfShips
    {
        Ship[] _ships;
        int _countShips;
        readonly MainForm _parent;
        public CreateFleetOfShips(MainForm parent) 
        {
            _parent = parent;
            _countShips = 10;
            _ships = new Ship[_countShips];
        }
        // создаём флот кораблей
        public void CreateShips(Point startPoint, int offset, bool show, ManualPositioningOfShips self)
        {
            int index = 0;

            // 4-х палубный
            _ships[index] = new Ship(startPoint, ShipType.Battleship, ShipPositioning.Horizontal);
            _ships[index].Name = "BattleShipBox";
            _ships[index].Image = new Bitmap(Properties.Resources.battleship);
            _parent.Controls.Add(_ships[index]);
            _ships[index].PlayerShipRef = self;
            _ships[index].Visible = show;
            index++;

            startPoint.Y += (offset * 2);
            Point tempPoint = startPoint;

            // 3-х палубные
            for (int i = 0; i < 2; i++)
            {
                _ships[index] = new Ship(tempPoint, ShipType.Cruiser, ShipPositioning.Horizontal);
                _ships[index].Name = "CruiserBox";
                _ships[index].Image = new Bitmap(Properties.Resources.cruiser);
                _parent.Controls.Add(_ships[index]);
                _ships[index].PlayerShipRef = self;
                _ships[index].Visible = show;

                tempPoint.X += offset * 4;
                index++;
            }

            // 2-х палубные
            startPoint.Y += (offset * 2);
            tempPoint = startPoint;

            for (int i = 0; i < 3; i++)
            {
                _ships[index] = new Ship(tempPoint, ShipType.Destroyer, ShipPositioning.Horizontal);
                _ships[index].Name = "DestroyerBox";
                _ships[index].Image = new Bitmap(Properties.Resources.destroyer);
                _parent.Controls.Add(_ships[index]);
                _ships[index].PlayerShipRef = self;
                _ships[index].Visible = show;

                tempPoint.X += offset * 3;
                index++;
            }

            // 1-о палубные
            startPoint.Y += (offset * 2);
            tempPoint = startPoint;

            for (int i = 0; i < 4; i++)
            {
                _ships[index] = new Ship(tempPoint, ShipType.Boat, ShipPositioning.Horizontal);
                _ships[index].Name = "BoatBox";
                _ships[index].Image = new Bitmap(Properties.Resources.boat);
                _parent.Controls.Add(_ships[index]);
                _ships[index].PlayerShipRef = self;
                _ships[index].Visible = show;

                tempPoint.X += offset * 2;
                index++;
            }
        }
        public Ship[] ArrayShips { get { return _ships; } }
    }
}
