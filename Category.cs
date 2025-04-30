namespace PearCruncher;

public class Category
{
    public required List<double> GradeValues { get; set; }
    public double AverageGrade { get; set; }
    public int Weight { get; init; }
    public double EarnedWeight { get; set; }
}
