using System.Globalization;
// ReSharper disable CompareOfFloatsByEqualityOperator
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

namespace PearCruncher;

public static class Grade
{
    public static void AddCategory(Dictionary<string, Category> category)
    {
        try 
        {
            Console.Write("\nEnter the name of the category: ");
            string attemptKey = Console.ReadLine();

            Console.Write("Enter the weight of the category: ");
            bool parseWeight = int.TryParse(Console.ReadLine(), out int outWeight);
            switch (parseWeight)
            {
                case true:
                    if (attemptKey != null && !category.ContainsKey(attemptKey))
                    {
                        category.Add(attemptKey, new Category
                        {
                            GradeValues = new List<double>(),
                            Weight = outWeight
                        });
                        Console.WriteLine();

                        SetValues(category, attemptKey);
                    }
                    else
                    {
                        Console.WriteLine("Duplicate input!\n");
                    }
                    break;
                case false:
                    Console.WriteLine("Invalid input!");
                    break;
            }
        }
        catch (Exception e)
        {
            Logger.ExceptionHandler(e);
        }
    }

    // SetValues has 2 overloads. Both take a dictionary, one allows you to pass in a dictionary key automatically.
    // Other overload manually prompts for key entry and displays choices.
    private static void SetValues(Dictionary<string, Category> category, string key)
    {
        try 
        {
            Console.Write("Enter the number of marked assignments: ");
            bool parseNum = int.TryParse(Console.ReadLine(), out int numAssignments);

            switch (parseNum)
            {
                case true:
                if (category.ContainsKey(key))
                {
                    category[key].GradeValues = new(numAssignments);
                    for (int i = 0; i < numAssignments; i++)
                    {
                        Console.Write($"Enter the grade of assignment {i + 1} (U for incomplete/unmarked): ");
                        var attemptGrade = Console.ReadLine();
                        switch (double.TryParse(attemptGrade, out var result))
                        {
                            case true:
                                if (result is >= 0 and <= 100)
                                {
                                    category[key].GradeValues.Add(result);
                                }
                                else
                                {
                                    Console.WriteLine("INVALID INPUT! PLEASE REDO THIS CATEGORY!");
                                    SetValues(category, key);
                                }

                                break;
                            case false:
                                if (attemptGrade == "U")
                                {
                                    // -1 = unfinished signal
                                    category[key].GradeValues.Add(-1);
                                }
                                else
                                {
                                    Console.WriteLine("INVALID INPUT! PLEASE REDO THIS CATEGORY!");
                                    SetValues(category, key);
                                }

                                break;
                        }
                    }

                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Invalid Category!");
                    SetValues(category, key);
                }
                break;
            }
        }
        catch (Exception e)
        {
            Logger.ExceptionHandler(e);
        }
    }

    public static void SetValues(Dictionary<string, Category> category)
    {
        try 
        {
            if (!category.Any())
            {
                Console.Clear();
                Console.WriteLine($"No data to reset!");
                Thread.Sleep(2000);
                Console.Clear();
                return;
            }
            
            Console.WriteLine($"\nAvailable Categories:");

            foreach (KeyValuePair<string, Category> entry in category)
            {
                Console.WriteLine($"- {entry.Key}");
            }

            Console.Write("\nEnter the name of the category: ");
            string key = Console.ReadLine();

            Console.Write("Enter the number of marked assignments: ");
            bool num = int.TryParse(Console.ReadLine(), out int assignmentNums);

            switch (num)
            {
                case true:
                if (key != null && category.ContainsKey(key))
                {
                    category[key].GradeValues = new List<double>(assignmentNums);
                    for (int i = 0; i < assignmentNums; i++)
                    {
                        Console.Write($"Enter the grade of assignment {i + 1} (U for incomplete/unmarked): ");
                        var attemptGrade = Console.ReadLine();
                        switch (double.TryParse(attemptGrade, out var result))
                        {
                            case true:
                                if (result is >= 0 and <= 100)
                                {
                                    category[key].GradeValues.Add(result);
                                }
                                else
                                {
                                    Console.WriteLine("INVALID INPUT! PLEASE REDO THIS CATEGORY!");
                                }

                                break;
                            case false:
                                if (attemptGrade == "U")
                                {
                                    // -1 = unfinished signal
                                    category[key].GradeValues.Add(-1);
                                }
                                else
                                {
                                    Console.WriteLine("INVALID INPUT! PLEASE REDO THIS CATEGORY!");
                                }

                                break;
                        }
                    }

                    // filler
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Invalid Category!");
                    if (key != null) SetValues(category, key);
                }
                break;
                case false:
                    Console.WriteLine("Invalid input!");
                    break;
            }
        }
        catch (Exception e)
        {
            Logger.ExceptionHandler(e);
        }
        
    }

    public static void PrintValues(Dictionary<string, Category> category)
    {
        try
        {
            if (!category.Any())
            {
                Console.Clear();
                Console.WriteLine("No data to display!");
                Thread.Sleep(2000);
                Console.Clear();
                return;
            }
            
            Console.Clear();
            
            double totalWeight = 0;
            double cumulativeWeight = 0;

            double minWeight = 0;
            double maxWeight = 0;

            foreach (KeyValuePair<string, Category> entry in category)
            {
                Console.WriteLine($"{entry.Key} - Weight: {entry.Value.Weight}");
                Console.WriteLine($"  Grades: \n     - {string.Join("\n     - ", entry.Value.GradeValues.Select(g => g == -1 ? "Unfinished/Unmarked" : g.ToString(CultureInfo.CurrentCulture)))}");

                // takes into consideration unfinished grades
                var processedGrades = entry.Value.GradeValues.Where(g => g != -1).ToList();
                double average = processedGrades.Any() ? processedGrades.Average() : 0;

                // calculate earned grade
                entry.Value.EarnedWeight = entry.Value.Weight * (average / 100);
                cumulativeWeight += entry.Value.EarnedWeight;

                // It's been months since I wrote this and I have no clue what it does lmao
                var minGrades = entry.Value.GradeValues.Select(g => g == -1 ? 0 : g).ToList();
                var maxGrades = entry.Value.GradeValues.Select(g => g == -1 ? 100 : g).ToList();

                // a major bug here before wasn't checking any before average
                double minAverage = minGrades.Any() ? minGrades.Average() : 0;
                double maxAverage = maxGrades.Any() ? maxGrades.Average() : 0;

                minWeight += entry.Value.Weight * (minAverage / 100);
                maxWeight += entry.Value.Weight * (maxAverage / 100);

                Console.WriteLine($"Average Grade in {entry.Key} = {average:N2}");
                Console.WriteLine($"Earned Weight in {entry.Key} = {entry.Value.EarnedWeight:N2} / {entry.Value.Weight}\n");

                totalWeight += entry.Value.Weight;
            }

            var grade = totalWeight > 0 ? (cumulativeWeight / totalWeight) * 100 : 0;
            var minGrade = totalWeight > 0 ? (minWeight / totalWeight) * 100 : 0;
            var maxGrade = totalWeight > 0 ? (maxWeight / totalWeight) * 100 : 0;

            Console.WriteLine($"------------------------------------------------");
            Console.WriteLine($"Cumulative (to-date) Earned Weight: {cumulativeWeight:N2}/{totalWeight}");
            Console.WriteLine($"Current Grade Mark: {grade:N2}% ({Conversions.LetterGrade(grade)}, {Conversions.GradePoint(grade)})");
            Console.WriteLine($"Hypothetical Minimum Mark: {minGrade:N2}% ({Conversions.LetterGrade(minGrade)}, {Conversions.GradePoint(minGrade)})");
            Console.WriteLine($"Hypothetical Maximum Mark: {maxGrade:N2}% ({Conversions.LetterGrade(maxGrade)}, {Conversions.GradePoint(maxGrade)})");
            Console.WriteLine($"------------------------------------------------\n");
        }
        catch (Exception e)
        {
            Logger.ExceptionHandler(e);
        }
    }
}