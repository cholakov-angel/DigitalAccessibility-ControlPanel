using System;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace Easy_mode_Desktop
{
    public partial class Step6 : Form
    {
        // Дефиниране на синтезатор на глас
        SpeechSynthesizer speech;
        public Step6()
        {
            // Инициализиране на нова инстанция от класа SpeechSynthesizer като speech;
            speech = new SpeechSynthesizer();
            InitializeComponent();
        } // Step6

        private void step6_Load(object sender, EventArgs e)
        {
            // Извличане на имената на инсталираните SAPI5 гласове
            foreach (var voice in speech.GetInstalledVoices())
            {
                // Добавяне на имената на инсталираните гласове като елементи на comboBox1
                comboBox1.Items.Add(voice.VoiceInfo.Name);
            }
        } // step6_Load

        private void button1_Click(object sender, EventArgs e)
        {
            // Избиране на глас по подразбиране на избрания глас от comboBox1 (неговия текст)
            // и неготово запазване
            Properties.Settings.Default.voice = comboBox1.Text;
            Properties.Settings.Default.Save();

            Finalstep fs = new Finalstep();

            // Отваряне на прозореца за приключване на първоначалната настройка в режим за незрящи
            fs.Show();

            // Затваряне на настоящия прозорец
            this.Close();
        } // button1_Click
    } // Step6
}
