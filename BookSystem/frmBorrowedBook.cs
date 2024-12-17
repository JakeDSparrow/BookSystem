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
    public partial class frmBorrowedBook : Form
    {

        SqlConnectionClass classcon;

        public frmBorrowedBook()
        {
            InitializeComponent();
            classcon = new SqlConnectionClass();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {  
           this.Close();
        }

        private void btn_Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void LoadBorrowedBooks()
        {
            string query = "SELECT bookid, booktitle, genre, volume, quantity, borrow_date, username FROM borrowedbooks";

            try
            {
                using (SqlConnection conn = classcon.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dgvBorrowedBooks.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmBorrowedBook_Load(object sender, EventArgs e)
        {
            LoadBorrowedBooks();
        }
    }
}
