using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_mode_Desktop
{
    public partial class Licence : Form
    {
        public Licence()
        {
            InitializeComponent();
        } // Licence

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        } // button2_Click

        private void button1_Click(object sender, EventArgs e)
        {
            Step2 step2 = new Step2();
            step2.Show();

            // Затваряне на настоящия прозорец
            this.Close();
        } // button1_Click

        private void Licence_Load(object sender, EventArgs e)
        {

        } // Licence_Load
    } // Licence
}
