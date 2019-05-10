using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            FntxtDoc fntxtDoc = FntxtDoc.LoadFile(@"sadness.fntxt");

            MessageBox.Show($"{fntxtDoc.Font} {fntxtDoc.FontSize} {fntxtDoc.Text}", "");

            rtBoxMain.Text = fntxtDoc.Text;
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

        private void mnuNew_Click(object sender, EventArgs e)
        {

        }



        private void mnuOpen_Click(object sender, EventArgs e)
        {
            var openFileDialog = Helper.PrepareOpenFileDialog();
            

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.currentFilePath = openFileDialog.FileName;
                this.Text += title+ $" {this.currentFilePath}";
            }

            //clear main text control
            this.rtBoxMain.Clear();
            
            HandleReadingFile(openFileDialog.FileName);
            
        }

        private void HandleReadingFile(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            Console.WriteLine("********* " + extension + " ********");
            
            switch (extension)
            {
                case ".fntxt":

                    break;
                case ".txt":
                default:
                    this.rtBoxMain.Text = File.ReadAllText(this.currentFilePath);
                    break;
            }



        }

        private void mnuSave_Click(object sender, EventArgs e)
        {

        }

        private void asToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnuFont_Click(object sender, EventArgs e)
        {

        }

        private void mnuDocumentation_Click(object sender, EventArgs e)
        {

        }
    }




}
