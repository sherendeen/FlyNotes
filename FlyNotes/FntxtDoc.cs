using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FlyNotes
{
    /// <summary>
    /// This is the implementation of FntxtDoc, otherwise
    /// known as Flynotes Text Document format, verion 1.
    /// </summary>
    public class FntxtDoc
    {
        private string font;
        private float fontSize;
        private bool isBold, isItalicized, isStriken, isUnderlined;
        private string text;
        
        public string Font { get => font; set => font = value; }
        public float FontSize { get => fontSize; set => fontSize = value; }
        public bool IsBold { get => isBold; set => isBold = value; }
        public bool IsItalicized { get => isItalicized; set => isItalicized = value; }
        public bool IsStriken { get => isStriken; set => isStriken = value; }
        public bool IsUnderlined { get => isUnderlined; set => isUnderlined = value; }
        public string Text { get => text; set => text = value; }

        /// <summary>
        /// Font is set to Times New Roman
        /// Size is set to 12f. Everything else
        /// is set to false.
        /// </summary>
        public FntxtDoc()
        {
            this.font = "Times New Roman";
            this.fontSize = 12f;
            this.isBold = false;
            this.isItalicized = false;
            this.isStriken = false;
            this.isUnderlined = false;
        }

        public FntxtDoc(string font, float fontSize, bool isBold,
            bool isItalicized, bool isStriken, bool isUnderlined)
        {
            this.font = font;
            this.fontSize = fontSize;
            this.isBold = isBold;
            this.isItalicized = isItalicized;
            this.isStriken = isStriken;
            this.isUnderlined = isUnderlined;
        }

        public static FntxtDoc LoadFile(string pathToFile)
        {
            FntxtDoc fntxtDoc = new FntxtDoc();

            var settings = new XmlReaderSettings
            {
                IgnoreComments=true,
                IgnoreWhitespace=true
            };

            //create reader object
            XmlReader reader = XmlReader.Create(pathToFile, settings);

            if(reader.ReadToDescendant("Document"))
            {
                while(reader.Name=="Document")
                {
                    reader.ReadStartElement("Document");//document
                    reader.ReadStartElement("Settings");//settings
                    fntxtDoc.font = reader.ReadElementString("Font");
                    fntxtDoc.fontSize = Helper.ConvertStringToFloatSafely(reader.ReadElementString("FontSize"));
                    fntxtDoc.isBold = Helper.ConvertStringToBooleanSafely(reader.ReadElementString("IsBold"));
                    fntxtDoc.isItalicized = Helper.ConvertStringToBooleanSafely(reader.ReadElementString("IsItalicized"));
                    fntxtDoc.isStriken = Helper.ConvertStringToBooleanSafely(reader.ReadElementString("IsStriken"));
                    fntxtDoc.isUnderlined = Helper.ConvertStringToBooleanSafely(reader.ReadElementString("IsUnderlined"));
                    reader.ReadEndElement();//end settings
                    fntxtDoc.text = reader.ReadElementString("Text");
                    reader.ReadEndElement();//end document


                }
            }

            reader.Close();

            

            return fntxtDoc;
        }

        public static void SaveFile(FntxtDoc fntxtDoc)
        {

        }

    }
}
