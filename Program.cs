namespace Statistics;

class Program
{
    public static int Main()
    {
        System.Console.WriteLine("Loading Sample / Population data from file...");

        Population World = Population.ImportFromCSV("example1.csv");
        Leafplot plot = new Leafplot(World);
        plot.PrintPlot();
        // FrequencyDistribution dist = new FrequencyDistribution(World, 6);
// 
        // System.Console.WriteLine("Sample size: {0}", dist.Size);
        // System.Console.WriteLine("Range: {0} ({1}-{2}).", dist.Range, dist.Min, dist.Max);
        // System.Console.WriteLine("Class Size: {0}", dist.IntervalWidth);
        // dist.PrintChart();
        // dist.DisplayFrequencyHistogram();
        // dist.DisplayRelativeFrequencyHistogram();
        // dist.DisplayFrequencyLineGraph();
        // dist.DisplayOgive();
        // System.Console.WriteLine("Saving Frequency Distribution to file...");
        // dist.ExportToCSV("Export2.csv");

        return 0;
    }
}