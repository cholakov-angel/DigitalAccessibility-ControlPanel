using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Speech.Synthesis;
using S22.Imap;
using System.Globalization;
using AudioSwitcher.AudioApi.CoreAudio;
using System.Diagnostics;
using Easy_mode_Desktop.Blind_mode.Volume_control;
using Easy_mode_Desktop.Keyboard_layout_changer;

namespace Easy_mode_Desktop
{
    
    public partial class Homeblind : Form
    {
        bool isPauseActivated = false;
        // Дефиниране на променлива, служеща за проверка при изключване на компютър
        int shutdown = 1;

        // Дефиниране на статичен член "form", който се използва в NewMessage()
        static Homeblind form;

        // Дефиниране на синтезатор на глас
        SpeechSynthesizer speech;

        // Дефиниране на предварително записаните аудио записи с инструкции с техните локации
        SoundPlayer soundPlayer = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\home.wav");
        SoundPlayer soundPlayernew = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\emailnew.wav");
        SoundPlayer soundPlayererror = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\commerror.wav");
        SoundPlayer soundPlayervolume = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\volume1.wav");
        SoundPlayer soundPlayervolume1 = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\volume2.wav");
        SoundPlayer soundPlayershutdown = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\shutdown.wav");
        SoundPlayer soundPlayershutdown1 = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\shutdown1.wav");

        public Homeblind()
        {
            // Статичния член "form" приема стойността на тази форма
            form=this;

            InitializeComponent();

            // Задаване на нова инстанция на класа SpeechSynthesizer като speech
            speech = new SpeechSynthesizer();
        }

        private void command_txt_KeyDown(object sender, KeyEventArgs e)
        {
            // При натискането на Enter, се извиква метода за отваряне на функционалностите
            if (e.KeyData == Keys.Enter)
            {
                OpenFunctionCommand();

                // Изчистване на настоящото текстово поле
                command_txt.Clear();
            }
        }

        private void OpenFunctionCommand()
        {
            Form formToOpen = null;
            switch (command_txt.Text)
            {
                case "0":
                    // Пускане на записа с първоначални инструкции и изчистване на настоящото поле
                    soundPlayer.Play();

                    break;


                case "1":
                    // При установяване на стойност "0" на настройката за изключване, се създава нов прозорец на текстовия редактор
                    if (shutdown == 1)
                    {
                        MessageBox.Show("Функцията не е налична от публичния GitHub!");
                    }
                    else
                    {
                        // Стартиране на процес за изключване на компютър
                        Process.Start("shutdown", "/s /t 0");
                    }

                    break;


                case "2":
                    // Създаване на нов прозорец за функцията "Файлов мениджър"
                    MessageBox.Show("Функцията не е налична от публичния GitHub!");

                    break;
                        

                case "3":
                    // Създаване на нов прозорец за функцията "Входяща поща"
                    MessageBox.Show("Функцията не е налична от публичния GitHub!");

                    break;


                case "4":
                    // Създаване на нов прозорец за функцията "Изпращане на имейл"
                    formToOpen = new Emailsend();

                    break;


                case "5":
                    // Спиране на изпълнението на записа с инструкции
                    soundPlayer.Stop();

                    // Избиране на предпочитания от потреб. глас и изговаряне на настоящата дата и час
                    speech.SelectVoice(Properties.Settings.Default.voice);         
                    speech.SpeakAsync(DateTime.Now.ToString());

                    break;

                case "6":
                    // Създаване на нов прозорец за функцията "PDF четец"
                    MessageBox.Show("Функцията не е налична от публичния GitHub!");

                    break;

                case "7":
                    // Създаване на нов прозорец за функцията "Музика"
                    MessageBox.Show("Функцията не е налична от публичния GitHub!");

                    break;

                case "8":
                    // Създаване на нов прозорец за функцията "Конткатна книга"
                    MessageBox.Show("Функцията не е налична от публичния GitHub!");

                    break;

                case "9":
                    // Създаване на нов прозорец за функцията "Интернет функционалности"
                    MessageBox.Show("Функцията не е налична от публичния GitHub!");

                    break;

                case "10":
                    if (shutdown == 1)
                    {
                        // Пускане на запис с инструкции за потребителя
                        soundPlayershutdown.Play();
                    }
                    else
                    {
                        // Пускане на запис с инструкции за потребителя
                        soundPlayershutdown1.Play();
                    }

                    // Задаване на обратната стойност на променливата за изключване на системата
                    shutdown = -shutdown;
                    break;

                case "11":
                    // Задаване на български език за въвеждане на текст
                    KeyboardLayoutSwitcher.Bulgarian();

                    break;

                case "12":
                    // Задаване на английски език за въвеждане на текст
                    KeyboardLayoutSwitcher.English();

                    break;

                case "13":
                    // Извършване на операцията по намаляване на звука
                    VolumeChanger.VolumeDown();

                    break;

                case "14":
                    // Извършване на операцията по увеличаване на звука
                    VolumeChanger.VolumeUp();

                    break;

                case "159":
                    // Създаване на нов прозорец за функцията "Настройки"
                    formToOpen = new SettingsConfirm();

                    break;

                case "-":
                    if (!isPauseActivated)
                    {
                        soundPlayer.Stop();
                        speech.Pause();
                    }
                    else
                    {
                        soundPlayer.Play();
                    }

                    // Задаване на обратната стойност на променливата за пауза
                    isPauseActivated = !isPauseActivated;

                    break;

                default:
                    // Пускане на запис за грешка и изчистване на настоящото поле
                    soundPlayererror.Play();

                    break; 
            }

            // Създаване на буферна променлива със стойност текста на полето за въвеждане
            int commandNumber = int.Parse(command_txt.Text);

            // Отваряне на създадения прозорец при валидните числа
            if (commandNumber > 0 && commandNumber < 10 && commandNumber != 5
                || commandNumber == 159)
            {
                formToOpen.Show();
            }
        }

        private void Homeblind_Load(object sender, EventArgs e)
        {
            // Пускане на запис с първоначални инструкции,
            soundPlayer.Play();
            
            // Стартиране на постояния процес за изчакване за получаване на имейл съобщения
            StartMail();
        }

        private void StartMail()
        {
            // Конфигуриране на имейл клиент
            Task.Run(() =>
            {
                // Задаване параметрите на имейла на потребителя, въведен в първоначалните настройки
                using (ImapClient client = new ImapClient(
                    "imap.gmail.com", 993, Properties.Settings.Default.Mail, Properties.Settings.Default.Password, AuthMethod.Login, true))
                {                    
                    // При получаване на ново съобщение да се активира void "OnNewMessage"
                    client.NewMessage += NewMessage;
                    
                    // С цикъла while се осигурява постоянното изпълнение на процеса, с което се гарантира, 
                    // че когато и съобщението да бъде получено по ел.поща, то програмата да го получи   
                    while (true);
                }
            });
            
        }

        static void NewMessage(object sender, IdleMessageEventArgs e)
        {
            // Пускане на записа с инструкции, уведомяващ потребителя за получено ново съобщение
            form.soundPlayernew.Play();
        }
    }
}
