using System;
using System.Windows.Forms;
using WindowsInput;
using System.Media;
using WindowsInput.Native;


namespace Easy_mode_Desktop
{
    public partial class Form8 : Form
    {
        // Дефиниране на предварително записаните аудио записи с инструкции с техните локации
        SoundPlayer soundPlayerwater = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\water.wav");
        SoundPlayer soundPlayerfood = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\food.wav");
        SoundPlayer soundPlayerwc = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\wc.wav");
        SoundPlayer soundPlayerah = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\ah.wav");
        SoundPlayer soundPlayerlosho = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\losho.wav");
        SoundPlayer soundPlayeryes = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\yes.wav");
        SoundPlayer soundPlayerno = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\no.wav");

        public Form8()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            InputSimulator simu = new InputSimulator();

            // Симулиране на натискане на бутон TAB
            simu.Keyboard.KeyDown(VirtualKeyCode.TAB);
            timer1.Stop();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.NumPad7:
                    // Избор на аудио файл със съдържание "Да" и избиране на първоначален бутон
                    soundPlayeryes.Play();
                    button1.Select();

                    break;

                case Keys.NumPad9:
                    // Избор на аудио файл със съдържание "Не" и избиране на първоначален бутон
                    soundPlayerno.Play();
                    button1.Select();

                    break;

                case Keys.NumPad0:
                    // Стартиране на таймера, симулиращ натискане на бутон TAB
                    timer1.Start();

                    break;
            }
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.NumPad7:
                    // Избор на аудио файл със съдържание "Да" и избиране на първоначален бутон
                    soundPlayeryes.Play();
                    button1.Select();

                    break;

                case Keys.NumPad9:
                    // Избор на аудио файл със съдържание "Не" и избиране на първоначален бутон
                    soundPlayerno.Play();
                    button1.Select();

                    break;

                case Keys.NumPad0:
                    // Стартиране на аудио файл с основна нужда и стартиране на таймера,
                    // симулиращ натискане на бутон TAB
                    soundPlayerwater.Play();

                    timer1.Start();

                    break;
            }
        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.NumPad7:
                    // Избор на аудио файл със съдържание "Да" и избиране на първоначален бутон
                    soundPlayeryes.Play();
                    button1.Select();

                    break;

                case Keys.NumPad9:
                    // Избор на аудио файл със съдържание "Не" и избиране на първоначален бутон
                    soundPlayerno.Play();
                    button1.Select();

                    break;

                case Keys.NumPad0:
                    // Стартиране на аудио файл с основна нужда и стартиране на таймера, симулиращ натискане на бутон TAB
                    soundPlayerfood.Play();
                    timer1.Start();

                    break;
            }
        }

        private void button4_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.NumPad7:
                    // Избор на аудио файл със съдържание "Да" и избиране на първоначален бутон
                    soundPlayeryes.Play();
                    button1.Select();

                    break;

                case Keys.NumPad9:
                    // Избор на аудио файл със съдържание "Не" и избиране на първоначален бутон
                    soundPlayerno.Play();
                    button1.Select();

                    break;

                case Keys.NumPad0:
                    // Стартиране на аудио файл с основна нужда и стартиране на таймера, симулиращ натискане на бутон TAB
                    soundPlayerwc.Play();
                    timer1.Start();

                    break;
            }
        }

        private void button5_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.NumPad7:
                    // Избор на аудио файл със съдържание "Да" и избиране на първоначален бутон
                    soundPlayeryes.Play();
                    button1.Select();

                    break;

                case Keys.NumPad9:
                    // Избор на аудио файл със съдържание "Не" и избиране на първоначален бутон
                    soundPlayerno.Play();
                    button1.Select();

                    break;

                case Keys.NumPad0:
                    // Стартиране на таймера, симулиращ натискане на бутон TAB
                    soundPlayerah.Play();
                    timer1.Start();

                    break;
            }
        }

        private void button6_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.NumPad7:
                    // Избор на аудио файл със съдържание "Да" и избиране на първоначален бутон
                    soundPlayeryes.Play();
                    button1.Select();

                    break;

                case Keys.NumPad9:
                    // Избор на аудио файл със съдържание "Не" и избиране на първоначален бутон
                    soundPlayerno.Play();
                    button1.Select();

                    break;

                case Keys.NumPad0:
                    // Стартиране на аудио файл с основна нужда и стартиране на таймера, симулиращ натискане на бутон TAB
                    soundPlayerlosho.Play();
                    timer1.Start();

                    break;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();

            // Отваряне на администраторския панел (settings)
            settings.Show();
        }
    }
}
