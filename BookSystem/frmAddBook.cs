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

        private string genres, basebookId;

        public frmAddBook()
        {
            InitializeComponent();
            conn = new SqlConnectionClass();
            cmd = new SqlCommand();

            string[] genres = {"Fantasy","Horror","Science Fiction","Educational"};

            foreach (string genre in genres) 
            {
                cmbGenre.Items.Add(genre);
            }
        }

        private void clearStuff()
        {
            txtBookID.Clear();
            txtAuthor.Clear();
            txtBooktitle.Clear();
            txtQuantity.Clear();
            cmbGenre.SelectedIndex = -1;
            txtVolume.Clear();
        }

        private void AddBook()
        {
            if (string.IsNullOrEmpty(txtBookID.Text) ||
                string.IsNullOrEmpty(txtBooktitle.Text) ||
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
            catch (FormatException)
            {
                MessageBox.Show("Volume and Quantity must be valid numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string baseBookID = txtBookID.Text;

            //updated query
            string query = "INSERT INTO books (bookid, booktitle, author, genre, volume, quantity) VALUES (@bookid, @booktitle, @author, @genre, @volume, 1)";

            try
            {
                using (SqlConnection connection = conn.GetConnection())
                {
                    connection.Open();

                    for (int i = 1; i <= quantity; i++) // Loops to handle each "unit" of each book
                    {
                        string uniqueBookID = $"{baseBookID}-{i}"; // generate unique bookid 

                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@bookid", uniqueBookID);
                            cmd.Parameters.AddWithValue("@booktitle", txtBooktitle.Text);
                            cmd.Parameters.AddWithValue("@author", txtAuthor.Text);
                            cmd.Parameters.AddWithValue("@genre", cmbGenre.SelectedItem?.ToString() ?? "Unknown");
                            cmd.Parameters.AddWithValue("@volume", volume);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Books added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearStuff();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddBook();
            frmAdmin frmAdmin = new frmAdmin();
            this.Close();
            frmAdmin.Show();
        }

        private void btn_Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmAdmin admin = new frmAdmin();
            this.Close();
            admin.Show();
        }
    }

}

