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
    }
}