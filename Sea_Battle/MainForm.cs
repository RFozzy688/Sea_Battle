using System.Text;

namespace Sea_Battle
{
    public partial class MainForm : Form
    {
        CreateFleetOfShips _playerFleet;
        CreatePlayingField _playerField;
        ManualPositioningOfShips _playerShipsPosition;

        CreateFleetOfShips _enemyFleet;
        CreatePlayingField _enemyField;
        AutomaticPositioningOfShips _enemyShipsPosition;

        EmbededFont _embededFont;
        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.BackgroundImage = new Bitmap(Properties.Resources.one_field);

            _playerFleet = new CreateFleetOfShips(this);
            _playerField = new CreatePlayingField();
            _playerShipsPosition = new ManualPositioningOfShips(this, _playerFleet, _playerField);

            _playerField.CreateField(new Point(23, 140), new Point(66, 183));
            _playerFleet.CreateShips(new Point(540, 140), 43, true, _playerShipsPosition);

            _enemyFleet = new CreateFleetOfShips(this);
            _enemyField = new CreatePlayingField();
            _enemyShipsPosition = new AutomaticPositioningOfShips(_enemyFleet, _enemyField);

            _enemyField.CreateField(new Point(540, 140), new Point(583, 183));
            _enemyFleet.CreateShips(new Point(200, 0), 0, false, null);

            _embededFont = new EmbededFont();

            BtnAuto.Font = _embededFont.GetBtnFontReleased();
            BtnNext.Font = _embededFont.GetBtnFontReleased();
            BtnAuto.ForeColor = Color.FromArgb(38, 42, 182);
            BtnNext.ForeColor = Color.FromArgb(38, 42, 182);
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

            _playerShipsPosition.RotationShip();
        }
        public void BtnRotationReleased(object sender, MouseEventArgs e)
        {
            BtnRotation.Image = new Bitmap(Properties.Resources.btn_rotation_relesed);
        }
        private void BtnAutoPressed(object sender, MouseEventArgs e)
        {
            BtnAuto.Font = _embededFont.GetBtnFontPressed();
            BtnAuto.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);

            _playerShipsPosition.ClearField();
            _playerShipsPosition.SetShipOnField();
            _playerShipsPosition.SetImageShipOnField();
        }
        private void BtnAutoReleased(object sender, MouseEventArgs e)
        {
            BtnAuto.Font = _embededFont.GetBtnFontReleased();
            BtnAuto.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
        }
        private void BtnNextPressed(object sender, MouseEventArgs e)
        {
            BtnNext.Font = _embededFont.GetBtnFontPressed();
            BtnNext.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);

            ChangeBG();

            DrawImage drawImage = new DrawImage(this);
            drawImage.AddPlayerShipsToList(_playerFleet, _playerField);
        }
        private void BtnNextReleased(object sender, MouseEventArgs e)
        {
            BtnNext.Font = _embededFont.GetBtnFontReleased();
            BtnNext.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
        }
        private void BtnBackPressed(object sender, MouseEventArgs e)
        {
            BtnBack.BackgroundImage = new Bitmap(Properties.Resources.btn_back_pressed);
        }
        private void BtnBackReleased(object sender, MouseEventArgs e)
        {
            BtnBack.BackgroundImage = new Bitmap(Properties.Resources.btn_back_relesed);
        }
        private void ChangeBG()
        {
            this.BackgroundImage = new Bitmap(Properties.Resources.two_field);
        }
    }
}