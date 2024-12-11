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
    public partial class frmReturn : Form
    {
        private string _username;
        SqlConnectionClass classcon = new SqlConnectionClass();
        public frmReturn(string username)
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

        private void ReturnBook()
        {
            // Check if a book is selected in the DataGridView
            if (dgvBorrowedBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to return.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Retrieve the selected book details from the DataGridView
            DataGridViewRow selectedRow = dgvBorrowedBooks.SelectedRows[0];
            string bookID = selectedRow.Cells["bookid"].Value.ToString(); // Assuming 'bookid' is a column in the DataGridView
            string bookTitle = selectedRow.Cells["booktitle"].Value.ToString(); // Assuming 'booktitle' is a column in the DataGridView
            string genre = selectedRow.Cells["genre"].Value.ToString(); // Assuming 'genre' is a column in the DataGridView
            string username = selectedRow.Cells["username"].Value.ToString(); // Assuming 'username' is a column in the DataGridView

            try
            {
                using (SqlConnection conn = classcon.GetConnection())
                {
                    conn.Open();

                    // SQL query to update the status of the book copy to 'Available'
                    string updateBookStatusQuery = "UPDATE books SET status = 'Available' WHERE bookid = @bookid";

                    // SQL query to remove the record from the borrowed books table (optional: keep the record or just update)
                    string deleteBorrowedBookQuery = "DELETE FROM borrowedbooks WHERE bookid = @bookid AND username = @username";

                    // Update the book status to 'Available'
                    using (SqlCommand updateStatusCmd = new SqlCommand(updateBookStatusQuery, conn))
                    {
                        updateStatusCmd.Parameters.AddWithValue("@bookid", bookID);
                        updateStatusCmd.ExecuteNonQuery();
                    }

                    // Remove the borrowed book record (optional, depending on whether you want to keep a history or just mark as returned)
                    using (SqlCommand deleteBorrowedCmd = new SqlCommand(deleteBorrowedBookQuery, conn))
                    {
                        deleteBorrowedCmd.Parameters.AddWithValue("@bookid", bookID);
                        deleteBorrowedCmd.Parameters.AddWithValue("@username", username);
                        deleteBorrowedCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Book returned successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optionally, refresh the DataGridView to reflect the change (if necessary)
                    LoadBorrowedBooks(username); // Assuming you have a method to reload the DataGridView with borrowed books.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBorrowedBooks(string username)
        {
            SqlConnection connection = classcon.GetConnection();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM borrowedbooks WHERE username = @username", connection);
            DataTable dataTable = new DataTable();

            try
            {
                // Debugging: Check if username is correct
                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Username is null or empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Add parameter for username
                dataAdapter.SelectCommand.Parameters.AddWithValue("@username", username);

                // Open connection and fill the DataTable
                connection.Open();
                int rowsAffected = dataAdapter.Fill(dataTable);

                // Check if any rows were retrieved
                if (rowsAffected == 0)
                {
                    MessageBox.Show("No borrowed books found for this user.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Set the DataGridView's data source to the DataTable
                dgvBorrowedBooks.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            ReturnBook();
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UserDashboard dashboard = new UserDashboard(_username);
            this.Close();
            dashboard.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            UserDashboard dashboard = new UserDashboard(_username);
            this.Close();
            dashboard.Show();
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

        private void frmReturn_Load(object sender, EventArgs e)
        {
            LoadBorrowedBooks(_username);
        }
    }
}
