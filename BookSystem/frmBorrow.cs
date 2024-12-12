using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BookSystem
{
    public partial class frmBorrow : Form
    {
        private string _username;
        private string booktitle, author;
        private int volumenum;
        private DateTime borrowDate;
        SqlConnectionClass classcon = new SqlConnectionClass();

        public frmBorrow(string username)
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
                        dgvBorrowBooks.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BorrowBook(string username)
        {
            //Check if a book is selected in the DataGridView
            if (dgvBorrowBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to borrow.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Retrieve the selected book details from the DataGridView
            DataGridViewRow selectedRow = dgvBorrowBooks.SelectedRows[0];

            //Validate if the row contains valid data
            if (selectedRow.Cells["bookid"].Value == null ||
                selectedRow.Cells["booktitle"].Value == null ||
                selectedRow.Cells["author"].Value == null ||
                selectedRow.Cells["genre"].Value == null ||
                selectedRow.Cells["volume"].Value == null)
            {
                MessageBox.Show("The selected row contains invalid data. Please select a valid book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string bookID = selectedRow.Cells["bookid"].Value.ToString();
            string bookTitle = selectedRow.Cells["booktitle"].Value.ToString();
            string author = selectedRow.Cells["author"].Value.ToString();
            string genre = selectedRow.Cells["genre"].Value.ToString();
            string volume = selectedRow.Cells["volume"].Value.ToString();

            DateTime borrowDate = dtpBorrow.Value; //Get the borrowing date from the DateTimePicker

            try
            {
                using (SqlConnection conn = classcon.GetConnection())
                {
                    conn.Open();

                    //Check if the book is available
                    string findBookQuery = "SELECT status FROM books WHERE bookid = @bookid AND status = 'Available'";
                    using (SqlCommand findBookCmd = new SqlCommand(findBookQuery, conn))
                    {
                        findBookCmd.Parameters.AddWithValue("@bookid", bookID);

                        object status = findBookCmd.ExecuteScalar();
                        if (status == null || status.ToString() != "Available")
                        {
                            MessageBox.Show("The selected book is not available for borrowing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    //Insert into the borrowedbooks table
                    string insertBorrowedQuery = "INSERT INTO borrowedbooks (bookid, booktitle, genre, volume, borrow_date, username) " +
                                                 "VALUES (@bookid, @booktitle, @genre, @volume, @borrow_date, @username)";

                    using (SqlCommand insertBorrowCmd = new SqlCommand(insertBorrowedQuery, conn))
                    {
                        insertBorrowCmd.Parameters.AddWithValue("@bookid", bookID);
                        insertBorrowCmd.Parameters.AddWithValue("@booktitle", bookTitle);
                        insertBorrowCmd.Parameters.AddWithValue("@genre", genre);
                        insertBorrowCmd.Parameters.AddWithValue("@volume", volume);
                        insertBorrowCmd.Parameters.AddWithValue("@borrow_date", borrowDate);
                        insertBorrowCmd.Parameters.AddWithValue("@username", username);

                        insertBorrowCmd.ExecuteNonQuery();
                    }

                    //Update the status of the book in the books table
                    string updateBookStatusQuery = "UPDATE books SET status = 'Borrowed' WHERE bookid = @bookid";
                    using (SqlCommand updateBookCmd = new SqlCommand(updateBookStatusQuery, conn))
                    {
                        updateBookCmd.Parameters.AddWithValue("@bookid", bookID);
                        updateBookCmd.ExecuteNonQuery();
                    }

                    //Insert into user_history table
                    string insertHistoryQuery = "INSERT INTO user_history (username, bookid, booktitle, borrow_date, genre, status) " +
                                                 "VALUES (@username, @bookid, @booktitle, @borrow_date, @genre, 'Pending')";

                    using (SqlCommand insertHistoryCmd = new SqlCommand(insertHistoryQuery, conn))
                    {
                        insertHistoryCmd.Parameters.AddWithValue("@username", username);
                        insertHistoryCmd.Parameters.AddWithValue("@bookid", bookID);
                        insertHistoryCmd.Parameters.AddWithValue("@booktitle", bookTitle);
                        insertHistoryCmd.Parameters.AddWithValue("@borrow_date", borrowDate);
                        insertHistoryCmd.Parameters.AddWithValue("@genre", genre);
                        insertHistoryCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Book borrowed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Reload the available books list
                    LoadAvailableBooks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadAvailableBooks() 
        {
            SqlConnection connection = classcon.GetConnection();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM books WHERE status = 'Available'", connection);
            DataTable dataTable = new DataTable();

            try
            {
                if (string.IsNullOrWhiteSpace(_username))
                {
                    MessageBox.Show("Username is null or empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dataAdapter.SelectCommand.Parameters.AddWithValue("@username", _username);

                connection.Open();
                int rowsAffected = dataAdapter.Fill(dataTable);

                if (rowsAffected == 0)
                {
                    MessageBox.Show("You have no borrowed book/s.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dgvBorrowBooks.DataSource = dataTable;
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

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            BorrowBook(_username);
        }

        //nadouble click ni chloe tong mga to
        private void label2_Click(object sender, EventArgs e)
        {

        }

        //paremove kuys
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            UserDashboard dashboard = new UserDashboard(_username);
            this.Close();
            dashboard.Show();
        }

        private void frmBorrow_Load(object sender, EventArgs e)
        {
            LoadAvailableBooks();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close?", "Booklat", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                UserDashboard uDB = new UserDashboard(_username);
                this.Close();
                uDB.Show();
            }
        }

        private void txtBooktitle_TextChanged(object sender, EventArgs e)
        {
            string query = txtBb.Text.Trim();
            LoadBooks(query);
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
