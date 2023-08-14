using System.Drawing;
using System.Globalization;
using System.Text;

namespace Sea_Battle
{
    public enum GameDifficulty
    {
        Hard,
        Easy
    }
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
        Sound _sound;
        Font _textButtonReleased; // текст на кнопках (отжата)
        Font _textButtonPressed; // текст на кнопках (нажата)
        Font _textLableStat; // текст на лейблах статистика
        Font _textLableResultBattle; // результат боя

        int _btnContinuePressed; // кнопка выполняет 2-е ф-ции - показ финального экрана и рестарт игры (0 - финальный экран, 1 - рестарт игры)
        bool _isBtnInBattlePressed;
        bool _isBtnClassicModePressed;
        bool _isSoundOn = true; // кнопка BtnSound вкл ли звук
        int _currentLanguage; // порядковый номер текущего языка для локализации 0 - ru, 1 - en, 2 - de, 3 - es
        int _selectedLanguage; // порядковый номер выбранного языка
        string? _strLocalization = null; // строка с локализацией
        List<string> _listLocalization; // все локализации
        GameDifficulty _gameDifficulty = GameDifficulty.Easy; // сложность игры
        public Color ColorText { get; }
        public Color ColorBG { get; }

        public MainForm()
        {
            LoadSettings();
            if (_strLocalization == null) { _strLocalization = "ru-RU"; }
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_strLocalization);

            InitializeComponent();

            //_loadScreen = new LoadScreen(this);
            //_loadScreen.Show();
            ShowInTaskbar = true;

            _embededFont = new EmbededFont();
            _drawImage = new DrawImage(this);
            _sound = new Sound(this);

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            ColorText = Color.FromArgb(38, 42, 182);
            ColorBG = Color.FromArgb(169, 94, 19);

            this.BackgroundImage = new Bitmap(Properties.Resources.bg_clear);
            this.BackColor = ColorBG;

            _isBtnInBattlePressed = false;
            _isBtnClassicModePressed = false;
            _btnContinuePressed = 0;
            _currentLanguage = 0;

            _textButtonReleased = _embededFont.CreateFont(23.0f, FontStyle.Regular);
            _textButtonPressed = _embededFont.CreateFont(20.0f, FontStyle.Regular);
            _textLableStat = _embededFont.CreateFont(30.0f, FontStyle.Regular);
            _textLableResultBattle = _embededFont.CreateFont(60.0f, FontStyle.Regular);

            InitControls();
            CreateListLocalization();

            ChoiceGameModeScreen();

        }

        public GameDifficulty GetGameDifficulty() { return _gameDifficulty; }
        public bool GetSoundOn() { return _isSoundOn; }
        public void SetBtnBackState(bool flag)
        {
            BtnBack.Enabled = flag;
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

            _sound.SoundMainScreenTimer(null, null);
            _sound.SoundMainScreenStartTimer();

            BtnClassicMode.Show();
            BtnExtendedMode.Show();
            BtnSetting.Show();

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
            _battle = new Battle(this, _playerFleet, _playerField, _enemyFleet, _enemyField, _drawImage, _aI, _sound);
            _gameStatistics = new GameStatistics(this);

            _drawImage.FinishRocketAnimationEvent += _battle.TransitionOfMoveInGame;
            _drawImage.FinishExplosionAnimationEvent += _battle.RepeatedShoot;
            _drawImage.InitializeStructPicture(new Point(1, 109), new Bitmap(Properties.Resources.left_field));
            _drawImage.AddImageToList();

            _battle.EndBattleEvent += EndBattle;
        }
        private void RestartGame()
        {
            _drawImage.FinishRocketAnimationEvent -= _battle.TransitionOfMoveInGame;
            _drawImage.FinishExplosionAnimationEvent -= _battle.RepeatedShoot;

            _drawImage.WhoShoot.Hide();

            _playerFleet.Dispose();
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
            _isBtnClassicModePressed = false;
            _btnContinuePressed = 0;
        }
        private void InitControls()
        {
            BtnAuto.Font = _textButtonReleased;
            BtnAuto.ForeColor = ColorText;

            BtnNext.Font = _textButtonReleased;
            BtnNext.ForeColor = ColorText;

            BtnToBattle.Font = _textButtonReleased;
            BtnToBattle.ForeColor = ColorText;

            BtnContinue.Font = _textButtonReleased;
            BtnContinue.ForeColor = ColorText;

            BtnExtendedMode.Font = _textButtonReleased;
            BtnExtendedMode.ForeColor = ColorText;

            BtnClassicMode.Font = _textButtonReleased;
            BtnClassicMode.ForeColor = ColorText;

            BtnHard.Font = _textButtonReleased;
            BtnHard.ForeColor = ColorText;

            BtnEasy.Font = _textButtonReleased;
            BtnEasy.ForeColor = ColorText;

            _battlesTotal.Font = _textLableStat;
            _battlesTotal.ForeColor = ColorText;
            _battlesTotal.Hide();

            _playerWin.Font = _textLableStat;
            _playerWin.ForeColor = ColorText;
            _playerWin.Hide();

            _enemyWin.Font = _textLableStat;
            _enemyWin.ForeColor = ColorText;
            _enemyWin.Hide();

            _textWin.Font = _textLableResultBattle;
            _textWin.ForeColor = ColorText;
            _textWin.Hide();

            _textLoss.Font = _textLableResultBattle;
            _textLoss.ForeColor = ColorText;
            _textLoss.Hide();

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
        }
        private void SaveSettings()
        {
            using (FileStream fs = new FileStream("settings.txt", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.Write(_strLocalization + "\n");
                    sw.Write(_isSoundOn + "\n");
                    sw.Write(((int)_gameDifficulty) + "\n");
                }
            }
        }
        private void LoadSettings()
        {
            using (FileStream fs = new FileStream("settings.txt", FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    _strLocalization = sr.ReadLine();
                    _isSoundOn = Boolean.Parse(sr.ReadLine());
                    _gameDifficulty = (GameDifficulty)Int32.Parse(sr.ReadLine());
                }
            }
        }
        private void CreateListLocalization()
        {
            _listLocalization = new List<string>();
            _listLocalization.Add("ru-RU");
            _listLocalization.Add("en-US");
            _listLocalization.Add("de-DE");
            _listLocalization.Add("es-ES");
        }
        private string GetStrLocalization(int index)
        {
            return _listLocalization[index];
        }
        private void SetImageLocalization()
        {
            if (_strLocalization == "ru-RU")
            {
                _language.BackgroundImage = new Bitmap(Properties.Resources.russia);
                _currentLanguage = 0;
            }
            else if (_strLocalization == "en-US")
            {
                _language.BackgroundImage = new Bitmap(Properties.Resources.english);
                _currentLanguage = 1;
            }
            else if (_strLocalization == "de-DE")
            {
                _language.BackgroundImage = new Bitmap(Properties.Resources.deutch);
                _currentLanguage = 2;
            }
            else if (_strLocalization == "es-ES")
            {
                _language.BackgroundImage = new Bitmap(Properties.Resources.espanol);
                _currentLanguage = 3;
            }
        }
        private void SetImageSound()
        {
            if (_isSoundOn) { BtnSound.BackgroundImage = new Bitmap(Properties.Resources.sound_released); }
            else { BtnSound.BackgroundImage = new Bitmap(Properties.Resources.no_sound_released); }
        }
        private void SetTextColorDifficulty()
        {
            if (_gameDifficulty == GameDifficulty.Hard) { BtnHard.ForeColor = Color.Red; }
            else if (_gameDifficulty == GameDifficulty.Easy) { BtnEasy.ForeColor = Color.Red; }
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
        public void BtnRotationReleased(object sender, MouseEventArgs e)
        {
            BtnRotation.Image = new Bitmap(Properties.Resources.btn_rotation_relesed);

            _playerShipsPosition.RotationShip();
        }
        private void BtnAutoReleased(object sender, MouseEventArgs e)
        {
            BtnAuto.Font = _textButtonReleased;
            BtnAuto.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);

            _playerShipsPosition.ClearField();
            _playerShipsPosition.SetShipOnField();
            _playerShipsPosition.SetImageShipOnField();
        }
        private void BtnNextReleased(object sender, MouseEventArgs e)
        {
            BtnNext.Font = _textButtonReleased;
            BtnNext.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
        }
        private void BtnBackReleased(object sender, MouseEventArgs e)
        {
            BtnBack.BackgroundImage = new Bitmap(Properties.Resources.btn_back_relesed);

            if (_currentLanguage != _selectedLanguage)
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_strLocalization);
                Controls.Clear();
                InitializeComponent();

                _currentLanguage = _selectedLanguage;
            }

            SaveSettings();

            _drawImage.ClearListDrawPicture();
            _drawImage.ClearBackground();

            if (_isBtnClassicModePressed) { RestartGame(); }

            InitControls();
            ChoiceGameModeScreen();

            //_drawImage.AddPlayerShipsToList(_enemyFleet, _enemyField);
            //_enemyShipsPosition.TestSave();
        }
        public void HideButtons()
        {
            BtnRotation.Hide();
            BtnAuto.Hide();
            BtnNext.Hide();
            BtnToBattle.Hide();
        }
        private void BtnToBattleReleased(object sender, MouseEventArgs e)
        {
            BtnToBattle.Font = _textButtonReleased;
            BtnToBattle.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);

            _sound.TimerSound.Stop();
            _sound.TimerSoundOcean.Stop();

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
            //_playerFleet.HideShips();

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
        private void BtnContinueReleased(object sender, MouseEventArgs e)
        {
            BtnContinue.Font = _textButtonReleased;
            BtnContinue.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);

            //BtnBack.Enabled = true;

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
                    _textWin.Show();
                }
                else
                {
                    // отрисовка финального изображения
                    _drawImage.InitializeStructPicture(new Point(3, 146), new Bitmap(Properties.Resources.img_loss));
                    _drawImage.AddImageToList();

                    // отрисовка текста "ПОРАЖЕНИЕ"
                    _textLoss.Show();
                }

                // фиксируем победителя в классе статистики
                _gameStatistics.Winner(_battle.Winner);

                // отрисовка статистики на финальном экране
                _battlesTotal.Show();
                _playerWin.Show();
                _enemyWin.Show();
                _drawImage.AddTextToList(_gameStatistics.GetBattleTotal(), new Point(240, 115), _embededFont.CreateFont(30.0f, FontStyle.Bold));
                _drawImage.AddTextToList(_gameStatistics.GetCountPlayerWin(), new Point(240, 170), _embededFont.CreateFont(30.0f, FontStyle.Bold));
                _drawImage.AddTextToList(_gameStatistics.GetCountEnemyWin(), new Point(240, 230), _embededFont.CreateFont(30.0f, FontStyle.Bold));

                // сохранение статистики
                _gameStatistics.SaveStats();
            }
            else
            {
                _drawImage.ClearListDrawPicture();
                _drawImage.ClearListDrawText();
                _drawImage.ClearBackground();

                _battlesTotal.Hide();
                _playerWin.Hide();
                _enemyWin.Hide();
                _textWin.Hide();
                _textLoss.Hide();
                BtnContinue.Hide();

                //BtnBack.Show();
                //BtnRotation.Show();
                //BtnToBattle.Show();
                //BtnAuto.Show();

                RestartGame();
                ChoiceGameModeScreen();
            }
        }
        private void BtnExtendedModeReleased(object sender, MouseEventArgs e)
        {
            BtnExtendedMode.Font = _textButtonReleased;
            BtnExtendedMode.BackgroundImage = new Bitmap(Properties.Resources.btn_long_released);
        }
        private void BtnClassicModeReleased(object sender, MouseEventArgs e)
        {
            BtnClassicMode.Font = _textButtonReleased;
            BtnClassicMode.BackgroundImage = new Bitmap(Properties.Resources.btn_long_released);
            _isBtnClassicModePressed = true;

            ClassicGameMode();

            BtnSetting.Hide();
            BtnExtendedMode.Hide();
            BtnClassicMode.Hide();

            BtnAuto.Show();
            BtnToBattle.Show();
            BtnBack.Show();
            BtnRotation.Show();
        }
        private void BtnSettingReleased(object sender, MouseEventArgs e)
        {
            BtnSetting.BackgroundImage = new Bitmap(Properties.Resources.settings_released);
            BtnSetting.Hide();
            BtnExtendedMode.Hide();
            BtnClassicMode.Hide();

            SettingsScreen();

            SetImageLocalization();
            _selectedLanguage = _currentLanguage;

            SetImageSound();
            SetTextColorDifficulty();

            BtnBack.Show();
            BtnRightLocalization.Show();
            BtnLeftLocalization.Show();
            BtnEasy.Show();
            BtnHard.Show();
            BtnSound.Show();
            _language.Show();
        }
        private void BtnLeftLocalizationReleased(object sender, MouseEventArgs e)
        {
            BtnLeftLocalization.BackgroundImage = new Bitmap(Properties.Resources.btn_green_arrow_left_released);

            _currentLanguage--;
            if (_currentLanguage < 0) { _currentLanguage = 3; }

            _strLocalization = GetStrLocalization(_currentLanguage);
            SetImageLocalization();
        }
        private void BtnRightLocalizationReleased(object sender, MouseEventArgs e)
        {
            BtnRightLocalization.BackgroundImage = new Bitmap(Properties.Resources.btn_green_arrow_right_released);

            _currentLanguage++;
            if (_currentLanguage > 3) { _currentLanguage = 0; }

            _strLocalization = GetStrLocalization(_currentLanguage);
            SetImageLocalization();
        }
        private void BtnHardReleased(object sender, MouseEventArgs e)
        {
            BtnHard.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
            BtnHard.Font = _textButtonReleased;

            _gameDifficulty = GameDifficulty.Hard;
        }
        private void BtnEasyReleased(object sender, MouseEventArgs e)
        {
            BtnEasy.BackgroundImage = new Bitmap(Properties.Resources.btn_relesed);
            BtnEasy.Font = _textButtonReleased;

            _gameDifficulty = GameDifficulty.Easy;
        }
        private void BtnSoundReleased(object sender, MouseEventArgs e)
        {
            _isSoundOn = !_isSoundOn;

            if (_isSoundOn) { BtnSound.BackgroundImage = new Bitmap(Properties.Resources.sound_released); }
            else { BtnSound.BackgroundImage = new Bitmap(Properties.Resources.no_sound_released); }
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

        private void BtnRotationPressed(object sender, MouseEventArgs e)
        {
            BtnRotation.Image = new Bitmap(Properties.Resources.btn_rotation_pressed);
        }
        private void BtnAutoPressed(object sender, MouseEventArgs e)
        {
            BtnAuto.Font = _textButtonPressed;
            BtnAuto.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);
        }
        private void BtnNextPressed(object sender, MouseEventArgs e)
        {
            BtnNext.Font = _textButtonPressed;
            BtnNext.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);
        }
        private void BtnBackPressed(object sender, MouseEventArgs e)
        {
            BtnBack.BackgroundImage = new Bitmap(Properties.Resources.btn_back_pressed);
        }
        private void BtnToBattlePressed(object sender, MouseEventArgs e)
        {
            BtnToBattle.Font = _textButtonPressed;
            BtnToBattle.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);
        }
        private void BtnContinuePressed(object sender, MouseEventArgs e)
        {
            BtnContinue.Font = _textButtonPressed;
            BtnContinue.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);
        }
        private void BtnExtendedModePressed(object sender, MouseEventArgs e)
        {
            BtnExtendedMode.Font = _textButtonPressed;
            BtnExtendedMode.BackgroundImage = new Bitmap(Properties.Resources.btn_long_pressed);
        }
        private void BtnClassicModePressed(object sender, MouseEventArgs e)
        {
            BtnClassicMode.Font = _textButtonPressed;
            BtnClassicMode.BackgroundImage = new Bitmap(Properties.Resources.btn_long_pressed);
        }
        private void BtnSettingPressed(object sender, MouseEventArgs e)
        {
            BtnSetting.BackgroundImage = new Bitmap(Properties.Resources.settings_pressed);
        }
        private void BtnLeftLocalizationPressed(object sender, MouseEventArgs e)
        {
            BtnLeftLocalization.BackgroundImage = new Bitmap(Properties.Resources.btn_green_arrow_left_pressed);
        }
        private void BtnRightLocalizationPressed(object sender, MouseEventArgs e)
        {
            BtnRightLocalization.BackgroundImage = new Bitmap(Properties.Resources.btn_green_arrow_right_pressed);
        }
        private void BtnHardPressed(object sender, MouseEventArgs e)
        {
            BtnHard.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);
            BtnHard.Font = _textButtonPressed;
            BtnHard.ForeColor = Color.Red;
            BtnEasy.ForeColor = ColorText;
        }
        private void BtnEasyPressed(object sender, MouseEventArgs e)
        {
            BtnEasy.BackgroundImage = new Bitmap(Properties.Resources.btn_pressed);
            BtnEasy.Font = _textButtonPressed;
            BtnEasy.ForeColor = Color.Red;
            BtnHard.ForeColor = ColorText;
        }
        private void BtnSoundPressed(object sender, MouseEventArgs e)
        {
            if (_isSoundOn) { BtnSound.BackgroundImage = new Bitmap(Properties.Resources.sound_pressed); }
            else { BtnSound.BackgroundImage = new Bitmap(Properties.Resources.no_sound_pressed); }
        }
    }
}