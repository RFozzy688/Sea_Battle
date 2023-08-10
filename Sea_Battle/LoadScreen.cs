using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using

namespace Sea_Battle
{
    public partial class LoadScreen : Form
    {
        int _width;
        Bitmap _originalBitmap;
        Bitmap _stretchBitmap;
        MainForm _parent;
        public LoadScreen(MainForm parent)
        {
            InitializeComponent();

            _parent = parent;
            _screen.BringToFront();

            _originalBitmap = new Bitmap(Properties.Resources.progress_bar);
            _stretchBitmap = new Bitmap(1, 1);
        }

        private void ProgressBarTimer(object sender, EventArgs e)
        {
            _width += 10;
            Rectangle rectangle = new Rectangle(0, 0, _width, _originalBitmap.Height);

            _stretchBitmap = _originalBitmap.Clone(rectangle, PixelFormat.DontCare);
            Invalidate();

            if (_width == 540)
            {
                _progressBarTimer.Stop();
                this.Close();

                _parent.Visible = true;
            }
        }

        private void LoadScreen_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(_stretchBitmap, 250, 510);
        }
    }
}
