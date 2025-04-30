namespace PearCruncher;

internal static class Program
{
    // this code is atrocious
    public static void Main()
    {
        // string = id, value = category object
        Dictionary<string, Category> categories = new();

        bool doRun = true;

        do
        {
            PrintLogo();

            try
            {
                char selection = Menu();
                switch (selection)
                {
                    case '1':
                        Grade.AddCategory(categories);
                        Console.Clear();
                        break;
                    case '2':
                        Grade.SetValues(categories);
                        Console.Clear();
                        break;
                    case '3':
                        Grade.PrintValues(categories);
                        doRun = false;
                        break;
                    case '0':
                        doRun = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid Action!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine($"- {e.Message.ToUpper()} -");
                Thread.Sleep(2000);
                Console.Clear();
            }
        } while (doRun);
    }

    private static char Menu()
    {
        Console.WriteLine("+------------------------------+");
        Console.WriteLine("| 1. Add and set categories    |");
        Console.WriteLine("| 2. Reset values manually     |");
        Console.WriteLine("| 3. Print grade table         |");
        Console.WriteLine("|                              |");
        Console.WriteLine("| 0. Exit                      |");
        Console.WriteLine("+------------------------------+");
        Console.Write("\nOption -> ");

        bool attemptOpt = char.TryParse(Console.ReadLine(), out char parseOut);
        switch (attemptOpt)
        {
            case true:
                return parseOut;
            case false:
                throw new InvalidOperationException("Invalid entry format!");
        }
    }

    private static void PrintLogo()
    {
        // this code is ugly, but it's so pretty :)
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" /$$$$$$$                                /$$$$$$                                          /$$                          ");
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("| $$__  $$                              /$$__  $$                                        | $$                          ");
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("| $$  \\ $$ /$$$$$$   /$$$$$$   /$$$$$$ | $$  \\__/  /$$$$$$  /$$   /$$ /$$$$$$$   /$$$$$$$| $$$$$$$   /$$$$$$   /$$$$$$ ");
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("| $$$$$$$//$$__  $$ |____  $$ /$$__  $$| $$       /$$__  $$| $$  | $$| $$__  $$ /$$_____/| $$__  $$ /$$__  $$ /$$__  $$");
        
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("| $$____/| $$$$$$$$  /$$$$$$$| $$  \\__/| $$      | $$  \\__/| $$  | $$| $$  \\ $$| $$      | $$  \\ $$| $$$$$$$$| $$  \\__/");
        
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("| $$     | $$_____/ /$$__  $$| $$      | $$    $$| $$      | $$  | $$| $$  | $$| $$      | $$  | $$| $$_____/| $$      ");
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("| $$     |  $$$$$$$|  $$$$$$$| $$      |  $$$$$$/| $$      |  $$$$$$/| $$  | $$|  $$$$$$$| $$  | $$|  $$$$$$$| $$      ");
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("|__/      \\_______/ \\_______/|__/       \\______/ |__/       \\______/ |__/  |__/ \\_______/|__/  |__/ \\_______/|__/      ");

        Console.ResetColor();
        
        Console.WriteLine();
    }
}
