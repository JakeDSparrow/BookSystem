using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookSystem
{
    public partial class frmAddBook : Form
    {
        public frmAddBook()
        {
            InitializeComponent();
        }

        public string booktitle, author;
        public int quantity, volumenum;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            /*booktitle = txtBooktitle.Text;
            author = txtAuthor.Text;
            quantity = int.Parse(txtQuantity.Text);
            volumenum = int.Parse(txtVolume.Text*/

            MessageBox.Show("Book added successfully.");
            frmAdmin frmAdmin = new frmAdmin();
            frmAdmin.ShowDialog();
        }
    }
}
