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
    public partial class frmArchive : Form
    {
        SqlConnectionClass classcon = new SqlConnectionClass();
        public frmArchive()
        {
            InitializeComponent();
        }

        //Moving panel
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]

        private static extern bool ReleaseCapture();


        private void btnClose_Click(object sender, EventArgs e)
        {
            frmAdmin frmAdmin = new frmAdmin(); 
            this.Close();
            frmAdmin.Show();
        }

        private void Archives()
        {
            SqlConnection connection = classcon.GetConnection();
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();

                //  Retrieves archived books from the "books" table
                string selectArchivedBooksQuery = "SELECT bookid, booktitle, author, volume FROM books WHERE status = 'Archived'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectArchivedBooksQuery, connection);

                // Fills the DataTable with the archived books
                int rowsAffected = dataAdapter.Fill(dataTable);

                if (rowsAffected == 0)
                {
                    MessageBox.Show("No archived books found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                /*// Insert these records into the "archives" table
                string insertArchivesQuery = @"
                INSERT INTO archives (bookid, booktitle, author, volume)
                SELECT bookid, booktitle, author, volume
                FROM books
                WHERE status = 'Archived'
                AND NOT EXISTS (
                SELECT 1 FROM archives WHERE archives.bookid = books.bookid
                )";

                using (SqlCommand insertCommand = new SqlCommand(insertArchivesQuery, connection))
                {
                    int insertedRows = insertCommand.ExecuteNonQuery();

                    if (insertedRows > 0)
                    {
                        MessageBox.Show($"{insertedRows} new archived book(s) added to the archives.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }*/

                // Display data from the "archives" table in the DataGridView
                string selectArchivesQuery = "SELECT * FROM archives";
                SqlDataAdapter archivesAdapter = new SqlDataAdapter(selectArchivesQuery, connection);
                DataTable archivesTable = new DataTable();
                archivesAdapter.Fill(archivesTable);

                dgvArchives.DataSource = archivesTable;
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

        private void frmArchive_Load(object sender, EventArgs e)
        {
            Archives();
        }

        private void DeleteArchives()
        {
            try
            {
                using (SqlConnection conn = classcon.GetConnection())
                {
                    conn.Open();

                    string deleteQuery = "DELETE FROM archives"; // Delete all records from the archives table

                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                    {
                        // Execute the delete query
                        deleteCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("All archival records have been deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the DataGridView inline after deletion
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM archives", conn))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dgvArchives.DataSource = dataTable; // Update the DataGridView to reflect changes
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the archives?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                DeleteArchives();
            }
        }

        private void frmArchive_MouseDown(object sender, MouseEventArgs e)
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
