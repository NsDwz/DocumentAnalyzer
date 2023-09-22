namespace AIBasedServises;

public class Classifier
{
    private List<SizeClass> _classes; 
    
    public Classifier()
    {
        _classes = new List<SizeClass>();
        _classes.Add(new SizeClass("XXL", 100, Double.MaxValue));
        _classes.Add(new SizeClass("XL", 80, 100));
        _classes.Add(new SizeClass("L", 60, 80));
        _classes.Add(new SizeClass("M", 40, 60));
        _classes.Add(new SizeClass("S", 30, 40));
        _classes.Add(new SizeClass("XS", 10, 30));
        _classes.Add(new SizeClass("XXS", Double.MinValue, 10));
    }

    public List<SizeClass> getClasses()
    {
        return _classes;
    }

}