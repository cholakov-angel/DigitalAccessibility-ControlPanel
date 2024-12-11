using Easy_mode_Desktop.Forms.StartUp_system.Setup;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_mode_Desktop
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private async void Loading_Load(object sender, EventArgs e)
        {
            mode_Selected_txt.Text = Properties.Settings.Default.setup;
            await this.Start();

            // Скриване на настоящия прозорец
            this.Hide();
        }

      private async Task Start()
        {
            // При стойност "Първоначална настройка" на 2 маркер за текст се стартира
            // 17 прозорец (първоначална настройка)
            if (Properties.Settings.Default.setup == "Първоначална настройка")
            {
                Welcome welcomeForm = new Welcome();

                welcomeForm.Show();
            }
            else
            {
                // При установяване на стойност "Режим за незрящи" на 2 маркер за текст
                // се стартира 4 прозорец(начален за режим за незрящи)
                if (Properties.Settings.Default.setup == "Режим за незрящи")
                {
                    // await GetEmail();

                    Homeblind f4 = new Homeblind();

                    f4.Show();

                    // Спиране на настоящия таймер
                    timer1.Stop();

                    // Скриване на настоящия прозорец
                    Hide();
                }

                // При установяване на стойност "Режим за комуникация" на 2 маркер за текст
                // се стартира 8 прозорец(начален за режим за комуникация)
                if (Properties.Settings.Default.setup == "Режим за комуникация")
                {

                    Form8 f8 = new Form8();

                    f8.Show();

                    // Спиране на настоящия таймер
                    timer1.Stop();

                    // Скриване на настоящия прозорец
                    Hide();
                }
            }
        }

        private static async Task GetEmail()
        {
            var emailSettings = await WebApiCommunicator.GetEmail(Properties.Settings.Default.UserId, Properties.Settings.Default.AdministratorId);
            Properties.Settings.Default.Mail = emailSettings.email;
            Properties.Settings.Default.Password = emailSettings.password;
        }
    }
}

