using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_mode_Desktop
{
    public partial class SettingsConfirm : Form
    {
        // Дефиниране на аудиозаписи с инструкции
        SoundPlayer soundPlayer = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\admin.wav");
        SoundPlayer soundPlayerexit = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\admin1.wav");
        SoundPlayer soundPlayerenter = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\admin2.wav");
        SoundPlayer soundPlayererror = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\commerror.wav");
        public SettingsConfirm()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                switch (command_txt.Text)
                {
                    case "1":
                        // Пускане на запис с инструкции за потребителя
                        soundPlayerenter.Play();

                        // Отваряне на прозореца с настройки
                        Settings settings = new Settings();
                        settings.Show();

                        // Затваряне на настоящият прозорец
                        Close();

                        break;

                    case "10":
                        // Пускане на запис с инструкции за потребителя
                        soundPlayerexit.Play();

                        // Затваряне на програмата
                        Close();

                        break;

                    default:
                        // Пускане на запис с инструкции за потребителя
                        soundPlayererror.Play();

                        // Изчистване на настоящото текстово поле
                        command_txt.Clear();

                        break;
                }
            }
        }

        private void SettingsConfirm_Load(object sender, EventArgs e)
        {
            // Пускане на запис с инструкции за потребителя
            soundPlayer.Play();
        }
    }
}
