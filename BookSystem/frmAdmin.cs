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

        //Moving panel
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]

        private static extern bool ReleaseCapture();


        private void lblAddbook_Click(object sender, EventArgs e)
        {
            frmAddBook frmAddBook = new frmAddBook();
            this.Hide();
            frmAddBook.ShowDialog();
        }

        //search button modified
        private void LoadBooks(string searchQuery = "", string orderByColumn = "bookid", string sortDirection = "ASC")
        {
            try
            {
                using (SqlConnection conn = classcon.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT * FROM Books";

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " WHERE CONCAT(bookid, booktitle, author, genre, volume, quantity, status) LIKE @search";
                    }

                    query += $" ORDER BY {orderByColumn} {sortDirection}";//added by chatgpt, unfunctional

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

        private void RemoveBook() 
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                string bookId = dgvBooks.SelectedRows[0].Cells["bookid"].Value.ToString();
                string bookTitle = dgvBooks.SelectedRows[0].Cells["booktitle"].Value.ToString();
                string genre = dgvBooks.SelectedRows[0].Cells["genre"].Value.ToString();
                string author = dgvBooks.SelectedRows[0].Cells["author"].Value.ToString();
                int volume = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["volume"].Value);
                int quantity = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["quantity"].Value);

                DialogResult result = MessageBox.Show("Are you sure you want to archive this book?", "Archive Book", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                   
                    string archiveQuery = @"
                        INSERT INTO archive (bookid, booktitle, genre, volume, quantity, username, borrow_date, return_date) 
                        VALUES (@bookid, @booktitle, @genre, @volume, @quantity, @username, @borrow_date, @return_date)";
                    
                    string updateQuery = "UPDATE books SET status = @status WHERE bookid = @bookid";
                    
                    using (SqlConnection con = classcon.GetConnection())
                    {
                        con.Open();

                        using (SqlTransaction transaction = con.BeginTransaction())
                        {
                            try
                            {
                                using (SqlCommand archiveCmd = new SqlCommand(archiveQuery, con, transaction))
                                {
                                    archiveCmd.Parameters.AddWithValue("@bookid", bookId);
                                    archiveCmd.Parameters.AddWithValue("@booktitle", bookTitle);
                                    archiveCmd.Parameters.AddWithValue("@genre", genre);
                                    archiveCmd.Parameters.AddWithValue("@volume", volume);
                                    archiveCmd.Parameters.AddWithValue("@quantity", quantity);
                                    //added by chatgpt, kapag may nanghiram na with set the user name and the dates
                                    archiveCmd.Parameters.AddWithValue("@username", DBNull.Value); // Set NULL for now
                                    archiveCmd.Parameters.AddWithValue("@borrow_date", DBNull.Value); // Set NULL for now
                                    archiveCmd.Parameters.AddWithValue("@return_date", DBNull.Value); // Set NULL for now

                                    archiveCmd.ExecuteNonQuery();
                                }

                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, con, transaction))
                                {
                                    updateCmd.Parameters.AddWithValue("@status", "Archived");
                                    updateCmd.Parameters.AddWithValue("@bookid", bookId);

                                    int rowsAffected = updateCmd.ExecuteNonQuery();
                                    if (rowsAffected > 0)
                                    {
                                        dgvBooks.SelectedRows[0].Cells["status"].Value = "Archived";
                                        MessageBox.Show("Book archived successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        throw new Exception("Failed to update the book status in the database.");
                                    }
                                }

                                
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                
                                transaction.Rollback();
                                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to archive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void lblRemoveBook_Click(object sender, EventArgs e)
        {
            RemoveBook();
        }

        // Search function modified
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string query = txtSearch.Text.Trim();
            LoadBooks(query);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRefresh();
        }
        //refresh to be cont.
        public void LoadRefresh()
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-814NNKN;Initial Catalog=BookSystemDB;Integrated Security=True;Encrypt=False");
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Books WHERE status = 'Available'", connection);
            DataTable dataTable = new DataTable();

            try
            {
                // Open connection and fill the DataTable
                connection.Open();
                dataAdapter.Fill(dataTable);

                // Set the DataGridView's data source to the DataTable
                dgvBooks.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
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
