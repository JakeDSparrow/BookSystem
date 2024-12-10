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

            string[] genres = {
                "General Works, Computer Science & Information",
                "Philosophy & Psychology",
                "Religion",
                "Social sciences",
                "Language",
                "Science",
                "Technology",
                "Arts & recreation",
                "Literature",
                "History & Geography"
            };

            foreach (string genre in genres) 
            {
                cmbGenre.Items.Add(genre);
            }
        }

        //Moving panel
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]

        private static extern bool ReleaseCapture();

        private void clearStuff()
        {
            txtBookID.Clear();
            txtAuthor.Clear();
            txtBooktitle.Clear();
            txtQuantity.Clear();
            cmbGenre.SelectedIndex = -1;
            txtVolume.Clear();
        }

        //method for getting the genre classification based on Dewey Decimal System
        private string GetGenre(int bookID)
        {
            if (bookID >= 0 && bookID <= 99)
                return "General Works, Computer Science & Information";
            else if (bookID >= 100 && bookID <= 199)
                return "Philosophy & Psychology";
            else if (bookID >= 200 && bookID <= 299)
                return "Religion";
            else if (bookID >= 300 && bookID <= 399)
                return "Social sciences";
            else if (bookID >= 400 && bookID <= 499)
                return "Language";
            else if (bookID >= 500 && bookID <= 599)
                return "Science";
            else if (bookID >= 600 && bookID <= 699)
                return "Technology";
            else if (bookID >= 700 && bookID <= 799)
                return "Arts & recreation";
            else if (bookID >= 800 && bookID <= 899)
                return "Literature";
            else if (bookID >= 900 && bookID <= 999)
                return "History & Geography";
            else
                return "Invalid Book ID";
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
        }

        private void btn_Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //Moving panel
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

        private void btnPlus_Click(object sender, EventArgs e)
        {
            int quantity = int.Parse(txtQuantity.Text);
            if (quantity < 10)
            {
                txtQuantity.Text = (quantity + 1).ToString();
            }
            else
            {
                MessageBox.Show("Maximum quantity is 10", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            int quantity = int.Parse(txtQuantity.Text);
            if (quantity > 0)
            {
                txtQuantity.Text = (quantity - 1).ToString();
            }
            else
            {
                MessageBox.Show("Quantity cannot be less than 0", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmAddBook_Load(object sender, EventArgs e)
        {
            txtQuantity.Text = "0";
            txtBookID.Text = "000";
            cmbGenre.Text = "General Works, Computer Science & Information";
        }

        //Allow only digits and control keys (e.g., backspace)
        private void txtBookID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; //Ignoring the key press
            }
        }

        private void txtBookID_Leave(object sender, EventArgs e)
        {

            //Check if the field is empty
            if (string.IsNullOrWhiteSpace(txtBookID.Text))
            {
                MessageBox.Show("Book ID cannot be empty.", "Booklat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBookID.Focus();
                txtBookID.Text = "000";
                return;
            }

            //Ensure the Book ID is numeric and within the valid range
            if (int.TryParse(txtBookID.Text, out int bookID) && bookID >= 0 && bookID <= 999)
            {
                //Get the genre and set it in the Genre dropdown or label
                string genre = GetGenre(bookID);
                cmbGenre.Text = genre;
            }
            else
            {
                MessageBox.Show("Please enter a valid Book ID (000-999).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBookID.Text = "000";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmAdmin admin = new frmAdmin();
            this.Close();
            admin.Show();
        }
    }

}

