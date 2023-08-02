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
            ((System.ComponentModel.ISupportInitialize)BtnRotation).BeginInit();
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
            BtnRotation.MouseUp += BtnRotationRelesed;
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
            BtnAuto.UseVisualStyleBackColor = false;
            BtnAuto.MouseDown += BtnAutoPressed;
            BtnAuto.MouseUp += BtnAutoRelesed;
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
            BtnNext.UseVisualStyleBackColor = false;
            BtnNext.MouseDown += BtnNextPressed;
            BtnNext.MouseUp += BtnNextRelesed;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.one_field;
            ClientSize = new Size(1024, 603);
            Controls.Add(BtnNext);
            Controls.Add(BtnAuto);
            Controls.Add(BtnRotation);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            MouseDown += MainForm_MouseDown;
            MouseMove += MainForm_MouseMove;
            ((System.ComponentModel.ISupportInitialize)BtnRotation).EndInit();
            ResumeLayout(false);
        }

        #endregion
        public PictureBox BtnRotation;
        public Button BtnAuto;
        public Button BtnNext;
    }
}