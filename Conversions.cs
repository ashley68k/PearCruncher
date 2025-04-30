namespace PearCruncher;

public static class Conversions
{
    public static string LetterGrade(double grade)
    {
        return grade switch
        {
            >= 0 and < 50 => "F",
            >= 50 and < 55 => "D",
            >= 55 and < 60 => "D+",
            >= 60 and < 65 => "C",
            >= 65 and < 70 => "C+",
            >= 70 and < 75 => "B",
            >= 75 and < 80 => "B+",
            >= 80 and < 90 => "A",
            >= 90 and <= 100 => "A+",
            _ => "INVALID!"
        };
    }

    public static double GradePoint(double grade)
    {
        return grade switch
        {
            >= 0 and < 50 => 0.0,
            >= 50 and < 55 => 1.0,
            >= 55 and < 60 => 1.5,
            >= 60 and < 65 => 2.0,
            >= 65 and < 70 => 2.5,
            >= 70 and < 75 => 3.0,
            >= 75 and < 80 => 3.5,
            >= 80 and < 90 => 4.0,
            >= 90 and <= 100 => 4.5,
            _ => -1
        };
    }
}