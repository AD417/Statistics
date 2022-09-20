using System;
using System.IO;
namespace Statistics;

class World
{
    public static int Main()
    {
        System.Console.WriteLine("Loading data from file...");

        Population World = Population.ImportFromCSV("example1.csv");
        FrequencyDistribution dist = new FrequencyDistribution(World, 7);

        System.Console.WriteLine("Sample size: {0}", dist.Size);
        System.Console.WriteLine("Range: {0} ({1}-{2}).", dist.Range, dist.Min, dist.Max);
        System.Console.WriteLine("Class Size: {0}", dist.IntervalWidth);
        dist.PrintData();

        System.Console.WriteLine("Loading data from file...");

        World = Population.ImportFromCSV("example2.csv");
        dist = new FrequencyDistribution(World, 6);

        System.Console.WriteLine("Sample size: {0}", dist.Size);
        System.Console.WriteLine("Range: {0} ({1}-{2}).", dist.Range, dist.Min, dist.Max);
        System.Console.WriteLine("Class Size: {0}", dist.IntervalWidth);
        dist.PrintData();

        dist.ExportToCSV("Export.csv");

        return 0;
    }
}