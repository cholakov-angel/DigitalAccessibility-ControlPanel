using System;
using System.Collections;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace Easy_mode_Desktop
{
    public partial class Settings : Form
    {
        SpeechSynthesizer speech;

        public Settings()
        {
            speech = new SpeechSynthesizer();

            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение за успешно направените промени
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // С тази опция се задава настройка за гласово въвеждане на стойност "1" - включена
            Properties.Settings.Default.voicetype = "1";

            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение за успешно направените настройки
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // С тази опция се задава настройка за гласово въвеждане на стойност "2" - изключена
            Properties.Settings.Default.voicetype = "2";

            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение за успешно направените настройки
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // С тази опция се запазва въведената в текстово поле 3 стойност за контакт 1
            Properties.Settings.Default.contact1 = textBox3.Text;

            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение за успешно направените настройки
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // С тази опция се запазва въведената в текстово поле 4 стойност за контакт 2
            Properties.Settings.Default.contact2 = textBox4.Text;

            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение за успешно направените настройки
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // С тази опция се запазва въведената в текстово поле 5 стойност за контакт 3
            Properties.Settings.Default.contact3 = textBox5.Text;

            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение за успешно направените настройки
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // С тази опция се запазва въведената в текстово поле 6 стойност за контакт 4
            Properties.Settings.Default.contact4 = textBox6.Text;

            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение за успешно направените настройки
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // С тази опция се запазва въведената в текстово поле 7 стойност за контакт 5
            Properties.Settings.Default.contact5 = textBox7.Text;

            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение за успешно направените настройки
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // С тази опция се нулира програмата "Дигитална достъпност"
            Properties.Settings.Default.setup = "Първоначална настройка";

            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение за успешно направените настройки
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // С тази опция се задава режим на комуникация като основен
            Properties.Settings.Default.setup = "Режим за комуникация";

            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение за успешно направените настройки
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            // Задаване стойността на текстовите полета да бъдат стойностите съответно за имейл адрес и парола
            textBox3.Text = Properties.Settings.Default.contact1;
            textBox4.Text = Properties.Settings.Default.contact2;
            textBox5.Text = Properties.Settings.Default.contact3;
            textBox6.Text = Properties.Settings.Default.contact4;
            textBox7.Text = Properties.Settings.Default.contact5;
            // Извличане на инсталираните SAPI5 гласове в синтезатора speech
            foreach (var voice in speech.GetInstalledVoices())
            {
                // Задаване на елементи на падащото меню comboBox1 да бъдат инсталираните гласове
                comboBox1.Items.Add(voice.VoiceInfo.Name);
            }
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            // Задаване стойността на настройката "voice" (гласа, който програмата използва) на стойността
            // на падащото меню "comboBox1"
            Properties.Settings.Default.voice = comboBox1.Text;

            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение, уведомяващо потребителя за успешното променяне на настройката
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // Задаване стойността на настройката "voicetype" (ключа за ChatGPT) на стойността
            // на текстовото поле "textBox9"
            Properties.Settings.Default.voiceassist = textBox9.Text;

            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение, уведомяващо потребителя за успешното променяне на настройката
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение за успешно направените промени
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Задаване стойността на настройката "voice" (гласа, който програмата използва) на стойността
            // на падащото меню "comboBox1"
            Properties.Settings.Default.voice = comboBox1.Text;

            // Запазване на направените промени на настройката
            Properties.Settings.Default.Save();

            // Извеждане на съобщение, уведомяващо потребителя за успешното променяне на настройката
            MessageBox.Show("Успешно запазихте настройките!");
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            Properties.Settings.Default.setup = "Първоначална настройка";
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            Properties.Settings.Default.setup = "Първоначална настройка";
            Properties.Settings.Default.Save();
        }
    }
}
