namespace PearCruncher;

public static class Logger
{
    public static void ExceptionHandler(Exception e)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        string logPath = Path.Combine(Directory.GetCurrentDirectory(), "logs");
        string fileName = Path.Combine(logPath, $"{DateTime.Now:MM-dd-yyyy hh_mm_ss_fff}.log");

        Console.Clear();
        Console.WriteLine($"\nERROR! Please send this crash log with any error reports!\nException:\n{e.Message}\n{e.StackTrace}");

        Console.WriteLine($"\nCrash logged to {fileName}.\n");
        
        // this checks if directory exists already so no need to validate
        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "logs"));

        using StreamWriter logger = new(fileName);
        logger.Write($"Exception:\n{e.Message}\n{e.StackTrace}");
    }
}