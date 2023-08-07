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
            ((System.ComponentModel.ISupportInitialize)BtnRotation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BtnBack).BeginInit();
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1024, 603);
            Controls.Add(BtnToBattle);
            Controls.Add(BtnBack);
            Controls.Add(BtnNext);
            Controls.Add(BtnAuto);
            Controls.Add(BtnRotation);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            MouseDown += MainForm_MouseDown;
            MouseMove += MainForm_MouseMove;
            ((System.ComponentModel.ISupportInitialize)BtnRotation).EndInit();
            ((System.ComponentModel.ISupportInitialize)BtnBack).EndInit();
            ResumeLayout(false);
        }

        #endregion
        public PictureBox BtnRotation;
        public Button BtnAuto;
        public Button BtnNext;
        private PictureBox BtnBack;
        public Button BtnToBattle;
    }
}