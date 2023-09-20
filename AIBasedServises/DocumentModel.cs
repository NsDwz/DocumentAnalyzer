using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace AIBasedServises
{
    public class DocumentModel
    {
        private AnalyzeResult result;


        public DocumentModel(AnalyzeResult result)
        {
            this.result = result;
        }


        public double GetFontSize(DocumentParagraph p)
        {
            
            double paragraphWidth =  Math.Abs((p.BoundingRegions[0].BoundingPolygon[2].Y) - (p.BoundingRegions[0].BoundingPolygon[0].Y));
            int lineCount = GetLineCount(p);

            double fontSize = paragraphWidth / lineCount;

            return Math.Round(fontSize, 2, MidpointRounding.AwayFromZero); ;
        }


        private int GetLineCount(DocumentParagraph p)
        {

            int count = 0;

            foreach (DocumentPage page in result.Pages)
            {
                foreach (DocumentLine line in page.Lines)
                {
                    if (p.Content.Contains(line.Content))
                        count++;
                }

            }
            return count;
        }


        public List<DocumentPage> GetPages()
        {
            return result.Pages.ToList();
        }

        public List<DocumentParagraph> GetParagraphs()
        {
            return result.Paragraphs.ToList();
        }

        public List<DocumentLine> GetLines()
        {
            List<DocumentLine> lines = new List<DocumentLine>();
            foreach (DocumentPage page in result.Pages)
            {
                lines.AddRange(page.Lines);
            }
            return lines;
        }

        public List<DocumentWord> GetWords()
        {
            List<DocumentWord> words = new List<DocumentWord>();
            foreach (DocumentPage page in result.Pages)
            {
                words.AddRange(page.Words);
            }
            return words;
        }
    }
}
