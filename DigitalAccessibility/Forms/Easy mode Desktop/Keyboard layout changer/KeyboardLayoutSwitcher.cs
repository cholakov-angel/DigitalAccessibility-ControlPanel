using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_mode_Desktop.Keyboard_layout_changer
{
    public static class KeyboardLayoutSwitcher
    {
        static SoundPlayer soundPlayerbg = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\bg.wav");
        static SoundPlayer soundPlayeren = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\en.wav");

        public static void Bulgarian()
        {
            // Задаване на български език за текущ метод на въвеждане
            CultureInfo bg = new CultureInfo("bg-bg");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(bg);

            // Пускане на запис с инструкции и изчистване на настоящото поле
            soundPlayerbg.Play();
        }

        public static void English()
        {
            // Задаване на английски език за текущ метод на въвеждане
            CultureInfo en = new CultureInfo("en-en");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(en);

            // Пускане на запис с инструкции и изчистване на настоящото поле
            soundPlayeren.Play();
        }
    }
}
