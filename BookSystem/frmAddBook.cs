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
    public partial class frmAddBook : Form
    {
        private SqlConnectionClass conn;
        private SqlCommand cmd;

        public frmAddBook()
        {
            InitializeComponent();
            conn = new SqlConnectionClass();
            cmd = new SqlCommand();
        }

        private void clearStuff()
        {
            txtAuthor.Clear();
            txtBooktitle.Clear();
            txtQuantity.Clear();
            txtVolume.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtBooktitle.Text) ||
                string.IsNullOrEmpty(txtAuthor.Text) ||
                string.IsNullOrEmpty(txtVolume.Text) ||
                string.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int volume, quantity; 
            
            try 
            {
                volume = int.Parse(txtVolume.Text);
                quantity = int.Parse(txtQuantity.Text);
            }
            catch(FormatException fe) 
            {
                MessageBox.Show(fe.Message);
            }

            string query = "INSERT INTO books (book_title, author, volume, quantity) VALUES (@book_title, @author, @volume, @quantity)";

            try
            {
                using (SqlConnection connection = conn.GetConnection())
                {
                    using (cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@book_title", txtBooktitle.Text);
                        cmd.Parameters.AddWithValue("@author", txtAuthor.Text);
                        cmd.Parameters.AddWithValue("@volume", int.Parse(txtVolume.Text));
                        cmd.Parameters.AddWithValue("@quantity", int.Parse(txtQuantity.Text));

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Book added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearStuff();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAdmin frmAdmin = new frmAdmin();
            this.Close();
            frmAdmin.Show();
        }
    }

}

