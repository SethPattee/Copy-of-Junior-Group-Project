using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace AutoShopAppLibrary.Data
{
    public class PDFGenerator
    {
        public PDFGenerator()
        {
        }

        public string GeneratePdf(Dictionary<string, string> formdetails)
        {
            string fileName = $"Form_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = $"./{fileName}";

            using (var writer = new PdfWriter(filePath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);

                    foreach (var item in formdetails)
                    {
                        // Adding content to the PDF    
                        document.Add(new Paragraph(item.Key + ": " + item.Value));

                    }
                }
                writer.Close();
            }

            return filePath;
            //https://www.gemboxsoftware.com/document/examples/c-sharp-vb-net-mail-merge-word/901
        }
    }
}