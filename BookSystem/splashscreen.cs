using System;
using System.Drawing;
using System.Windows.Forms;

namespace BookSystem
{
    public partial class splashscreen : Form
    {

        public splashscreen()
        {
            InitializeComponent();

            this.progressBar = new CustomProgressBar
            {
                Location = new Point(150, 400),
                Size = new Size(400, 20),
                Maximum = 100,
                BarColor = Color.LightBlue
            };

            this.Controls.Add(this.progressBar);
            this.progressBar.BringToFront();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            progressBar.Increment(3);


            if (progressBar.Value >= progressBar.Maximum)
            {
                timer1.Stop();
                frmLogin frmLogin = new frmLogin();
                frmLogin.Show();
                this.Hide();
            }
        }

        //Moving panel
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private void splashscreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //check if the click was on the title bar
                if (e.Clicks == 1 && e.Y <= this.Height && e.Y >= 0)
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
        }
    }
}