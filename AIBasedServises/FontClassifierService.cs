namespace AIBasedServises;

static public class FontClassifierService
{
    static private Classifier _classifier = new Classifier();
    
    static public string ClassifyFontSize(double fontSize)
    {
        string res = "";
        foreach (var c in _classifier.getClasses())
        {
            if (c.higherBound < fontSize && c.lowerBound >= c.lowerBound )
            {
                res = c.label;    
            }
        }

        return res;
    }
}