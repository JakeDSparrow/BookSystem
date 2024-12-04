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
    public partial class frmSignUp : Form
    {
        public frmSignUp()
        {
            InitializeComponent();  
        }

        SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;

        public void Signup()
        {
            SqlConnectionClass sqlConnectionClass = new SqlConnectionClass();
            using (SqlConnection cn = sqlConnectionClass.GetConnection())
            {
                try
                {
                    cn.Open();

                    // Checking if fields are empty
                    if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
                    {
                        MessageBox.Show("Please enter value in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Checks if the user already exists 
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, cn))
                    {
                        cmdCheck.Parameters.AddWithValue("@username", txtUsername.Text);
                        int userCount = (int)cmdCheck.ExecuteScalar();

                        if (userCount > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose another username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Inserts a new user
                    string insertQuery = "INSERT INTO users (username, password) VALUES (@username, @password)";
                    using (SqlCommand cmdInsert = new SqlCommand(insertQuery, cn))
                    {
                        cmdInsert.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmdInsert.Parameters.AddWithValue("@password", txtPassword.Text);

                        int result = cmdInsert.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Signup successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            frmLogin frmLogin = new frmLogin();
                            frmLogin.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("An error occurred while signing up. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnSignup_Click(object sender, EventArgs e)
        {
            Signup();
        }

    }
}

