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
            // Trimmed input values
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confpass = txtConfirmPass.Text.Trim();

            SqlConnectionClass sqlConnectionClass = new SqlConnectionClass();
            using (SqlConnection cn = sqlConnectionClass.GetConnection())
            {
                try
                {
                    cn.Open();

                    // Checking if fields are empty or contain only spaces
                    if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confpass))
                    {
                        MessageBox.Show("Fields cannot be empty or contain only whitespace. Please enter valid values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Checking if passwords match
                    if (password != confpass)
                    {
                        MessageBox.Show("Passwords do not match. Please confirm your password again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Check if the username is valid (no spaces or special characters)
                    if (!IsValidUsername(username))
                    {
                        MessageBox.Show("Username must not contain spaces or special characters.", "Invalid Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Check if the user already exists
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, cn))
                    {
                        cmdCheck.Parameters.AddWithValue("@username", username);
                        int userCount = (int)cmdCheck.ExecuteScalar();

                        if (userCount > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose another username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Insert the new user
                    string insertQuery = "INSERT INTO users (username, password) VALUES (@username, @password)";
                    using (SqlCommand cmdInsert = new SqlCommand(insertQuery, cn))
                    {
                        cmdInsert.Parameters.AddWithValue("@username", username);
                        cmdInsert.Parameters.AddWithValue("@password", password);

                        int result = cmdInsert.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Signup successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            frmLogin frmLogin = new frmLogin();
                            frmLogin.Show();
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
 
        private bool IsValidUsername(string username)
        {
            // Username must not contain spaces or special characters
            return username.All(char.IsLetterOrDigit);
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            //SIGN UP method
            Signup();
        }

    }
}

