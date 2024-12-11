using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Easy_mode_Desktop.Blind_mode.Volume_control
{
    public static class VolumeChanger
    {
        static SoundPlayer soundPlayervolume1 = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\volume2.wav");
        static SoundPlayer soundPlayervolume = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\volume1.wav");

        public static void VolumeUp()
        {
            // Извършване на операцията по увеличаване на звука
            CoreAudioDevice defaultPlaybackDevice1 = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice1.Volume += 15;

            // Пускане на запис с инструкции и изчистване на настоящото поле
            soundPlayervolume1.Play();
        }

        public static void VolumeDown()
        {
            // Извършване на операцията по увеличаване на звука
            CoreAudioDevice defaultPlaybackDevice1 = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice1.Volume -= 15;

            // Пускане на запис с инструкции и изчистване на настоящото поле
            soundPlayervolume.Play();
        }
    }
}
