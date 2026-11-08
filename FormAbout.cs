using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColoriResistenze
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            Form1 frmPrincipale = new Form1();
            this.Text = frmPrincipale.strTitolo + " - About";
        }

        private void Btn_Chiudi_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
