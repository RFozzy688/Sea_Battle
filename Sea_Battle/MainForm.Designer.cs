namespace Sea_Battle
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            BtnRotation = new PictureBox();
            BtnAuto = new Button();
            BtnNext = new Button();
            BtnBack = new PictureBox();
            BtnToBattle = new Button();
            BtnContinue = new Button();
            BtnExtendedMode = new Button();
            BtnClassicMode = new Button();
            BtnSetting = new PictureBox();
            BtnRightLocalization = new PictureBox();
            BtnLeftLocalization = new PictureBox();
            _language = new PictureBox();
            BtnSound = new PictureBox();
            BtnHard = new Button();
            BtnEasy = new Button();
            ((System.ComponentModel.ISupportInitialize)BtnRotation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BtnBack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BtnSetting).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BtnRightLocalization).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BtnLeftLocalization).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_language).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BtnSound).BeginInit();
            SuspendLayout();
            // 
            // BtnRotation
            // 
            resources.ApplyResources(BtnRotation, "BtnRotation");
            BtnRotation.BackColor = Color.Transparent;
            BtnRotation.Image = Properties.Resources.btn_rotation_relesed;
            BtnRotation.Name = "BtnRotation";
            BtnRotation.TabStop = false;
            BtnRotation.MouseDown += BtnRotationPressed;
            BtnRotation.MouseUp += BtnRotationReleased;
            // 
            // BtnAuto
            // 
            resources.ApplyResources(BtnAuto, "BtnAuto");
            BtnAuto.BackColor = Color.Transparent;
            BtnAuto.BackgroundImage = Properties.Resources.btn_relesed;
            BtnAuto.CausesValidation = false;
            BtnAuto.FlatAppearance.BorderSize = 0;
            BtnAuto.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnAuto.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnAuto.Name = "BtnAuto";
            BtnAuto.TabStop = false;
            BtnAuto.UseCompatibleTextRendering = true;
            BtnAuto.UseVisualStyleBackColor = true;
            BtnAuto.MouseDown += BtnAutoPressed;
            BtnAuto.MouseUp += BtnAutoReleased;
            // 
            // BtnNext
            // 
            resources.ApplyResources(BtnNext, "BtnNext");
            BtnNext.BackColor = Color.Transparent;
            BtnNext.BackgroundImage = Properties.Resources.btn_relesed;
            BtnNext.CausesValidation = false;
            BtnNext.FlatAppearance.BorderSize = 0;
            BtnNext.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnNext.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnNext.Name = "BtnNext";
            BtnNext.TabStop = false;
            BtnNext.UseCompatibleTextRendering = true;
            BtnNext.UseVisualStyleBackColor = false;
            BtnNext.MouseDown += BtnNextPressed;
            BtnNext.MouseUp += BtnNextReleased;
            // 
            // BtnBack
            // 
            resources.ApplyResources(BtnBack, "BtnBack");
            BtnBack.BackColor = Color.Transparent;
            BtnBack.BackgroundImage = Properties.Resources.btn_back_relesed;
            BtnBack.Name = "BtnBack";
            BtnBack.TabStop = false;
            BtnBack.MouseDown += BtnBackPressed;
            BtnBack.MouseUp += BtnBackReleased;
            // 
            // BtnToBattle
            // 
            resources.ApplyResources(BtnToBattle, "BtnToBattle");
            BtnToBattle.BackColor = Color.Transparent;
            BtnToBattle.BackgroundImage = Properties.Resources.btn_relesed;
            BtnToBattle.CausesValidation = false;
            BtnToBattle.FlatAppearance.BorderSize = 0;
            BtnToBattle.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnToBattle.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnToBattle.Name = "BtnToBattle";
            BtnToBattle.TabStop = false;
            BtnToBattle.UseCompatibleTextRendering = true;
            BtnToBattle.UseVisualStyleBackColor = false;
            BtnToBattle.MouseDown += BtnToBattlePressed;
            BtnToBattle.MouseUp += BtnToBattleReleased;
            // 
            // BtnContinue
            // 
            resources.ApplyResources(BtnContinue, "BtnContinue");
            BtnContinue.BackColor = Color.Transparent;
            BtnContinue.BackgroundImage = Properties.Resources.btn_relesed;
            BtnContinue.CausesValidation = false;
            BtnContinue.FlatAppearance.BorderSize = 0;
            BtnContinue.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnContinue.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnContinue.ForeColor = SystemColors.ControlText;
            BtnContinue.Name = "BtnContinue";
            BtnContinue.TabStop = false;
            BtnContinue.UseCompatibleTextRendering = true;
            BtnContinue.UseVisualStyleBackColor = true;
            BtnContinue.MouseDown += BtnContinuePressed;
            BtnContinue.MouseUp += BtnContinueReleased;
            // 
            // BtnExtendedMode
            // 
            resources.ApplyResources(BtnExtendedMode, "BtnExtendedMode");
            BtnExtendedMode.BackColor = Color.Transparent;
            BtnExtendedMode.BackgroundImage = Properties.Resources.btn_long_released;
            BtnExtendedMode.FlatAppearance.BorderSize = 0;
            BtnExtendedMode.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnExtendedMode.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnExtendedMode.Name = "BtnExtendedMode";
            BtnExtendedMode.UseCompatibleTextRendering = true;
            BtnExtendedMode.UseVisualStyleBackColor = false;
            BtnExtendedMode.MouseDown += BtnExtendedModePressed;
            BtnExtendedMode.MouseUp += BtnExtendedModeReleased;
            // 
            // BtnClassicMode
            // 
            resources.ApplyResources(BtnClassicMode, "BtnClassicMode");
            BtnClassicMode.BackColor = Color.Transparent;
            BtnClassicMode.BackgroundImage = Properties.Resources.btn_long_released;
            BtnClassicMode.FlatAppearance.BorderSize = 0;
            BtnClassicMode.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnClassicMode.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnClassicMode.Name = "BtnClassicMode";
            BtnClassicMode.UseCompatibleTextRendering = true;
            BtnClassicMode.UseVisualStyleBackColor = false;
            BtnClassicMode.MouseDown += BtnClassicModePressed;
            BtnClassicMode.MouseUp += BtnClassicModeReleased;
            // 
            // BtnSetting
            // 
            resources.ApplyResources(BtnSetting, "BtnSetting");
            BtnSetting.BackColor = Color.Transparent;
            BtnSetting.BackgroundImage = Properties.Resources.settings_released;
            BtnSetting.Name = "BtnSetting";
            BtnSetting.TabStop = false;
            BtnSetting.MouseDown += BtnSettingPressed;
            BtnSetting.MouseUp += BtnSettingReleased;
            // 
            // BtnRightLocalization
            // 
            resources.ApplyResources(BtnRightLocalization, "BtnRightLocalization");
            BtnRightLocalization.BackColor = Color.Transparent;
            BtnRightLocalization.BackgroundImage = Properties.Resources.btn_green_arrow_right_released;
            BtnRightLocalization.Name = "BtnRightLocalization";
            BtnRightLocalization.TabStop = false;
            BtnRightLocalization.MouseDown += BtnRightLocalizationPressed;
            BtnRightLocalization.MouseUp += BtnRightLocalizationReleased;
            // 
            // BtnLeftLocalization
            // 
            resources.ApplyResources(BtnLeftLocalization, "BtnLeftLocalization");
            BtnLeftLocalization.BackColor = Color.Transparent;
            BtnLeftLocalization.BackgroundImage = Properties.Resources.btn_green_arrow_left_released;
            BtnLeftLocalization.Name = "BtnLeftLocalization";
            BtnLeftLocalization.TabStop = false;
            BtnLeftLocalization.MouseDown += BtnLeftLocalizationPressed;
            BtnLeftLocalization.MouseUp += BtnLeftLocalizationReleased;
            // 
            // _language
            // 
            resources.ApplyResources(_language, "_language");
            _language.BackColor = Color.Transparent;
            _language.BackgroundImage = Properties.Resources.russia;
            _language.Name = "_language";
            _language.TabStop = false;
            // 
            // BtnSound
            // 
            resources.ApplyResources(BtnSound, "BtnSound");
            BtnSound.BackColor = Color.Transparent;
            BtnSound.BackgroundImage = Properties.Resources.sound_released;
            BtnSound.Name = "BtnSound";
            BtnSound.TabStop = false;
            BtnSound.MouseDown += BtnSoundPressed;
            BtnSound.MouseUp += BtnSoundReleased;
            // 
            // BtnHard
            // 
            resources.ApplyResources(BtnHard, "BtnHard");
            BtnHard.BackColor = Color.Transparent;
            BtnHard.BackgroundImage = Properties.Resources.btn_relesed;
            BtnHard.CausesValidation = false;
            BtnHard.FlatAppearance.BorderSize = 0;
            BtnHard.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnHard.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnHard.Name = "BtnHard";
            BtnHard.TabStop = false;
            BtnHard.UseCompatibleTextRendering = true;
            BtnHard.UseVisualStyleBackColor = true;
            BtnHard.MouseDown += BtnHardPressed;
            BtnHard.MouseUp += BtnHardReleased;
            // 
            // BtnEasy
            // 
            resources.ApplyResources(BtnEasy, "BtnEasy");
            BtnEasy.BackColor = Color.Transparent;
            BtnEasy.BackgroundImage = Properties.Resources.btn_relesed;
            BtnEasy.CausesValidation = false;
            BtnEasy.FlatAppearance.BorderSize = 0;
            BtnEasy.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnEasy.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnEasy.Name = "BtnEasy";
            BtnEasy.TabStop = false;
            BtnEasy.UseCompatibleTextRendering = true;
            BtnEasy.UseVisualStyleBackColor = true;
            BtnEasy.MouseDown += BtnEasyPressed;
            BtnEasy.MouseUp += BtnEasyReleased;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.bg_clear;
            Controls.Add(BtnEasy);
            Controls.Add(BtnHard);
            Controls.Add(BtnSound);
            Controls.Add(_language);
            Controls.Add(BtnLeftLocalization);
            Controls.Add(BtnRightLocalization);
            Controls.Add(BtnSetting);
            Controls.Add(BtnClassicMode);
            Controls.Add(BtnExtendedMode);
            Controls.Add(BtnContinue);
            Controls.Add(BtnToBattle);
            Controls.Add(BtnBack);
            Controls.Add(BtnNext);
            Controls.Add(BtnAuto);
            Controls.Add(BtnRotation);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            ShowInTaskbar = false;
            Shown += MainForm_Shown;
            Paint += MainForm_Paint;
            MouseDoubleClick += MainForm_MouseDoubleClick;
            MouseDown += MainForm_MouseDown;
            MouseMove += MainForm_MouseMove;
            ((System.ComponentModel.ISupportInitialize)BtnRotation).EndInit();
            ((System.ComponentModel.ISupportInitialize)BtnBack).EndInit();
            ((System.ComponentModel.ISupportInitialize)BtnSetting).EndInit();
            ((System.ComponentModel.ISupportInitialize)BtnRightLocalization).EndInit();
            ((System.ComponentModel.ISupportInitialize)BtnLeftLocalization).EndInit();
            ((System.ComponentModel.ISupportInitialize)_language).EndInit();
            ((System.ComponentModel.ISupportInitialize)BtnSound).EndInit();
            ResumeLayout(false);
        }

        #endregion
        public PictureBox BtnRotation;
        public Button BtnAuto;
        public Button BtnNext;
        private PictureBox BtnBack;
        public Button BtnToBattle;
        public Button BtnContinue;
        private Button BtnExtendedMode;
        private Button BtnClassicMode;
        private PictureBox BtnSetting;
        private PictureBox BtnRightLocalization;
        private PictureBox BtnLeftLocalization;
        private PictureBox _language;
        private PictureBox BtnSound;
        public Button BtnHard;
        public Button BtnEasy;
    }
}