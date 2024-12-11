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
    public partial class frmHistory : Form
    {
        private string _username;
        SqlConnectionClass classcon = new SqlConnectionClass();
        public frmHistory(string username)
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

        private void History(string username)
        {
            SqlConnection connection = classcon.GetConnection();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM archive WHERE username = @username", connection);
            DataTable dataTable = new DataTable();

            try
            {
                // Check if the username is valid
                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Username is null or empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Add username as parameter to the query
                dataAdapter.SelectCommand.Parameters.AddWithValue("@username", username);

                // Open connection and fill the DataTable
                connection.Open();
                int rowsAffected = dataAdapter.Fill(dataTable);

                // Check if there are any records found
                if (rowsAffected == 0)
                {
                    MessageBox.Show("You have no borrowing history.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Bind the data to the DataGridView
                dgvHistory.DataSource = dataTable;
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


        private void frmHistory_Load(object sender, EventArgs e)
        {

        }
    }
}
