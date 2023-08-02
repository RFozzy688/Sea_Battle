using System.Text;

namespace Sea_Battle
{
    public partial class MainForm : Form
    {

        PlayingField _playerField;
        EmbededFont _embededFont;
        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.BackgroundImage = new Bitmap(Properties.Resources.one_field);

            _playerField = new PlayingField(this);
            _embededFont = new EmbededFont();

            BtnAuto.Font = _embededFont.GetBtnFontRelesed();
            BtnNext.Font = _embededFont.GetBtnFontRelesed();
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

            _playerField.RotationShip();
        }
        public void BtnRotationRelesed(object sender, MouseEventArgs e)
        {
            BtnRotation.Image = new Bitmap(Properties.Resources.btn_rotation_relesed);
        }
        private void BtnAutoPressed(object sender, MouseEventArgs e)
        {
            BtnAuto.Font = _embededFont.GetBtnFontPressed();
            BtnAuto.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);
            _playerField.ClearField();
            _playerField.SetShipOnField();
            _playerField.SetImageShipOnField();
        }
        private void BtnAutoRelesed(object sender, MouseEventArgs e)
        {
            BtnAuto.Font = _embededFont.GetBtnFontRelesed();
            BtnAuto.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
        }
        private void BtnNextPressed(object sender, MouseEventArgs e)
        {
            BtnNext.Font = _embededFont.GetBtnFontPressed();
            BtnNext.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);
        }
        private void BtnNextRelesed(object sender, MouseEventArgs e)
        {
            BtnNext.Font = _embededFont.GetBtnFontRelesed();
            BtnNext.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
        }
        private void BtnBackPressed(object sender, MouseEventArgs e)
        {
            BtnBack.BackgroundImage = new Bitmap(Properties.Resources.btn_back_pressed);
        }
        private void BtnBackRelesed(object sender, MouseEventArgs e)
        {
            BtnBack.BackgroundImage = new Bitmap(Properties.Resources.btn_back_relesed);
        }
    }
}