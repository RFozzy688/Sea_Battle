using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    internal class EmbededFont
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font _btnFontPressed;
        Font _btnFontReleased;
        Font BattleResultFont { get; }
        public Font GetBtnFontPressed() { return _btnFontPressed; }
        public Font GetBtnFontReleased() { return _btnFontReleased; }

        public EmbededFont()
        {
            byte[] fontData = Properties.Resources.RubikVinyl_Regular;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.RubikVinyl_Regular.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.RubikVinyl_Regular.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            //_btnFontPressed = new Font(fonts.Families[0], 26.0F, FontStyle.Regular, GraphicsUnit.Point);
            //_btnFontReleased = new Font(fonts.Families[0], 30.0F, FontStyle.Regular, GraphicsUnit.Point);
            //BattleResultFont = new Font(fonts.Families[0], 60.0F, FontStyle.Bold, GraphicsUnit.Point);
        }
        public Font CreateFont(float size, FontStyle style)
        {
            return new Font(fonts.Families[0], size, style, GraphicsUnit.Point);
        }
    }
}
