using System;
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

        private void BorrowBook(string username)
        {
            // Retrieve inputs
            string booktitle = txtBooktitle.Text;
            string author = txtAuthor.Text;
            string volume = cmbVolume.Text; // Retrieve the selected volume as text
            DateTime borrowDate = dtpBorrow.Value;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(booktitle) ||
                string.IsNullOrWhiteSpace(author) ||
                string.IsNullOrWhiteSpace(volume))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(volume, out int volumeNumber) || volumeNumber <= 0 || volumeNumber > 20)
            {
                MessageBox.Show("Selected volume is not valid. Please choose a value between 1 and 20.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = classcon.GetConnection())
                {
                    conn.Open();

                    string bookID = null;
                    string genre = null;
                    string status = null;

                    // SQL queries
                    string findbookquery = "SELECT bookid, genre, status FROM books WHERE booktitle = @booktitle AND author = @author AND volume = @volume AND status = 'Available'";

                    string insertborrowed = "INSERT INTO borrowedbooks (bookid, booktitle, genre, volume, borrow_date, username) VALUES (@bookid, @booktitle, @genre, @volume, @borrow_date, @username)";

                    string updateBookStatus = "UPDATE books SET status = 'Borrowed' WHERE bookid = @bookid AND status = 'Available'";

                    // Find the book based on title, author, and volume
                    using (SqlCommand findBookCmd = new SqlCommand(findbookquery, conn))
                    {
                        findBookCmd.Parameters.AddWithValue("@booktitle", booktitle);
                        findBookCmd.Parameters.AddWithValue("@author", author);
                        findBookCmd.Parameters.AddWithValue("@volume", volumeNumber);

                        using (SqlDataReader reader = findBookCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("No matching book found or all copies are already borrowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            bookID = reader.GetString(0);
                            genre = reader.GetString(1);
                            status = reader.GetString(2);
                        }
                    }

                    // If a valid book copy was found, borrow it
                    if (!string.IsNullOrEmpty(bookID) && status == "Available")
                    {
                        // Insert the borrowed book record
                        using (SqlCommand insertBorrowCmd = new SqlCommand(insertborrowed, conn))
                        {
                            insertBorrowCmd.Parameters.AddWithValue("@bookid", bookID);
                            insertBorrowCmd.Parameters.AddWithValue("@booktitle", booktitle);
                            insertBorrowCmd.Parameters.AddWithValue("@genre", genre);
                            insertBorrowCmd.Parameters.AddWithValue("@volume", volumeNumber);
                            insertBorrowCmd.Parameters.AddWithValue("@borrow_date", borrowDate); // Use selected date
                            insertBorrowCmd.Parameters.AddWithValue("@username", username);

                            insertBorrowCmd.ExecuteNonQuery();
                        }

                        // Update the status of the specific book copy (bookid)
                        using (SqlCommand updateBookCmd = new SqlCommand(updateBookStatus, conn))
                        {
                            updateBookCmd.Parameters.AddWithValue("@bookid", bookID);
                            updateBookCmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Book borrowed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        string inserthistoryquery = @"INSERT INTO user_history (username, bookid, booktitle, borrow_date, genre, status) VALUES (@username, @bookid, @booktitle, @borrow_date, @genre, 'Pending');";

                        using (SqlCommand insertHistoryCmd = new SqlCommand(inserthistoryquery, conn))
                        {
                            insertHistoryCmd.Parameters.AddWithValue("@username", username);
                            insertHistoryCmd.Parameters.AddWithValue("@bookid", bookID);
                            insertHistoryCmd.Parameters.AddWithValue("@booktitle", booktitle);
                            insertHistoryCmd.Parameters.AddWithValue("@borrow_date", borrowDate);
                            insertHistoryCmd.Parameters.AddWithValue("@genre", genre);
                            insertHistoryCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show("The book is not available for borrowing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
