using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookSystem
{
    public partial class frmAdmin : Form
    {
        SqlConnectionClass classcon;
        SqlCommand cmd;
        public frmAdmin()
        {
            InitializeComponent();
            classcon = new SqlConnectionClass();
        }

        private void lblAddbook_Click(object sender, EventArgs e)
        {
            frmAddBook frmAddBook = new frmAddBook();
            frmAddBook.ShowDialog();
            this.Hide();
        }

        //search button modified
        private void LoadBooks(string searchQuery = "")
        {
            try
            {
                using (SqlConnection conn = classcon.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT book_title, author, volume, quantity FROM Books";

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " WHERE CONCAT(book_title, author, volume, quantity) LIKE @search";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + searchQuery + "%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvBooks.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void lblLogout_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            this.Close();
            frmLogin.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text.Trim();
            LoadBooks(query);
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void lblRefresh_Click(object sender, EventArgs e)
        {

        }

        private void lblRemoveBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0) 
            {
                string bkname = (string)dgvBooks.SelectedRows[0].Cells[0].Value;
                DialogResult result = MessageBox.Show("Are you sure you want to remove the book?", "Remove Book", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes) 
                {
                    string query = "DELETE FROM books WHERE book_title = @bkname";
                    
                    using (SqlConnection con = classcon.GetConnection()) 
                    {
                        con.Open();
                        using (cmd = new SqlCommand(query, con)) 
                        {
                            cmd.Parameters.AddWithValue("@bkname", bkname);
                            int Rows = cmd.ExecuteNonQuery();

                            if (Rows > 0)
                            {
                                // Remove the row from the DataGridView
                                dgvBooks.Rows.RemoveAt(dgvBooks.SelectedRows[0].Index);
                                MessageBox.Show("Book removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to remove the book from the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close?", "Booklat", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        // Search function modified
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string query = txtSearch.Text.Trim();
            LoadBooks(query);
        }
    }
}
