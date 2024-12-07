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
            this.Username = username;
        }

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
            MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            this.Close();
            frmLogin.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmBorrow frmBorrow = new frmBorrow();
            frmBorrow.Show();
        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {
            labelWelcome.Text = $"Welcome, {Username}!";
        }
    }
}
