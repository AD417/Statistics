namespace Statistics;

class Program
{
    public static int Main()
    {
        int[] weights = {1, 2, 3, 4, 5, 6, 5, 4, 3, 2, 1};
        Sample data = ExperimentalProbability.WeightedOutcomeTrial(weights, 10000);

        // System.Console.WriteLine(data.MagicNumbersAsPrintable());

        new DataSummary(data).Summarize();

        // FrequencyDistribution fd = new FrequencyDistribution(data, 7);
        // fd.DisplayCumulativeFrequencyLineGraph();

        return 0;
    }
}