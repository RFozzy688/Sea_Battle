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

            _battleship = new Ship(this, new Point(540, 140), ShipType.Battleship);
            _battleship.Name = "BattleShipBox";
            _battleship.Image = new Bitmap(Properties.Resources.battleship);
            this.Controls.Add(_battleship);
            _battleship.TransparentBackground();

            //_boat = new Ship(this);
            //_boat.Name = "BattleShipBox";
            //_boat.Image = new Bitmap(Properties.Resources.boat);
            //this.Controls.Add(_boat);




            _battleship.PlayingFieldRef = _playerField;

            //_boat.PlayingFieldRef = _playerField;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            Text = e.X + " " + e.Y;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            //_playerField.func(new Point(e.X, e.Y));
        }
    }
}