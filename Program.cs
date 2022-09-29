namespace Statistics;

class Program
{
    public static int Main()
    {
        System.Console.WriteLine("Loading Sample / Population data from file...");

        Population World = Population.ImportFromCSV("example1.csv");

        DataSummary data = new DataSummary(World);

        data.Summarize();

        return 0;
    }
}