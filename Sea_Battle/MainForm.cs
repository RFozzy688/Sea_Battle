using System.Text;

namespace Sea_Battle
{
    public partial class MainForm : Form
    {

        PlayingField _playerField;
        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.BackgroundImage = new Bitmap(Properties.Resources.one_field);

            _playerField = new PlayingField(this);
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
        public void BtnRotationRelesed(object sender, MouseEventArgs e)
        {
            BtnRotation.Image = new Bitmap(Properties.Resources.btn_rotation_relesed);
        }
        private void BtnAutoPressed(object sender, MouseEventArgs e)
        {
            BtnAuto.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);
            _playerField.ClearField();
            _playerField.SetShipOnField();
            _playerField.SetImageShipOnField();
        }
        private void BtnAutoRelesed(object sender, MouseEventArgs e)
        {
            BtnAuto.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
        }
        private void BtnNextPressed(object sender, MouseEventArgs e)
        {
            BtnNext.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);
        }
        private void BtnNextRelesed(object sender, MouseEventArgs e)
        {
            BtnNext.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
        }
    }
}