using System;
namespace Statistics;

class World
{
    public static int Main()
    {
        System.Console.WriteLine("Loading data from file...");

        Population World = Population.ImportFromCSV("testcat.csv");
        // Leafplot plot = new Leafplot(World);
        // plot.PrintPlot();
        FrequencyDistribution dist = new FrequencyDistribution(World, 7);

        System.Console.WriteLine("Sample size: {0}", dist.Size);
        System.Console.WriteLine("Range: {0} ({1}-{2}).", dist.Range, dist.Min, dist.Max);
        System.Console.WriteLine("Class Size: {0}", dist.IntervalWidth);
        dist.PrintChart();
        dist.DisplayHistogram();

        return 0;
    }
}