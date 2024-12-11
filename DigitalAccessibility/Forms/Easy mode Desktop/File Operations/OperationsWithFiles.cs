using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Easy_mode_Desktop.FileOperations
{
    public static class OperationsWithFiles
    {
        static SoundPlayer soundPlayertexterror = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\errorfile.wav");
        static SoundPlayer soundPlayererrorsave = new SoundPlayer(soundLocation: @"C:\Digital accessibility\BG\errorsave.wav");

        public static string OpenFile(string path, string fileName, string extension)
        {
            string result = "";
            // Извършва се проверка за това дали въведения файл съществува
            if (!File.Exists(path + fileName + extension))
            {
                // При установена липса на съществуване на файла, се пуска запис с инструкции
                soundPlayertexterror.Play();

                throw new ArgumentException();
            }
            else
            {
                // Отваряне на документ в текстово поле за въвеждане на текст с локация:C:\Easy mode files\, с име-въведеното в текстовото поле за отваряне на файл, във формат .txt
                StreamReader read = new StreamReader(File.OpenRead(path + fileName + extension));
                result = read.ReadToEnd();

                // Спиране на StreamReader - "read"
                read.Dispose();

                return result;
            }
        }

        public static void SaveFile(string path, string fileName, string extension, string text)
        {
            if (fileName == "")
            {
                // Пускане на запис с инструкции
                soundPlayererrorsave.Play();

                throw new ArgumentException();
            }
            else
            {
                StreamWriter streamWriter =
                    new StreamWriter(path + fileName + extension);
                streamWriter.Write(text);

                // Затваряне на StreamWriter - "streamWriter"
                streamWriter.Close();
            }
        }
    }
}
