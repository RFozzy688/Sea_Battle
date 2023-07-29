namespace Sea_Battle
{
    public partial class MainForm : Form
    {
        Ship _battleship;
        Ship[] _cruiser;
        Ship[] _destroyer;
        Ship[] _boat;
        PlayingField _playerField;
        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.BackgroundImage = new Bitmap(Properties.Resources.one_field);

            _playerField = new PlayingField(this);
            _playerField.CreateField(new Point(23, 140), new Point(66, 183));

            CreateShips();
        }

        private void CreateShips()
        {
            // 4-х палубный
            _battleship = new Ship(this, new Point(540, 140), ShipType.Battleship, ShipLocation.Horizontal);
            _battleship.Name = "BattleShipBox";
            _battleship.Image = new Bitmap(Properties.Resources.battleship);
            this.Controls.Add(_battleship);
            _battleship.PlayingFieldRef = _playerField;

            Point tempPoint = new Point(540, 226);

            // 3-х палубные
            _cruiser = new Ship[2];

            for (int i = 0; i < 2; i++)
            {
                _cruiser[i] = new Ship(this, tempPoint, ShipType.Cruiser, ShipLocation.Horizontal);
                _cruiser[i].Name = "CruiserBox";
                _cruiser[i].Image = new Bitmap(Properties.Resources.cruiser);
                this.Controls.Add(_cruiser[i]);
                _cruiser[i].PlayingFieldRef = _playerField;

                tempPoint.X += 43 * 4;
            }

            // 2-х палубные
            _destroyer = new Ship[3];
            tempPoint = new Point(540, 312);

            for (int i = 0; i < 3; i++)
            {
                _destroyer[i] = new Ship(this, tempPoint, ShipType.Destroyer, ShipLocation.Horizontal);
                _destroyer[i].Name = "CruiserBox";
                _destroyer[i].Image = new Bitmap(Properties.Resources.destroyer);
                this.Controls.Add(_destroyer[i]);
                _destroyer[i].PlayingFieldRef = _playerField;

                tempPoint.X += 43 * 3;
            }

            // 1-о палубные
            _boat = new Ship[4];
            tempPoint = new Point(540, 398);

            for (int i = 0; i < 4; i++)
            {
                _boat[i] = new Ship(this, tempPoint, ShipType.Boat, ShipLocation.Horizontal);
                _boat[i].Name = "CruiserBox";
                _boat[i].Image = new Bitmap(Properties.Resources.boat);
                this.Controls.Add(_boat[i]);
                _boat[i].PlayingFieldRef = _playerField;

                tempPoint.X += 43 * 2;
            }
        }
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            //Text = e.X + " " + e.Y;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            //_playerField.func(new Point(e.X, e.Y));
        }

        private void BtnRotationPressed(object sender, MouseEventArgs e)
        {
            BtnRotation.Image = new Bitmap(Properties.Resources.btn_rotation_pressed);

            _playerField.RotationShip();
        }

        private void BtnRotationRelesed(object sender, MouseEventArgs e)
        {
            BtnRotation.Image = new Bitmap(Properties.Resources.btn_rotation_relesed);
        }
    }
}