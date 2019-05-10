using System;
using System.IO;
using System.Windows.Forms;


/// <summary>
/// Seth G. R. Herendeen
/// 
/// 
/// </summary>
namespace FlyNotes
{
    public partial class FlyNotes : Form
    {
        private Configuration config;
        private bool isLoaded = false;
        /// <summary>
        /// represents the path of the currently opened file
        /// </summary>
        private string currentFilePath = "";

        private string title = "Fly Notes";

        public FlyNotes()
        {
            InitializeComponent();
        }

        private void FlyNotes_Load(object sender, EventArgs e)
        {
            if( File.Exists("fconfig.xml") == false)
            {
                Configuration.SetConfiguration("fconfig.xml",
                    new Configuration(1, 0, "Impetus",
                    "Times New Roman", 12.0f));
            }
            else
            {
                //Console.WriteLine("line 40");
                this.config = Configuration.GetConfiguration("fconfig.xml");
            }

            this.isLoaded = true;

            //this.rtBoxMain.Text = $"{sbyte.MaxValue + ", "+sbyte.MinValue}";
#if DEBUG == true
#endif

        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            if (isLoaded)
            {
                MessageBox.Show($"Fly Notes {config.MajorVersion}.{config.MinorVersion} \"{config.VersionName}\"" +
                    $"\n{config.DefaultFontName + " " + config.DefaultFontSize}", "About - Fly Notes");
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MnuNew_Click(object sender, EventArgs e)
        {
            this.rtBoxMain.Clear();
            this.Text = this.title;
        }



        private void mnuOpen_Click(object sender, EventArgs e)
        {
            var openFileDialog = Helper.PrepareOpenFileDialog();


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.currentFilePath = openFileDialog.FileName;
                this.Text += title + $" {this.currentFilePath}";

                this.rtBoxMain.Clear();

                HandleReadingFile(openFileDialog.FileName);
            }
            
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            var saveFileDialog = Helper.PrepareSaveFileDialog();

            if (currentFilePath == "")
            {

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    HandleSavingFile(saveFileDialog.FileName);
                }
            }
            else
            {
                HandleSavingFile(saveFileDialog.FileName);
            }
        }

        private void HandleSavingFile(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            switch (extension)
            {
                case ".rtf":
                    this.rtBoxMain.SaveFile(fileName);
                    break;
                case ".fntxt":
                    
                    break;
                case ".txt":
                default:
                    File.WriteAllText(fileName,
                        this.rtBoxMain.Text);
                    break;
            }
        }

        private void HandleReadingFile(string fileName)
        {
            string extension = Path.GetExtension(fileName);
          //  Console.WriteLine("********* " + extension + " ********");
            
            switch (extension)
            {
                case ".rtf":
                    //Console.WriteLine("************* "+ this.currentFilePath);
                    //Console.WriteLine("*************"+ fileName);
                    //MessageBox.Show($"{this.currentFilePath}\n{fileName}");


                    this.rtBoxMain.LoadFile(this.currentFilePath);
                    break;
                case ".fntxt":
                    FntxtDoc file = FntxtDoc.LoadFile(fileName);
                    this.rtBoxMain.Text = file.Text;


                    break;
                case ".txt":
                default:

              //      Console.WriteLine("entered default reader");
             //       MessageBox.Show($"Entered DEFAULT READER");
                    this.rtBoxMain.Text = File.ReadAllText(fileName);
                    break;
            }



        }

        private void mnuFont_Click(object sender, EventArgs e)
        {
            var fontDialog = new FontDialog
            {
               
            };

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(fontDialog.Font.ToString());
                Console.WriteLine(fontDialog.Color.ToString());
                this.rtBoxMain.SelectionFont = fontDialog.Font;
                this.rtBoxMain.ForeColor = fontDialog.Color;
            }
        }

        private void mnuDocumentation_Click(object sender, EventArgs e)
        {

        }
    }
}
