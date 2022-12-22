namespace Statistics;

class Program
{
    public static int Main(string[] args)
    {
        SampleDistribution.setTabLength(7);
        SampleDistribution sd;

        sd = new SampleDistribution(3.63, 0.21, 48);
        sd.population = "cost of gasoline";
        sd.unit = "dollars";
        sd.Summarize();
        System.Console.Write("35) a) "); sd.ConfidenceIntervalFor(0.9, true);
        System.Console.Write("    b) "); sd.ConfidenceIntervalFor(0.95, true);

        sd = new SampleDistribution(23, 6.7, 36);
        sd.Summarize();
        System.Console.Write("36) a) "); sd.ConfidenceIntervalFor(0.9, true);
        System.Console.Write("    b) "); sd.ConfidenceIntervalFor(0.95, true);

        SampleDistribution.setTabLength(4);

        sd = new SampleDistribution(2650, 425, 50);
        sd.Summarize();
        System.Console.Write("37) "); sd.ConfidenceIntervalFor(0.95, true);

        sd = new SampleDistribution(2650, 425, 80);
        sd.Summarize();
        System.Console.Write("39) "); sd.ConfidenceIntervalFor(0.95, true);

        sd = new SampleDistribution(2650, 375, 50);
        sd.Summarize();
        System.Console.Write("41) "); sd.ConfidenceIntervalFor(0.95, true);

        sd = new SampleDistribution(150, 15.5, 60);
        sd.Summarize();
        System.Console.Write("38) "); sd.ConfidenceIntervalFor(0.99, true);

        sd = new SampleDistribution(150, 15.5, 40);
        sd.Summarize();
        System.Console.Write("40) "); sd.ConfidenceIntervalFor(0.99, true);

        return 0;
    }
}