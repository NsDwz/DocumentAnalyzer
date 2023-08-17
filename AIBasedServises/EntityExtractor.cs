using Azure.AI.FormRecognizer.DocumentAnalysis;
using System.Numerics;

namespace AIBasedServises
{
    public class EntityExtractor
    {
        private AnalyzeResult result;


        public EntityExtractor(AnalyzeResult result)
        {
            this.result = result;
        }


        public double GetFontSize(DocumentParagraph p)
        {
            
            int paragraphWidth =  Math.Abs(((int)p.BoundingRegions[0].BoundingPolygon[1].Y) - ((int)p.BoundingRegions[0].BoundingPolygon[0].Y));
            int lineCount = GetLineCount(p);

            double fontSize = paragraphWidth / lineCount;

            return fontSize;
        }


        private int GetLineCount(DocumentParagraph p)
        {

            int count = 0;

            foreach (DocumentPage page in result.Pages)
            {
                foreach(DocumentLine line in page.Lines)
                {
                    if (p.Content.Contains(line.Content))
                        count++;
                }

            }
            return count;
        }


        


    }
}
