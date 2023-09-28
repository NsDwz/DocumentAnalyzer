using Azure.AI.FormRecognizer.DocumentAnalysis;
using DocumentEstraction.Types;

namespace AIBasedServises
{
    public class DocumentModel
    {
        private HtsResult result;

        public DocumentModel(HtsResult result)
        {
            this.result = result;
        }

        public double GetFontSize(HtsParagraph p)
        {
            double paragraphWidth =  Math.Abs((p.BoundingRegions[0].BoundingPolygon[2].Y) - (p.BoundingRegions[0].BoundingPolygon[0].Y));
            int lineCount = GetLineCount(p);

            double fontSize = paragraphWidth / lineCount;

            return Math.Round(fontSize, 2, MidpointRounding.AwayFromZero);
        }


        private int GetLineCount(HtsParagraph p)
        {
            int count = 0;

            foreach (HtsPage page in result.Pages)
            {
                foreach (HtsLine line in page.Lines)
                {
                    if (p.Content.Contains(line.Content))
                        count++;
                }
            }
            return count;
        }


        public List<HtsPage> GetPages()
        {
            return result.Pages;
        }

        public List<HtsParagraph> GetParagraphs()
        {
            return result.Paragraphs;
        }

        public List<HtsLine> GetLines()
        {
            List<HtsLine> lines = new List<HtsLine>();
            foreach (HtsPage page in result.Pages)
            {
                lines.AddRange(page.Lines);
            }
            return lines;
        }

        public List<HtsWord> GetWords()
        {
            List<HtsWord> words = new List<HtsWord>();
            foreach (HtsPage page in result.Pages)
            {
                words.AddRange(page.Words);
            }
            return words;
        }
        
        public static void Iterate(dynamic variable)
        {
            if (variable.GetType() == typeof(Newtonsoft.Json.Linq.JObject))
            {
                Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
                foreach(var property in variable){
                    Console.WriteLine("property name: "+property.Name.ToString());
                    Console.WriteLine("property type: "+property.GetType().ToString());	
                    Iterate(property.Value);
                }
            }else if (variable.GetType() == typeof(Newtonsoft.Json.Linq.JArray))
            {
                Console.WriteLine("type is Array");
                foreach(var item in variable){
                    Iterate(item);	
                }
            }
            else if (variable.GetType() == typeof(Newtonsoft.Json.Linq.JValue))
            {
                Console.WriteLine("type is Variable, value: "+variable.ToString());
            }
        }
    }
}
