using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_mode_Desktop.Forms.StartUp_system.Setup
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        } // Welcome

        private async void StartButton_Click(object sender, EventArgs e)
        {
            LicenseEnter licenseEnter = new LicenseEnter();
            licenseEnter.Show();

            this.Close();
        } // StartButton_Click
    } // Welcome
}
