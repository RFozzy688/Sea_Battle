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
            BtnRotation.BackColor = Color.Transparent;
            BtnRotation.Image = Properties.Resources.btn_rotation_relesed;
            BtnRotation.Location = new Point(459, 479);
            BtnRotation.Name = "BtnRotation";
            BtnRotation.Size = new Size(100, 98);
            BtnRotation.TabIndex = 0;
            BtnRotation.TabStop = false;
            BtnRotation.MouseDown += BtnRotationPressed;
            BtnRotation.MouseUp += BtnRotationReleased;
            // 
            // BtnAuto
            // 
            BtnAuto.BackColor = Color.Transparent;
            BtnAuto.BackgroundImage = Properties.Resources.btn_relesed;
            BtnAuto.BackgroundImageLayout = ImageLayout.Center;
            BtnAuto.CausesValidation = false;
            BtnAuto.FlatAppearance.BorderSize = 0;
            BtnAuto.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnAuto.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnAuto.FlatStyle = FlatStyle.Flat;
            BtnAuto.Location = new Point(571, 501);
            BtnAuto.Name = "BtnAuto";
            BtnAuto.Size = new Size(212, 76);
            BtnAuto.TabIndex = 4;
            BtnAuto.TabStop = false;
            BtnAuto.Text = "Авто";
            BtnAuto.UseCompatibleTextRendering = true;
            BtnAuto.UseVisualStyleBackColor = true;
            BtnAuto.MouseDown += BtnAutoPressed;
            BtnAuto.MouseUp += BtnAutoReleased;
            // 
            // BtnNext
            // 
            BtnNext.BackColor = Color.Transparent;
            BtnNext.BackgroundImage = Properties.Resources.btn_relesed;
            BtnNext.BackgroundImageLayout = ImageLayout.Center;
            BtnNext.CausesValidation = false;
            BtnNext.FlatAppearance.BorderSize = 0;
            BtnNext.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnNext.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnNext.FlatStyle = FlatStyle.Flat;
            BtnNext.Location = new Point(789, 501);
            BtnNext.Name = "BtnNext";
            BtnNext.Size = new Size(212, 76);
            BtnNext.TabIndex = 5;
            BtnNext.TabStop = false;
            BtnNext.Text = "Далее";
            BtnNext.UseCompatibleTextRendering = true;
            BtnNext.UseVisualStyleBackColor = false;
            BtnNext.Visible = false;
            BtnNext.MouseDown += BtnNextPressed;
            BtnNext.MouseUp += BtnNextReleased;
            // 
            // BtnBack
            // 
            BtnBack.BackColor = Color.Transparent;
            BtnBack.BackgroundImage = Properties.Resources.btn_back_relesed;
            BtnBack.BackgroundImageLayout = ImageLayout.Center;
            BtnBack.Location = new Point(26, 12);
            BtnBack.Name = "BtnBack";
            BtnBack.Size = new Size(80, 80);
            BtnBack.TabIndex = 6;
            BtnBack.TabStop = false;
            BtnBack.MouseDown += BtnBackPressed;
            BtnBack.MouseUp += BtnBackReleased;
            // 
            // BtnToBattle
            // 
            BtnToBattle.BackColor = Color.Transparent;
            BtnToBattle.BackgroundImage = Properties.Resources.btn_relesed;
            BtnToBattle.BackgroundImageLayout = ImageLayout.Center;
            BtnToBattle.CausesValidation = false;
            BtnToBattle.FlatAppearance.BorderSize = 0;
            BtnToBattle.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnToBattle.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnToBattle.FlatStyle = FlatStyle.Flat;
            BtnToBattle.Location = new Point(789, 501);
            BtnToBattle.Name = "BtnToBattle";
            BtnToBattle.Size = new Size(212, 76);
            BtnToBattle.TabIndex = 7;
            BtnToBattle.TabStop = false;
            BtnToBattle.Text = "В бой";
            BtnToBattle.UseCompatibleTextRendering = true;
            BtnToBattle.UseVisualStyleBackColor = false;
            BtnToBattle.MouseDown += BtnToBattlePressed;
            BtnToBattle.MouseUp += BtnToBattleReleased;
            // 
            // BtnContinue
            // 
            BtnContinue.BackColor = Color.Transparent;
            BtnContinue.BackgroundImage = Properties.Resources.btn_relesed;
            BtnContinue.BackgroundImageLayout = ImageLayout.Center;
            BtnContinue.CausesValidation = false;
            BtnContinue.FlatAppearance.BorderSize = 0;
            BtnContinue.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnContinue.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnContinue.FlatStyle = FlatStyle.Flat;
            BtnContinue.ForeColor = SystemColors.ControlText;
            BtnContinue.Location = new Point(789, 16);
            BtnContinue.Name = "BtnContinue";
            BtnContinue.Size = new Size(212, 76);
            BtnContinue.TabIndex = 8;
            BtnContinue.TabStop = false;
            BtnContinue.Text = "Далее...";
            BtnContinue.UseCompatibleTextRendering = true;
            BtnContinue.UseVisualStyleBackColor = true;
            BtnContinue.MouseDown += BtnContinuePressed;
            BtnContinue.MouseUp += BtnContinueReleased;
            // 
            // BtnExtendedMode
            // 
            BtnExtendedMode.BackColor = Color.Transparent;
            BtnExtendedMode.BackgroundImage = Properties.Resources.btn_long_released;
            BtnExtendedMode.BackgroundImageLayout = ImageLayout.Center;
            BtnExtendedMode.FlatAppearance.BorderSize = 0;
            BtnExtendedMode.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnExtendedMode.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnExtendedMode.FlatStyle = FlatStyle.Flat;
            BtnExtendedMode.Location = new Point(298, 272);
            BtnExtendedMode.Name = "BtnExtendedMode";
            BtnExtendedMode.Size = new Size(441, 83);
            BtnExtendedMode.TabIndex = 9;
            BtnExtendedMode.Text = "Расширенный режим";
            BtnExtendedMode.UseCompatibleTextRendering = true;
            BtnExtendedMode.UseVisualStyleBackColor = false;
            BtnExtendedMode.MouseDown += BtnExtendedModePressed;
            BtnExtendedMode.MouseUp += BtnExtendedModeReleased;
            // 
            // BtnClassicMode
            // 
            BtnClassicMode.BackColor = Color.Transparent;
            BtnClassicMode.BackgroundImage = Properties.Resources.btn_long_released;
            BtnClassicMode.BackgroundImageLayout = ImageLayout.Center;
            BtnClassicMode.FlatAppearance.BorderSize = 0;
            BtnClassicMode.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnClassicMode.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnClassicMode.FlatStyle = FlatStyle.Flat;
            BtnClassicMode.Location = new Point(298, 381);
            BtnClassicMode.Name = "BtnClassicMode";
            BtnClassicMode.Size = new Size(441, 83);
            BtnClassicMode.TabIndex = 10;
            BtnClassicMode.Text = "Классический режим";
            BtnClassicMode.UseCompatibleTextRendering = true;
            BtnClassicMode.UseVisualStyleBackColor = false;
            BtnClassicMode.MouseDown += BtnClassicModePressed;
            BtnClassicMode.MouseUp += BtnClassicModeReleased;
            // 
            // BtnSetting
            // 
            BtnSetting.BackColor = Color.Transparent;
            BtnSetting.BackgroundImage = Properties.Resources.settings_released;
            BtnSetting.BackgroundImageLayout = ImageLayout.Center;
            BtnSetting.Location = new Point(26, 1);
            BtnSetting.Name = "BtnSetting";
            BtnSetting.Size = new Size(90, 91);
            BtnSetting.TabIndex = 11;
            BtnSetting.TabStop = false;
            BtnSetting.MouseDown += BtnSettingPressed;
            BtnSetting.MouseUp += BtnSettingReleased;
            // 
            // BtnRightLocalization
            // 
            BtnRightLocalization.BackColor = Color.Transparent;
            BtnRightLocalization.BackgroundImage = Properties.Resources.btn_green_arrow_right_released;
            BtnRightLocalization.BackgroundImageLayout = ImageLayout.Center;
            BtnRightLocalization.Location = new Point(918, 109);
            BtnRightLocalization.Name = "BtnRightLocalization";
            BtnRightLocalization.Size = new Size(82, 117);
            BtnRightLocalization.TabIndex = 12;
            BtnRightLocalization.TabStop = false;
            BtnRightLocalization.MouseDown += BtnRightLocalizationPressed;
            BtnRightLocalization.MouseUp += BtnRightLocalizationReleased;
            // 
            // BtnLeftLocalization
            // 
            BtnLeftLocalization.BackColor = Color.Transparent;
            BtnLeftLocalization.BackgroundImage = Properties.Resources.btn_green_arrow_left_released;
            BtnLeftLocalization.BackgroundImageLayout = ImageLayout.Center;
            BtnLeftLocalization.Location = new Point(534, 109);
            BtnLeftLocalization.Name = "BtnLeftLocalization";
            BtnLeftLocalization.Size = new Size(82, 117);
            BtnLeftLocalization.TabIndex = 13;
            BtnLeftLocalization.TabStop = false;
            BtnLeftLocalization.MouseDown += BtnLeftLocalizationPressed;
            BtnLeftLocalization.MouseUp += BtnLeftLocalizationReleased;
            // 
            // _language
            // 
            _language.BackColor = Color.Transparent;
            _language.BackgroundImage = Properties.Resources.russia;
            _language.BackgroundImageLayout = ImageLayout.Center;
            _language.Location = new Point(625, 127);
            _language.Name = "_language";
            _language.Size = new Size(284, 80);
            _language.TabIndex = 14;
            _language.TabStop = false;
            // 
            // BtnSound
            // 
            BtnSound.BackColor = Color.Transparent;
            BtnSound.BackgroundImage = Properties.Resources.sound_released;
            BtnSound.BackgroundImageLayout = ImageLayout.Center;
            BtnSound.Location = new Point(155, 355);
            BtnSound.Name = "BtnSound";
            BtnSound.Size = new Size(205, 215);
            BtnSound.TabIndex = 15;
            BtnSound.TabStop = false;
            BtnSound.MouseDown += BtnSoundPressed;
            BtnSound.MouseUp += BtnSoundReleased;
            // 
            // BtnHard
            // 
            BtnHard.BackColor = Color.Transparent;
            BtnHard.BackgroundImage = Properties.Resources.btn_relesed;
            BtnHard.BackgroundImageLayout = ImageLayout.Center;
            BtnHard.CausesValidation = false;
            BtnHard.FlatAppearance.BorderSize = 0;
            BtnHard.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnHard.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnHard.FlatStyle = FlatStyle.Flat;
            BtnHard.Location = new Point(155, 150);
            BtnHard.Name = "BtnHard";
            BtnHard.Size = new Size(212, 76);
            BtnHard.TabIndex = 16;
            BtnHard.TabStop = false;
            BtnHard.Text = "Сложный";
            BtnHard.UseCompatibleTextRendering = true;
            BtnHard.UseVisualStyleBackColor = true;
            BtnHard.MouseDown += BtnHardPressed;
            BtnHard.MouseUp += BtnHardReleased;
            // 
            // BtnEasy
            // 
            BtnEasy.BackColor = Color.Transparent;
            BtnEasy.BackgroundImage = Properties.Resources.btn_relesed;
            BtnEasy.BackgroundImageLayout = ImageLayout.Center;
            BtnEasy.CausesValidation = false;
            BtnEasy.FlatAppearance.BorderSize = 0;
            BtnEasy.FlatAppearance.MouseDownBackColor = Color.Transparent;
            BtnEasy.FlatAppearance.MouseOverBackColor = Color.Transparent;
            BtnEasy.FlatStyle = FlatStyle.Flat;
            BtnEasy.Location = new Point(155, 254);
            BtnEasy.Name = "BtnEasy";
            BtnEasy.Size = new Size(212, 76);
            BtnEasy.TabIndex = 17;
            BtnEasy.TabStop = false;
            BtnEasy.Text = "Лёгкий";
            BtnEasy.UseCompatibleTextRendering = true;
            BtnEasy.UseVisualStyleBackColor = true;
            BtnEasy.MouseDown += BtnEasyPressed;
            BtnEasy.MouseUp += BtnEasyReleased;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.bg_clear;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1024, 603);
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
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
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