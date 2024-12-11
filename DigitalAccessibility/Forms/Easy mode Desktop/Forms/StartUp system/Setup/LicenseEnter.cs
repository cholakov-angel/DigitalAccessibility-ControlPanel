using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_mode_Desktop.Forms.StartUp_system.Setup
{
    public partial class LicenseEnter : Form
    {
        public LicenseEnter()
        {
            InitializeComponent();
        } // LicenseEnter

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        } // CloseButton_Click

        private async Task ActivateButton_Click(object sender, EventArgs e)
        {
            
        } // ActivateButton_Click

        private void LicenseEnter_Load(object sender, EventArgs e)
        {

        } // LicenseEnter_Load

        private async void activateButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Извличане на идентификаторите на потребителя
                var result = await WebApiCommunicator.Activate(licenseKeyTextBox.Text, masterKeyTextBox.Text);

                // Запазване на идентификаторите на потребителя
                Properties.Settings.Default.UserId = result.Id;
                Properties.Settings.Default.AdministratorId = result.AdministratorId;
                Properties.Settings.Default.Save();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Невалиден потребител!");
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Настъпи грешка!");
                return;
            }

            Licence licence = new Licence();
            licence.Show();

            this.Close();
        }
    } // LicenseEnter
}
