using System.Drawing;
using System.Text;

namespace Sea_Battle
{
    public partial class MainForm : Form
    {
        LoadScreen _loadScreen;

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
        AI _aI;

        int _btnContinuePressed; // кнопка выполняет 2-е ф-ции - показ финального экрана и рестарт игры (0 - финальный экран, 1 - рестарт игры)
        bool _isBtnInBattlePressed;
        bool _isSoundOn; // кнопка BtnSound вкл ли звук
        public Color ColorText { get; }
        public Color ColorBG { get; }

        public MainForm()
        {
            InitializeComponent();

            //_loadScreen = new LoadScreen(this);
            //_loadScreen.Show();
            ShowInTaskbar = true;

            _embededFont = new EmbededFont();
            _drawImage = new DrawImage(this);

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            ColorText = Color.FromArgb(38, 42, 182);
            ColorBG = Color.FromArgb(169, 94, 19);

            this.BackgroundImage = new Bitmap(Properties.Resources.bg_clear);
            this.BackColor = ColorBG;

            _isBtnInBattlePressed = false;
            _btnContinuePressed = 0;
            _isSoundOn = true;

            BtnAuto.Font = _embededFont.GetBtnFontReleased();
            BtnAuto.ForeColor = ColorText;

            BtnNext.Font = _embededFont.GetBtnFontReleased();
            BtnNext.ForeColor = ColorText;

            BtnToBattle.Font = _embededFont.GetBtnFontReleased();
            BtnToBattle.ForeColor = ColorText;

            BtnContinue.Font = _embededFont.GetBtnFontReleased();
            BtnContinue.ForeColor = ColorText;

            BtnExtendedMode.Font = _embededFont.CreateFont(25.0f, FontStyle.Bold);
            BtnExtendedMode.ForeColor = ColorText;

            BtnClassicMode.Font = _embededFont.CreateFont(25.0f, FontStyle.Bold);
            BtnClassicMode.ForeColor = ColorText;

            BtnHard.Font = _embededFont.CreateFont(25.0f, FontStyle.Bold);
            BtnHard.ForeColor = ColorText;

            BtnEasy.Font = _embededFont.CreateFont(25.0f, FontStyle.Bold);
            BtnEasy.ForeColor = ColorText;

            BtnContinue.Hide();
            BtnAuto.Hide();
            BtnNext.Hide();
            BtnToBattle.Hide();
            BtnBack.Hide();
            BtnRotation.Hide();
            BtnRightLocalization.Hide();
            BtnLeftLocalization.Hide();
            BtnEasy.Hide();
            BtnHard.Hide();
            BtnSound.Hide();
            _language.Hide();

            ChoiceGameModeScreen();

        }

        private void EndBattle()
        {
            BtnContinue.Show();
            BtnBack.Hide();
        }
        private void ChoiceGameModeScreen()
        {
            _drawImage.InitializeStructPicture(new Point(0, 100), new Bitmap(Properties.Resources.choice_mode));
            _drawImage.AddImageToList();

            BtnContinue.Hide();
            BtnAuto.Hide();
            BtnNext.Hide();
            BtnToBattle.Hide();
            BtnBack.Hide();
            BtnRotation.Hide();
        }
        private void SettingsScreen()
        {
            _drawImage.ClearListDrawPicture();
            _drawImage.ClearBackground();

            _drawImage.InitializeStructPicture(new Point(540, 316), new Bitmap(Properties.Resources.anchor));
            _drawImage.AddImageToList();
        }
        private void ClassicGameMode()
        {
            _drawImage.ClearListDrawPicture();
            _drawImage.ClearBackground();

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

            _aI = new AI(this, _playerField);
            _battle = new Battle(this, _playerFleet, _playerField, _enemyFleet, _enemyField, _drawImage, _aI);
            _gameStatistics = new GameStatistics(this);

            _drawImage.FinishRocketAnimationEvent += _battle.TransitionOfMoveInGame;
            _drawImage.FinishExplosionAnimationEvent += _battle.RepeatedShoot;
            _drawImage.InitializeStructPicture(new Point(1, 109), new Bitmap(Properties.Resources.left_field));
            _drawImage.AddImageToList();

            _battle.EndBattleEvent += EndBattle;
        }
        private void RestartGame()
        {
            _playerFleet = null;
            _playerField = null;
            _playerShipsPosition = null;

            _enemyFleet = null;
            _enemyField = null;
            _enemyShipsPosition = null;

            _battle = null;
            _gameStatistics = null;
            _aI = null;

            _isBtnInBattlePressed = false;
            _btnContinuePressed = 0;

            ClassicGameMode();
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
        }
        private void BtnToBattleReleased(object sender, MouseEventArgs e)
        {
            BtnToBattle.Font = _embededFont.GetBtnFontReleased();
            BtnToBattle.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);

            // растановка кораблей врага в рандомном порядке
            _enemyShipsPosition.SetShipOnField();
            _enemyShipsPosition.SetImageShipOnField();

            // если игрок не раставил все свои корабли и нажал кнопку "В бой"
            // то корабли раставятся в рандомном порядке
            if (!_playerShipsPosition.AreAllShipsOnField())
            {
                _playerShipsPosition.ClearField();
                _playerShipsPosition.SetShipOnField();
                _playerShipsPosition.SetImageShipOnField();
            }

            BtnRotation.Hide();
            BtnAuto.Hide();
            BtnNext.Hide();
            BtnToBattle.Hide();

            // отрисовка игрового поля врага
            _drawImage.InitializeStructPicture(new Point(520, 109), new Bitmap(Properties.Resources.right_field));
            _drawImage.AddImageToList();

            // отрисовка кораблей игрока на поле
            _drawImage.AddPlayerShipsToList(_playerFleet, _playerField);

            _isBtnInBattlePressed = true; // кнопка в бой была нажата
            _battle.Shooter = _battle.WhoFirstShoots(); // выбор первого хода
            _drawImage.WhoShoot.Show(); // показываем PictureBox чей сейчас ход

            // если враг игру начинает первым
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
        private void BtnContinuePressed(object sender, MouseEventArgs e)
        {
            BtnContinue.Font = _embededFont.GetBtnFontPressed();
            BtnContinue.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);
        }
        private void BtnContinueReleased(object sender, MouseEventArgs e)
        {
            BtnContinue.Font = _embededFont.GetBtnFontReleased();
            BtnContinue.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);

            if (_btnContinuePressed == 0)
            {
                _btnContinuePressed++; // == 1. при повторном нажатии рестар игры

                // отключаем отрисовку изображений на поле, т.е. обнуляем список с предыдущими изображениями
                // кораблей и т. д. они нам больше не нужны
                _drawImage.ClearListDrawPicture();
                _drawImage.ClearBackground();
                _drawImage.WhoShoot.Hide();

                if (_battle.Winner == EnumPlayers.player)
                {
                    // отрисовка финального изображения
                    _drawImage.InitializeStructPicture(new Point(121, 165), new Bitmap(Properties.Resources.img_win));
                    _drawImage.AddImageToList();
                    // отрисовка текста "ПОБЕДА"
                    _drawImage.AddTextToList("ПОБЕДА", new Point(450, 115), _embededFont.CreateFont(60.0f, FontStyle.Regular));
                }
                else
                {
                    // отрисовка финального изображения
                    _drawImage.InitializeStructPicture(new Point(3, 146), new Bitmap(Properties.Resources.img_loss));
                    _drawImage.AddImageToList();
                    // отрисовка текста "ПОРАЖЕНИЕ"
                    _drawImage.AddTextToList("ПОРАЖЕНИЕ", new Point(350, 115), _embededFont.CreateFont(60.0f, FontStyle.Regular));
                }

                // фиксируем победителя в классе статистики
                _gameStatistics.Winner(_battle.Winner);

                // отрисовка статистики на финальном экране
                _drawImage.AddTextToList(_gameStatistics.GetBattleTotal(), new Point(20, 108), _embededFont.CreateFont(30.0f, FontStyle.Bold));
                _drawImage.AddTextToList(_gameStatistics.GetCountPlayerWin(), new Point(20, 150), _embededFont.CreateFont(30.0f, FontStyle.Bold));
                _drawImage.AddTextToList(_gameStatistics.GetCountEnemyWin(), new Point(20, 195), _embededFont.CreateFont(30.0f, FontStyle.Bold));

                // сохранение статистики
                _gameStatistics.SaveStats();
            }
            else
            {
                _drawImage.ClearListDrawPicture();
                _drawImage.ClearListDrawText();
                _drawImage.ClearBackground();

                BtnBack.Show();
                BtnRotation.Show();
                BtnToBattle.Show();
                BtnAuto.Show();

                RestartGame();
            }
        }

        private void MainForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _playerShipsPosition.TestSave();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //this.Visible = false;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            //Bitmap
        }

        private void BtnExtendedModePressed(object sender, MouseEventArgs e)
        {
            BtnExtendedMode.Font = _embededFont.CreateFont(21.0f, FontStyle.Bold);
            BtnExtendedMode.BackgroundImage = new Bitmap(Properties.Resources.btn_long_pressed);
        }
        private void BtnExtendedModeReleased(object sender, MouseEventArgs e)
        {
            BtnExtendedMode.Font = _embededFont.CreateFont(25.0f, FontStyle.Bold);
            BtnExtendedMode.BackgroundImage = new Bitmap(Properties.Resources.btn_long_released);
        }
        private void BtnClassicModePressed(object sender, MouseEventArgs e)
        {
            BtnClassicMode.Font = _embededFont.CreateFont(21.0f, FontStyle.Bold);
            BtnClassicMode.BackgroundImage = new Bitmap(Properties.Resources.btn_long_pressed);
        }
        private void BtnClassicModeReleased(object sender, MouseEventArgs e)
        {
            BtnClassicMode.Font = _embededFont.CreateFont(25.0f, FontStyle.Bold);
            BtnClassicMode.BackgroundImage = new Bitmap(Properties.Resources.btn_long_released);

            ClassicGameMode();

            BtnSetting.Hide();
            BtnExtendedMode.Hide();
            BtnClassicMode.Hide();

            BtnAuto.Show();
            BtnToBattle.Show();
            BtnBack.Show();
            BtnRotation.Show();
        }
        private void BtnSettingPressed(object sender, MouseEventArgs e)
        {
            BtnSetting.BackgroundImage = new Bitmap(Properties.Resources.settings_pressed);
        }
        private void BtnSettingReleased(object sender, MouseEventArgs e)
        {
            BtnSetting.BackgroundImage = new Bitmap(Properties.Resources.settings_released);
            BtnSetting.Hide();
            BtnExtendedMode.Hide();
            BtnClassicMode.Hide();

            SettingsScreen();

            BtnBack.Show();
            BtnRightLocalization.Show();
            BtnLeftLocalization.Show();
            BtnEasy.Show();
            BtnHard.Show();
            BtnSound.Show();
            _language.Show();
        }

        private void BtnLeftLocalizationPressed(object sender, MouseEventArgs e)
        {
            BtnLeftLocalization.BackgroundImage = new Bitmap(Properties.Resources.btn_green_arrow_left_pressed);
        }
        private void BtnLeftLocalizationReleased(object sender, MouseEventArgs e)
        {
            BtnLeftLocalization.BackgroundImage = new Bitmap(Properties.Resources.btn_green_arrow_left_released);
        }
        private void BtnRightLocalizationPressed(object sender, MouseEventArgs e)
        {
            BtnRightLocalization.BackgroundImage = new Bitmap(Properties.Resources.btn_green_arrow_right_pressed);
        }
        private void BtnRightLocalizationReleased(object sender, MouseEventArgs e)
        {
            BtnRightLocalization.BackgroundImage = new Bitmap(Properties.Resources.btn_green_arrow_right_released);
        }
        private void BtnHardPressed(object sender, MouseEventArgs e)
        {
            BtnHard.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);
            BtnHard.Font = _embededFont.CreateFont(20.0f, FontStyle.Bold);

        }
        private void BtnHardReleased(object sender, MouseEventArgs e)
        {
            BtnHard.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
            BtnHard.Font = _embededFont.CreateFont(25.0f, FontStyle.Bold);
        }
        private void BtnEasyPressed(object sender, MouseEventArgs e)
        {
            BtnEasy.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);
            BtnEasy.Font = _embededFont.CreateFont(20.0f, FontStyle.Bold);
        }
        private void BtnEasyReleased(object sender, MouseEventArgs e)
        {
            BtnEasy.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
            BtnEasy.Font = _embededFont.CreateFont(25.0f, FontStyle.Bold);
        }
        private void BtnSoundPressed(object sender, MouseEventArgs e)
        {
            if (_isSoundOn)
            {
                BtnSound.BackgroundImage = new Bitmap(Properties.Resources.sound_pressed);
            }
            else
            {
                BtnSound.BackgroundImage = new Bitmap(Properties.Resources.no_sound_pressed);
            }
            //_isSoundOn = false;
        }
        private void BtnSoundReleased(object sender, MouseEventArgs e)
        {
            _isSoundOn = !_isSoundOn;

            if (_isSoundOn)
            {
                BtnSound.BackgroundImage = new Bitmap(Properties.Resources.sound_released);
            }
            else
            {
                BtnSound.BackgroundImage = new Bitmap(Properties.Resources.no_sound_released);
            }
            
        }
    }
}