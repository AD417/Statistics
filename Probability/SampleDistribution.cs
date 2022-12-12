namespace Statistics;



class SampleDistribution : NormalDistribution
{
    private static Random Generator = new Random(0);
    public SampleDistribution(Set data, int trials, int countPerTrial = 30) : base(0,0)
    {
        int len = data.MemberCount;
        List<double> samples = new List<double>();
        double sampleSum;
        samples.EnsureCapacity(trials);
        for (int i = 0; i < trials; i++)
        {   
            sampleSum = 0;
            for (int j = 0; j < countPerTrial; j++)
            {
                sampleSum += data.Members[Generator.Next(len)].MagicNumber;
            }
            samples.Add(sampleSum);
        }
        Mean = samples.Sum() / samples.Count();
        Stdev = 1; // TODO: FIX THIS CRAP!!!
    }

    public SampleDistribution(double mean, double stdev, int sampleSize) : base(mean, stdev / Math.Sqrt(sampleSize)) 
    {
        if (sampleSize < 30) System.Console.WriteLine("WARNING: n < 30. Make sure the data is normally distributed!"); 
        System.Console.WriteLine($"μ = {mean}  (n = {sampleSize})  {Mean}");
        System.Console.WriteLine($"σ = {Math.Round(stdev, precision + 1)} --------> {Math.Round(Stdev, precision + 1)}");
    }
    public SampleDistribution(double mean, double stdev) : base(mean, stdev) {}

    public SampleDistribution ConvertToSampleDistribution(NormalDistribution nd, int trials)
    {
        return new SampleDistribution(nd.Mean, nd.Stdev, trials);
    }

    public static void ResetGenerator(int seed)
    {
        Generator = new Random(seed);
    }
    public static void ResetGenerator() => ResetGenerator(DateTime.Now.Millisecond);
}