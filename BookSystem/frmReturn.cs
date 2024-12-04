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
    public partial class frmReturn : Form
    {
        private string _username;
        public frmReturn(string username)
        {
            InitializeComponent();
            _username = username;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            //FUNCTION FOR UPDATE
        }
    }
}
