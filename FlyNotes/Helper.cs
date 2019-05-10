using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlyNotes
{
    class Helper
    {
        public static int ConvertStringToIntegerSafely(string input)
        {
            int output = 0;

            if (int.TryParse(input, out output) == false)
            {
                MessageBox.Show("Failed to convert safely!\n"+
                    $"input string:{input}\noutput int32:{output}",
                    "Mismatch error");
            }

            return output;
        }

        public static bool ConvertStringToBooleanSafely(string input)
        {
            bool output = false;
            if(bool.TryParse(input,out output) == false)
            {
                MessageBox.Show("Failed to convert safely!\n" +
                    $"input string:{input}\noutput boolean:{output}",
                    "Mismatch error");
            }
            return output;
        }

        public static float ConvertStringToFloatSafely(string input)
        {
            float output = 0f;

            if (float.TryParse(input, out output) == false)
            {
                MessageBox.Show("Failed to convert safely!\n" +
                    $"input string:{input}\noutput int32:{output}",
                    "Mismatch error");
            }

            return output;
        }

        public static OpenFileDialog PrepareOpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = $"{Environment.SpecialFolder.MyDocuments}",
                RestoreDirectory = true,
                Title = "Browse Text Documents",
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt|Flynote Files (*.fntxt)|*.fntxt|All files (*.*)|*.*",
                FilterIndex = 1,
                CheckFileExists = true,
                CheckPathExists = true
            };
            return openFileDialog;
        }


    }
}
