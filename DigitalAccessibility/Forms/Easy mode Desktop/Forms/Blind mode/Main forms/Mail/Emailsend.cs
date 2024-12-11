using System;
using System.Windows.Forms;
using WindowsInput.Native;
using WindowsInput;
using System.Speech.Synthesis;
using System.Net.Mail;
using System.Media;
using System.Threading;
using System.IO;
using System.Globalization;
using AudioSwitcher.AudioApi.CoreAudio;
using System.Diagnostics;
using Easy_mode_Desktop.Blind_mode.Volume_control;
using Easy_mode_Desktop.Keyboard_layout_changer;

namespace Easy_mode_Desktop
{
    public partial class Emailsend : Form
    {
        // Дефиниране на синтезатор на глас
        SpeechSynthesizer speech;

        // Дефиниране на предварително записаните аудио записи с инструкции с техните локации
        SoundPlayer soundPlayeropen = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\emailstart.wav");
        SoundPlayer soundPlayerclose = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\emailclose.wav");
        SoundPlayer soundPlayertoselect = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\toselect.wav");
        SoundPlayer soundPlayerto = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\emailto.wav");
        SoundPlayer soundPlayersubject = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\emailsubject.wav");
        SoundPlayer soundPlayeremail = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\email.wav");
        SoundPlayer soundPlayernext = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\emailnext.wav");
        SoundPlayer soundPlayernext1 = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\emailnext1.wav");
        SoundPlayer soundPlayerback = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\emailback.wav");
        SoundPlayer soundPlayersent = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\emailsent.wav");
        SoundPlayer soundPlayearttach = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\attach.wav");
        SoundPlayer soundPlayearttachs = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\attachs.wav");
        SoundPlayer soundPlayearttachno = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\attachuns.wav");
        SoundPlayer soundPlayererror = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\commerror.wav");
        SoundPlayer soundPlayerbg = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\\bg.wav");
        SoundPlayer soundPlayeren = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\en.wav");
        SoundPlayer soundPlayervolume = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\volume1.wav");
        SoundPlayer soundPlayervolume1 = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\volume2.wav");

        public Emailsend()
        {
            InitializeComponent();

            // Задаване на нова инстанция на класа SpeechSynthesizer като speech
            speech = new SpeechSynthesizer();
        }

        private void command_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                switch (command_txt.Text)
                {
                    case "0":
                        // Повторно пускане на запис с инструкции и изчистване на настоящото текстово поле
                        soundPlayeropen.Play();
                        contact_txt.Select();

                        break;

                    case "1":
                        // Пускане на запис с инструкции и избор на текстово поле за избор на получател
                        soundPlayertoselect.Play();
                        contact_txt.Select();

                        break;

                    case "10":
                        // Пускане на запис с инструкции
                        soundPlayerclose.Play();
                        
                        // Затваряне на настоящия прозорец
                        Close();

                        break;

                    case "11":
                        // Задаване на български език на въвеждане
                        KeyboardLayoutSwitcher.Bulgarian();
                        command_txt.Clear();

                        break;

                    case "12":
                        // Задаване на английски език на въвеждане
                        KeyboardLayoutSwitcher.English();
                        command_txt.Clear();

                        break;

                    case "13":
                        // Извършване на операцията по намаляване на звука
                        VolumeChanger.VolumeDown();

                        // Изчистване на настоящото текстово поле
                        command_txt.Clear();

                        break;

                    case "14":
                        // Извършване на операцията по увеличаване на звука
                        VolumeChanger.VolumeUp();

                        // Изчистване на настоящото текстово поле
                        command_txt.Clear();

                        break;

                    default:
                        // Пускане на запис за грешка и изчистване на настоящото поле
                        soundPlayererror.Play();
                        command_txt.Clear();

                        break;
                }
            }
        }

        private void from_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                // При натискане на бутон Enter се избира текстово поле за въвеждане на парола
                password_txt.Select();
            }
        }

        private void password_txt_KeyDown(object sender, KeyEventArgs e)
        {         
            if (e.KeyData == Keys.Enter)
            {
                // При натискане на бутон Enter се избира текстовото поле за въвеждане на получател
                to_txt.Select();
            }
        }

        private void to_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                // При натискане на бутон Enter се пуска запис с инструкции
                soundPlayersubject.Play();

                if (voicelabel.Text == "1")
                {
                    // При установяване на стойност на маркера за текст, показващ дали гласовото въвеждане  включено, "1" се избира 2 помощно 
                    // текстово поле
                    opt2.Select();
                }
                else
                {
                    // При установяване на стойност на маркера за текст, показващ дали гласовото въвеждане  включено, различта от "1" 
                    // се избира текстовото поле за въвеждане на тема на съобщението
                    subject_txt.Select();
                }
            }
        }

        private void subject_txt_KeyDown(object sender, KeyEventArgs e)
        {
            // При натискане на Enter
            if (e.KeyData == Keys.Enter)
            {
                // При установен текст на текстовото поле "voicelabel" "1", се задействат инструкциите при случай на включена функция за гласово въвеждане
                if (voicelabel.Text == "1")
                {
                    // Пускане на запис с инструкции и избиране на трето помощно текстово поле
                    soundPlayeremail.Play();
                    opt3.Select();
                }
                else
                {
                    // Пускане на запис с инструкции и избиране на текстовото поле за въвеждане на съобщение
                    soundPlayeremail.Play();
                    letter_txt.Select();
                }
            }
        }

        private void Emailsend_Load(object sender, EventArgs e)
        {
            // Пускане на файл с инструкции
            soundPlayeropen.Play();

            // Задаване стойността на текстовите полета за подател и парола на съответно запазените стойности за подател и парола
            from_txt.Text = Properties.Settings.Default.Mail;
            password_txt.Text = Properties.Settings.Default.Password;
            voicelabel.Text = Properties.Settings.Default.voicetype;
        }

        private void letter_txt_KeyDown(object sender, KeyEventArgs e)
        {
            // При натискане на бутон Enter се избира полето за въвеждане на команди след въвеждане на имейл и се пуска запис с инструкции
            if (e.KeyData == Keys.Enter)
            {
                soundPlayernext.Play();
                command_aftertxt.Select();
            }
        }

        private void command_after_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                switch (command_aftertxt.Text)
                {
                    case "0":
                        // Пускане на записа с инстукции (този след написване на съобщението) и
                        // изчистване на настоящето текстово поле
                        soundPlayernext1.Play();
                        command_aftertxt.Clear();

                        break;

                    case "1":
                        // Прочит на въведеното съобщение и изчистване на настоящето поле
                        speech.SelectVoice(Properties.Settings.Default.voice);
                        speech.SpeakAsync(letter_txt.Text);
                        command_aftertxt.Clear();

                        break;

                    case "2":
                        // Пускане на запис с инструкции
                        soundPlayerback.Play();

                        // Избиране на текстовото поле за въвеждане на съобщение
                        letter_txt.Select();

                        // Изчистване на натстоящото поле
                        command_aftertxt.Clear();

                        break;

                    case "3":
                        // Пускане на запис с инструкции
                        soundPlayearttach.Play();
                        if (voicelabel.Text == "1")
                        {
                            // При стойност на маркер за текст "voicelabel" "1" се изчаква и се пуска таймера,
                            // вклюючващ гласовото въвеждане
                            Thread.Sleep(3500);
                            timer2.Start();
                        }

                        // Избор на текстовото поле за въвеждане на име на прикачен файл
                        filename_txt.Select();

                        // Изчистване на настоящето текстово поле
                        command_aftertxt.Clear();

                        break;

                    case "4":
                        using (MailMessage mail = new MailMessage())
                        {
                            // Задаване на стойностите на имейл съобщение
                            mail.From = new MailAddress(from_txt.Text);
                            mail.To.Add(to_txt.Text);
                            mail.Subject = subject_txt.Text;
                            mail.Body = letter_txt.Text;

                            // Добавяне на прикачени файлове, ако има такива (стойността на настройката
                            // "emailattachmentcheck" трябва да бъде "1"
                            if (Properties.Settings.Default.emailattachmentcheck != 0)
                            {
                                mail.Attachments.Add(new Attachment(filedirectory_txt.Text));
                            }

                            // Конфигуриране на настройки на имейл клиент
                            using (SmtpClient smpt = new SmtpClient(Properties.Settings.Default.MailOutgoingServer, 587))
                            {
                                smpt.UseDefaultCredentials = false;

                                // Задаване на информацията за получател
                                smpt.Credentials = new System.Net.NetworkCredential(from_txt.Text, password_txt.Text);

                                // Включване на SSL като конфигурация на имейл
                                smpt.EnableSsl = true;

                                // Изпращане на имейл съобщение
                                smpt.Send(mail);

                                // Пускане на запис с инструкции
                                soundPlayersent.Play();

                                // Задаване обратно на стойнсот 0 на настройката за прикачен имейл
                                Properties.Settings.Default.emailattachmentcheck = 0;
                                Properties.Settings.Default.Save();

                                // Изчистване на информацията от текстовите полета
                                command_txt.Clear();
                                to_txt.Clear();
                                subject_txt.Clear();
                                letter_txt.Clear();
                                command_aftertxt.Clear();
                                contact_txt.Clear();
                                filedirectory_txt.Clear();
                                filename_txt.Clear();

                                // Избор на текстово поле за въвеждане на команди
                                command_txt.Select();
                            }
                        }
                        break;

                    default:
                        // Изчистване на настоящото поле и пускане на запис за грешка
                        command_aftertxt.Clear();
                        soundPlayererror.Play();

                        break;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        { 
            var simu = new InputSimulator();

            // Симулиране на клавишна комбинация - Win+H, която активира гласотото въвеждане
            simu.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_H);

            // Спиране на настоящия таймер
            timer2.Stop();
        }

        private void opt2txt_KeyDown(object sender, KeyEventArgs e)
        {
            // При натискане на бутон Enter се избира текстовото поле за въвеждане на тема
            if (e.KeyData == Keys.Enter)
            {
                subject_txt.Select();

                // При стойност "1" на маркера за текст "voicelabel" - т.е. включено гласово въвеждане, се активира
                // таймера, активиращ гласовото въвеждане
                if (voicelabel.Text == "1")
                {
                    Thread.Sleep(3500);
                    timer2.Start();
                }
            }
        }

        private void opt3txt_KeyDown(object sender, KeyEventArgs e)
        {
            // При натискане на бутон Enter се избира текстовото поле за въвеждане на съобщение
            if (e.KeyData == Keys.Enter)
            {
                letter_txt.Select();

                // При стойност "1" на маркера за текст "voicelabel" - т.е. включено гласово
                // въвеждане, се активира таймера, активиращ гласовото въвеждане 
                if (voicelabel.Text == "1")
                {
                    Thread.Sleep(3500);
                    timer2.Start();
                }
            }
        }

        private void contact_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                switch (contact_txt.Text)
                {
                    case "0":
                        // Повторно пускане на запис с инструкции и изчистване на настоящото текстово поле
                        soundPlayertoselect.Play();
                        contact_txt.Clear();

                        break;

                    case "1":
                        // Пускане на файл с инструкции, задаване стойността на текстовото поле за
                        // получател на информацията от настройката за 1. контакт и избор на полето
                        // за въвеждане на тема на съобщение
                        soundPlayersubject.Play();
                        to_txt.Text = Properties.Settings.Default.contact1;

                        if (voicelabel.Text == "1")
                        {
                            // При установяване на стойност на маркера за текст,
                            // показващ дали гласовото въвеждане  включено, "1" се избира 2 помощно текстово поле
                            opt2.Select();
                        }
                        else
                        {
                            // При установяване на стойност на маркера за текст, показващ дали гласовото въвеждане
                            // включено, различта от "1" се избира текстовото поле за въвеждане на тема на съобщението
                            subject_txt.Select();
                        }

                        break;

                    case "2":
                        // Пускане на файл с инструкции, задаване стойността на текстовото поле за получател
                        // на информацията от настройката за 2. контакт и избор на полето за въвеждане на
                        // тема на съобщение
                        soundPlayersubject.Play();
                        to_txt.Text = Properties.Settings.Default.contact2;

                        if (voicelabel.Text == "1")
                        {
                            // При установяване на стойност на маркера за текст, показващ дали гласовото
                            // въвеждане  включено, "1" се избира 2 помощно текстово поле
                            opt2.Select();
                        }
                        else
                        {
                            // При установяване на стойност на маркера за текст, показващ дали гласовото въвеждане
                            // включено, различта от "1"  се избира текстовото поле за въвеждане на тема на съобщението
                            subject_txt.Select();
                        }

                        break;

                    case "3":
                        // Пускане на файл с инструкции, задаване стойността на текстовото поле за получател на
                        // информацията от настройката за 3. контакт и избор на полето за въвеждане на тема на съобщение
                        soundPlayersubject.Play();
                        to_txt.Text = Properties.Settings.Default.contact3;

                        if (voicelabel.Text == "1")
                        {
                            // При установяване на стойност на маркера за текст, показващ дали гласовото въвеждане
                            // включено, "1" се избира 2 помощно текстово поле
                            opt2.Select();
                        }
                        else
                        {
                            // При установяване на стойност на маркера за текст, показващ дали гласовото въвеждане
                            // включено, различта от "1" се избира текстовото поле за въвеждане на тема на съобщението
                            subject_txt.Select();
                        }

                        break;

                    case "4":
                        // Пускане на файл с инструкции, задаване стойността на текстовото поле за получател на
                        // информацията от настройката за 4. контакт и избор на полето за въвеждане на тема на съобщение
                        soundPlayersubject.Play();
                        to_txt.Text = Properties.Settings.Default.contact4;

                        if (voicelabel.Text == "1")
                        {
                            // При установяване на стойност на маркера за текст, показващ дали гласовото
                            // въвеждане  включено, "1" се избира 2 помощно текстово поле
                            opt2.Select();
                        }
                        else
                        {
                            // При установяване на стойност на маркера за текст, показващ дали гласовото
                            // въвеждане  включено, различта от "1" се избира текстовото поле за въвеждане
                            // на тема на съобщението
                            subject_txt.Select();
                        }

                        break;

                    case "5":
                        // Пускане на файл с инструкции, задаване стойността на текстовото поле за получател
                        // на информацията от настройката за 5. контакт и избор на полето за въвеждане на
                        // тема на съобщение
                        soundPlayersubject.Play();
                        to_txt.Text = Properties.Settings.Default.contact5;

                        if (voicelabel.Text == "1")
                        {
                            // При установяване на стойност на маркера за текст, показващ дали гласовото
                            // въвеждане  включено, "1" се избира 2 помощно текстово поле
                            opt2.Select();
                        }
                        else
                        {
                            // При установяване на стойност на маркера за текст, показващ дали гласовото въвеждане
                            // включено, различта от "1" се избира текстовото поле за въвеждане на тема на съобщението
                            subject_txt.Select();
                        }

                        break;

                    case "6":
                        // Пускане на запис с инструкции и избор на текстово поле за въвеждане на получател
                        soundPlayerto.Play();
                        to_txt.Select();

                        if (voicelabel.Text == "1")
                        {
                            // При стойност на маркера за текст "voicelabel" "1" се изчаква и се пуска таймера,
                            // включващ гласовото въвеждане
                            Thread.Sleep(3500);
                            timer2.Start();
                        }
                        break;

                    default:
                        // Изчистване на настоящото текстово поле и пускане на запис за грешка
                        contact_txt.Clear();
                        soundPlayererror.Play();

                        break;
                }               
            }
        }

        private void filename_txt_KeyDown(object sender, KeyEventArgs e)
        {
            // При натискане на бутон ENTER се задава стойност на 11 текстово поле на "C:\Easy mode Files\",
            // заедно с информацията, въведена в настоящето поле и ".txt", изчистване на настоящото текстово поле
            // и неговото изчистване
            if (e.KeyData == Keys.Enter)
            {
                // Задаване на стойността на текстоото поле "filedirectotytxt" на локацията на файла и името
                // на файла, въведено от потребителя
                filedirectory_txt.Text = @"C:\Digital accessibility\Easy mode Files\" + filename_txt.Text + ".txt";

                if (!File.Exists(filedirectory_txt.Text))
                {
                    // При установяване, че въведения файл не съществува, се пуска запис, уведомяващ
                    // потребителя за това
                    soundPlayearttachno.Play();
                }
                else
                {
                    // При установяване, че въведения файл съществува, се пуска запис, уведомяващ
                    // потребителя за това
                    soundPlayearttachs.Play();
                }

                // Избиране на полето за въвеждане на команди и изчистване на същото поле
                command_aftertxt.Select();
                command_aftertxt.Clear();

                // Задаване на стойност на настройката за проверка на прикачен файл на 1 - включена и запазването ѝ
                Properties.Settings.Default.emailattachmentcheck = 1;
                Properties.Settings.Default.Save();
            }
        }
    }
}
