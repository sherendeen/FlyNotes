using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FlyNotes
{
    public class Configuration
    {

        private readonly int majorVersion;
        private readonly int minorVersion;
        private readonly string versionName;
        /// <summary>
        /// Used when creating new files, opening txt files
        /// It is overrided when loading fntxt files
        /// </summary>
        private string defaultFontName;
        private float defaultFontSize;

        public int MajorVersion
        {
            get
            {
                return this.majorVersion;
            }
        }

        public int MinorVersion
        {
            get
            {
                return this.minorVersion;
            }
        }

        public string VersionName
        {
            get
            {
                return this.versionName;
            }
        }

        public string DefaultFontName
        {
            get
            {
                return this.defaultFontName;
            }

            set
            {
                this.defaultFontName = value;
            }
        }

        public float DefaultFontSize
        {
            get
            {
                return this.defaultFontSize;
            }

            set
            {
                this.defaultFontSize = value;
            }
        }



        public Configuration()
        {
            this.majorVersion = 1;
            this.minorVersion = 0;
            this.versionName = "Unknown";
            this.defaultFontName = "Times New Roman";
            this.defaultFontSize = 12f;
        }

        public Configuration(int majorVersion, int minorVersion,
            string versionName, string defaultFontName, float defaultFontSize)
        {
            this.majorVersion = majorVersion;
            this.minorVersion = minorVersion;
            this.versionName = versionName;
            this.defaultFontName = defaultFontName;
            this.defaultFontSize = defaultFontSize;
        }

        /// <summary>
        /// Gets the XML-based configuration for the application
        /// </summary>
        /// <param name="pathToFile">path to the file</param>
        /// <returns></returns>
        public static Configuration GetConfiguration(string pathToFile)
        {

            var settings = new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                IgnoreComments = true
            };

            // reader object
            XmlReader xRead = XmlReader.Create(pathToFile, settings);

            int major=1, minor=0;
            string name="", fontName="";
            float fontSize=0f;

            if (xRead.ReadToDescendant("Configuration"))
            {
                while (xRead.Name=="Configuration")
                {
                    xRead.ReadStartElement("Configuration");
                    major = Helper.ConvertStringToIntegerSafely( 
                        xRead.ReadElementString("MajorVersion") 
                        );

                    minor = Helper.ConvertStringToIntegerSafely( 
                        xRead.ReadElementString("MinorVersion")
                        );

                    name = xRead.ReadElementString("VersionName");

                    fontName = xRead.ReadElementString("DefaultFontName");

                    fontSize = Helper.ConvertStringToFloatSafely(
                        xRead.ReadElementString("DefaultFontSize")
                        );

                    //finish reading
                    xRead.ReadEndElement();

                }
            }

            xRead.Close();

            //create the config object
            Configuration configuration = new Configuration(major, minor, 
                name, fontName, fontSize);


            return configuration;
        }

        /// <summary>
        /// SEts the configuration.
        /// </summary>
        /// <param name="configuration"></param>
        public static void SetConfiguration(string pathToFile, 
            Configuration configuration)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = " "
            };

            XmlWriter xmlWriter = XmlWriter.Create(pathToFile, settings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("FlySettings");

            xmlWriter.WriteStartElement("Configuration");
            xmlWriter.WriteElementString("MajorVersion", configuration.MajorVersion + "");
            xmlWriter.WriteElementString("MinorVersion", configuration.MinorVersion + "");
            xmlWriter.WriteElementString("VersionName", configuration.VersionName);
            xmlWriter.WriteElementString("DefaultFontName", configuration.DefaultFontName);
            xmlWriter.WriteElementString("DefaultFontSize", configuration.DefaultFontSize + "");

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.Close();


        }


    }
}
