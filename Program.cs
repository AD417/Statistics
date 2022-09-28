namespace Statistics;

class Program
{
    public static int Main()
    {
        System.Console.WriteLine("Loading Sample / Population data from file...");

        Population World = Population.ImportFromCSV("example1.csv");

        Leafplot plot = new Leafplot(World);
        plot.PrintPlot();

        return 0;
    }
}