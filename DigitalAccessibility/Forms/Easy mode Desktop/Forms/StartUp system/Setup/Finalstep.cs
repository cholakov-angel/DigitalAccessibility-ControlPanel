using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace Easy_mode_Desktop
{
    public partial class Finalstep : Form
    {
        public Finalstep()
        {
            InitializeComponent();
        } // Finalstep

        private async void button2_Click(object sender, EventArgs e)
        {
            // Задаване ключ за "Registry", чрез който ДД да се стартира автоматично при старт на компютъра
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("Digital accessibility", Application.ExecutablePath.ToString());
            
            // Извличане на имейл настройките от базата данни
            try
            {
                var emailSettings = await WebApiCommunicator.GetEmail(Properties.Settings.Default.UserId, Properties.Settings.Default.AdministratorId);
                Properties.Settings.Default.Mail = emailSettings.email;
                Properties.Settings.Default.Password = emailSettings.password;
                Properties.Settings.Default.Save();
            }
            catch
            {
                MessageBox.Show("Настъпи грешка в извличането на данните!");
                return;
            }
            // Задаване на основен режим на работа - за незрящи
            Properties.Settings.Default.setup = "Режим за незрящи";
            Properties.Settings.Default.Save();

            // Отваряне на 4 прозорец (Form4) - основният за режима
            Homeblind f4 = new Homeblind();

            f4.Show();

            // Затваряне на настоящия прозорец
            Close();
        } // button2_Click
    } // Finalstep
}
