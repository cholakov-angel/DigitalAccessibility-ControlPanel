using System;
using System.Windows.Forms;

namespace Easy_mode_Desktop
{
    public partial class Step2 : Form
    {
        public Step2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Задаване настройка за гласово въвеждане на стойност "1" - включена
            Properties.Settings.Default.voicetype = "1";
            Properties.Settings.Default.Save();

            Step6 step5 = new Step6();

            // Отваряне на 3 стъпка от настройката на програмата (Step5)
            step5.Show();
            
            // Затваряне на настоящия прозорец
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Задаване настройка за гласово въвеждане на стойност "2" - изключена
            Properties.Settings.Default.voicetype = "2";
            Properties.Settings.Default.Save();

            Step6 f2 = new Step6();

            // Отваряне на 3 стъпка от настройката на програмата (Step5)
            f2.Show();

            // Затваряне на настоящия прозорец
            Close();
        }

    }
}
