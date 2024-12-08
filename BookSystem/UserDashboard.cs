using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookSystem
{
    public partial class UserDashboard : Form
    {

        private string _username;
        private string Username;

        public UserDashboard(string username)
        {
            InitializeComponent();
            _username = username;
        }
        
        //Moving panel
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]

        private static extern bool ReleaseCapture();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmReturn returnbook = new frmReturn(_username);
            this.Close();
            returnbook.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Booklat", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                this.Close();
                frmLogin.ShowDialog();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmBorrow frmBorrow = new frmBorrow(_username);
            this.Hide();
            frmBorrow.Show();
        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {
            labelWelcome.Text = $"Welcome, {_username}!";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close?", "Booklat", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                System.Environment.Exit(0);
            }
        }

        private void btn_Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //moving panel function
        private void panel2_MouseDown(object sender, MouseEventArgs e)
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
