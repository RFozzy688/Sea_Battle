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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.one_field;
            ClientSize = new Size(1024, 603);
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

        private PictureBox BtnRotation;
    }
}