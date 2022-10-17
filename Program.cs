namespace Statistics;

class Program
{
    public static int Main()
    {
        Population data = Population.ImportFromCSV("example1.csv");

        new DataSummary(data).Summarize();

        return 0;
    }
}