using System.Drawing;
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

        DrawImage _drawImage;
        Battle _battle;
        EmbededFont _embededFont;
        GameStatistics _gameStatistics;

        bool _isBtnInBattlePressed;
        public Color ColorText { get; }
        public Color ColorBG { get; }

        public MainForm()
        {
            InitializeComponent();

            ColorText = Color.FromArgb(38, 42, 182);
            ColorBG = Color.FromArgb(169, 94, 19);

            _isBtnInBattlePressed = false;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.BackgroundImage = new Bitmap(Properties.Resources.bg_clear);
            this.BackColor = ColorBG;

            _playerFleet = new CreateFleetOfShips(this);
            _playerField = new CreatePlayingField();
            _playerShipsPosition = new ManualPositioningOfShips(this, _playerFleet, _playerField);

            _playerField.CreateField(new Point(23, 142), new Point(66, 185));
            _playerFleet.CreateShips(new Point(540, 142), 43, true, _playerShipsPosition);

            _enemyFleet = new CreateFleetOfShips(this);
            _enemyField = new CreatePlayingField();
            _enemyShipsPosition = new AutomaticPositioningOfShips(_enemyFleet, _enemyField);

            _enemyField.CreateField(new Point(540, 142), new Point(583, 185));
            _enemyFleet.CreateShips(new Point(200, 0), 0, false, null);

            _embededFont = new EmbededFont();
            _drawImage = new DrawImage(this);
            _battle = new Battle(this, _playerFleet, _playerField, _enemyFleet, _enemyField, _drawImage);
            _gameStatistics = new GameStatistics(this);

            _drawImage.FinishRocketAnimationEvent += _battle.StartEnemyShoots;
            _drawImage.FinishExplosionAnimationEvent += _battle.RepeatedShoot;
            _drawImage.InitializeStructPicture(new Point(1, 109), new Bitmap(Properties.Resources.left_field));
            _drawImage.AddImageToList();

            BtnAuto.Font = _embededFont.GetBtnFontReleased();
            BtnAuto.ForeColor = ColorText;

            BtnNext.Font = _embededFont.GetBtnFontReleased();
            BtnNext.ForeColor = ColorText;

            BtnToBattle.Font = _embededFont.GetBtnFontReleased();
            BtnToBattle.ForeColor = ColorText;

            BtnContinue.Font = _embededFont.GetBtnFontReleased();
            BtnContinue.ForeColor = ColorText;
            //BtnContinue.Hide();

            _battle.EndBattleEvent += EndBattle;
        }

        private void EndBattle()
        {
            BtnContinue.Show();
            BtnBack.Hide();
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            Text = e.X + " " + e.Y;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (_battle.Shooter == EnumPlayers.player && _battle.IsCanPressed && _isBtnInBattlePressed)
            {
                _battle.HitLocation = e.Location;

                if (_battle.IsConvertHitLocationToIndexes())
                {
                    _battle.IsCanPressed = false;
                    _battle.Fire();
                }
            }
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
        }
        private void BtnNextReleased(object sender, MouseEventArgs e)
        {
            BtnNext.Font = _embededFont.GetBtnFontReleased();
            BtnNext.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
        }
        private void BtnBackPressed(object sender, MouseEventArgs e)
        {
            BtnBack.BackgroundImage = new Bitmap(Properties.Resources.btn_back_pressed);

            //_drawImage.AddPlayerShipsToList(_enemyFleet, _enemyField);
            _enemyShipsPosition.TestSave();
        }
        private void BtnBackReleased(object sender, MouseEventArgs e)
        {
            BtnBack.BackgroundImage = new Bitmap(Properties.Resources.btn_back_relesed);
        }
        public void HideButtons()
        {
            BtnRotation.Hide();
            BtnAuto.Hide();
            BtnNext.Hide();
            BtnToBattle.Hide();
        }
        private void BtnToBattlePressed(object sender, MouseEventArgs e)
        {
            BtnToBattle.Font = _embededFont.GetBtnFontPressed();
            BtnToBattle.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);

            _enemyShipsPosition.SetShipOnField();
            _enemyShipsPosition.SetImageShipOnField();

            if (!_playerShipsPosition.AreAllShipsOnField())
            {
                _playerShipsPosition.ClearField();
                _playerShipsPosition.SetShipOnField();
                _playerShipsPosition.SetImageShipOnField();
            }

            _drawImage.InitializeStructPicture(new Point(520, 109), new Bitmap(Properties.Resources.right_field));
            _drawImage.AddImageToList();

            HideButtons();

            _drawImage.AddPlayerShipsToList(_playerFleet, _playerField);

            _isBtnInBattlePressed = true;
            _battle.Shooter = _battle.WhoFirstShoots();

            if (_battle.Shooter == EnumPlayers.enemy)
            {
                _battle.EnemyShoots(null, null);
                _drawImage.SetImageWhoShooter(_battle.Shooter);
            }
            else
            {
                _drawImage.SetImageWhoShooter(_battle.Shooter);
            }
        }
        private void BtnToBattleReleased(object sender, MouseEventArgs e)
        {
            BtnToBattle.Font = _embededFont.GetBtnFontReleased();
            BtnToBattle.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
        }
        private void BtnContinuePressed(object sender, MouseEventArgs e)
        {
            BtnContinue.Font = _embededFont.GetBtnFontPressed();
            BtnContinue.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);

            // отключаем отрисовку изображений на поле
            _drawImage.ClearListDrawPicture();
            _drawImage.ClearBackground();

            if (_battle.Winner == EnumPlayers.player)
            {
                _drawImage.InitializeStructPicture(new Point(121, 165), new Bitmap(Properties.Resources.img_win));
                _drawImage.AddImageToList();
                _drawImage.AddTextToList("ПОБЕДА", new Point(450, 115), _embededFont.CreateFont(60.0f, FontStyle.Regular));
            }
            else
            {
                _drawImage.InitializeStructPicture(new Point(3, 146), new Bitmap(Properties.Resources.img_loss));
                _drawImage.AddImageToList();
                _drawImage.AddTextToList("ПОРАЖЕНИЕ", new Point(350, 115), _embededFont.CreateFont(60.0f, FontStyle.Regular));
            }

            _gameStatistics.Winner(_battle.Winner);

            _drawImage.AddTextToList(_gameStatistics.GetBattleTotal(), new Point(20, 108), _embededFont.CreateFont(30.0f, FontStyle.Bold));
            _drawImage.AddTextToList(_gameStatistics.GetCountPlayerWin(), new Point(20, 150), _embededFont.CreateFont(30.0f, FontStyle.Bold));
            _drawImage.AddTextToList(_gameStatistics.GetCountEnemyWin(), new Point(20, 195), _embededFont.CreateFont(30.0f, FontStyle.Bold));

            _gameStatistics.SaveStats();
        }

        private void BtnContinueReleased(object sender, MouseEventArgs e)
        {
            BtnContinue.Font = _embededFont.GetBtnFontReleased();
            BtnContinue.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
        }
    }
}