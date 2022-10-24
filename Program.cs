namespace Statistics;

class Program
{
    public static int Main()
    {
        Sample data = Sample.ImportFromCSV("example1.csv");

        System.Console.WriteLine(data.MagicNumbersAsPrintable());

        new DataSummary(data).Summarize();

        // FrequencyDistribution fd = new FrequencyDistribution(data, 7);
        // fd.DisplayCumulativeFrequencyLineGraph();

        return 0;
    }
}