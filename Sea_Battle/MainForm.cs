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

            _battleship = new Ship(this);
            _battleship.Name = "BattleShipBox";
            _battleship.Image = new Bitmap(Properties.Resources.battleship);
            this.Controls.Add(_battleship);

            _playerField = new PlayingField(this);
            _playerField.CreateField(new Point(23, 140), new Point(66, 183));
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            //Text = e.X + " " + e.Y;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            _playerField.func(new Point(e.X, e.Y));
        }
    }
}