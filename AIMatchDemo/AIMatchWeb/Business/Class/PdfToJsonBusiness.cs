using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System.Diagnostics.Metrics;
using System.Text;
using Newtonsoft.Json;
using AIMatchWeb.Models;
using AIMatchWeb.Business.Interfaces;

namespace AIMatchWeb.Business.Class
{
    public class PdfToJsonBusiness : IPdfToJsonBusiness
    {
        public string ConvertPDFToJson(string filePath,string fileName)
        {
            var pdfText = new StringBuilder();

            using (var reader = new PdfReader(filePath))
            {
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    var text = PdfTextExtractor.GetTextFromPage(reader, page);
                    pdfText.AppendLine(text);
                }
            }

            var result = new PdfResultModel
            {
                FileName = fileName,
                Content = pdfText.ToString()
            };

           return JsonConvert.SerializeObject(result);
        }
    }
}
