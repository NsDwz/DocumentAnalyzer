namespace AIBasedServises;

public struct SizeClass
{
    public string label;
    public Double lowerBound;
    public Double higherBound;

    public SizeClass(
        string label,
        Double lowerBound,
        Double higherBound)
    {
        this.label = label;
        this.lowerBound = lowerBound;
        this.higherBound = higherBound;
    }
}