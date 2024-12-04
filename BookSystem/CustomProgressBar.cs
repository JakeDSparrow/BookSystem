using System;
using System.Drawing;
using System.Windows.Forms;

namespace BookSystem
{
    public class CustomProgressBar : ProgressBar
    {
        public Color BarColor { get; set; } = Color.Green;

        public CustomProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = this.ClientRectangle;

            using (Brush brush = new SolidBrush(this.BarColor))
            {
                int fillWidth = (int)(rect.Width * ((double)this.Value / this.Maximum));
                e.Graphics.FillRectangle(brush, 0, 0, fillWidth, rect.Height);
            }

            e.Graphics.DrawRectangle(Pens.Black, 0, 0, rect.Width - 1, rect.Height - 1);
        }
    }
}
