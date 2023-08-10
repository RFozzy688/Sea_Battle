namespace Sea_Battle
{
    partial class LoadScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _screen = new PictureBox();
            _progressBarTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)_screen).BeginInit();
            SuspendLayout();
            // 
            // _screen
            // 
            _screen.BackColor = Color.Transparent;
            _screen.Image = Properties.Resources.load_screen;
            _screen.Location = new Point(0, 0);
            _screen.Name = "_screen";
            _screen.Size = new Size(1024, 600);
            _screen.TabIndex = 0;
            _screen.TabStop = false;
            // 
            // _progressBarTimer
            // 
            _progressBarTimer.Enabled = true;
            _progressBarTimer.Tick += ProgressBarTimer;
            // 
            // LoadScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1024, 600);
            Controls.Add(_screen);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoadScreen";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoadScreen";
            TopMost = true;
            Paint += LoadScreen_Paint;
            ((System.ComponentModel.ISupportInitialize)_screen).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox _screen;
        private System.Windows.Forms.Timer _progressBarTimer;
    }
}