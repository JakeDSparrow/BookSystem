using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookSystem
{
    public partial class frmLogin : Form
    {

        public string username, password;

        public frmLogin()
        {
            InitializeComponent();
        }

        //for login button...

        private void Login()
        {
            username = txtUsername.Text;
            password = txtPassword.Text;


            if (IsValidUser(username, password))
            {
                if (username == "admin01" || username == "ADMIN01" || username == "admin")
                {
                    frmAdmin frmadmin = new frmAdmin();
                    this.Hide();
                    frmadmin.ShowDialog();
                }
                else
                {
                    UserDashboard userDashboard = new UserDashboard(username); // Pass the username
                    MessageBox.Show("Welcome, " + username, "Welcome", MessageBoxButtons.OK);
                    this.Hide();
                    userDashboard.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Booklat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        //for signup button...
        private void lblSignup_Click(object sender, EventArgs e)
        {
            frmSignUp frmSignUp = new frmSignUp();
            frmSignUp.ShowDialog();
            this.Hide();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close?", "Booklat", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                System.Environment.Exit(0);
            }
        }

        private bool IsValidUser(string admin, string password)
        {
            SqlConnectionClass dbConnection = new SqlConnectionClass();

            string adminQuery = "SELECT COUNT(*) FROM admin WHERE admin = @username AND password = @password";
            string userQuery = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";

            try
            {
                using (SqlConnection connection = dbConnection.GetConnection())
                {
                    connection.Open();

                
                    using (SqlCommand adminCommand = new SqlCommand(adminQuery, connection))
                    {
                        adminCommand.Parameters.AddWithValue("@username", username);
                        adminCommand.Parameters.AddWithValue("@password", password);

                        int adminCount = (int)adminCommand.ExecuteScalar();
                        if (adminCount > 0)
                        {
                            return true;
                        }
                    }

                    using (SqlCommand userCommand = new SqlCommand(userQuery, connection))
                    {
                        userCommand.Parameters.AddWithValue("@username", username);
                        userCommand.Parameters.AddWithValue("@password", password);

                        int userCount = (int)userCommand.ExecuteScalar();
                        if (userCount > 0)
                        {
                            return true; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while validating user: " + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            return false;
        }
    }
}
