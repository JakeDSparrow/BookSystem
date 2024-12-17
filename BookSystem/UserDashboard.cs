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
        private List<string> quotes;
        private Random random;
        public UserDashboard(string username)
        {
            InitializeComponent();
            _username = username;

            quotes = new List<string>
            {
                "\"A reader lives a thousand lives before he dies. The man who never reads lives only one.\" – George R.R. Martin",
                "\"Reading is essential for those who seek to rise above the ordinary.\" – Jim Rohn",
                "\"Books are a uniquely portable magic.\" – Stephen King",
                "\"Once you learn to read, you will be forever free.\" – Frederick Douglass",
                "\"A room without books is like a body without a soul.\" – Marcus Tullius Cicero",
                "\"There is no friend as loyal as a book.\" – Ernest Hemingway",
                "\"I have always imagined that paradise will be a kind of library.\" – Jorge Luis Borges",
                "\"Reading gives us someplace to go when we have to stay where we are.\" – Mason Cooley",
                "\"Today a reader, tomorrow a leader.\" – Margaret Fuller",
                "\"Reading is dreaming with open eyes.\" – Unknown"
            };
            random = new Random();

            timer1.Interval = 3000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
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
                frmLogin.Show();
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
                Application.Exit();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (quotes != null && quotes.Count > 0)
            {
                lblQuote.Text = quotes[random.Next(quotes.Count)]; //display a random quote
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmHistory history = new frmHistory(_username);
            this.Hide();
            history.Show();
        }

        private void UserDashboard_MouseDown(object sender, MouseEventArgs e)
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
