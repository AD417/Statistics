namespace Statistics;

class SampleDistribution : NormalDistribution
{
    public int SampleSize;
    public bool fromNormalDistribution = false;
    public String population = "__SAMPLED__";
    public String unit = "__UNITS__";
    private static Random Generator = new Random(0);
    public SampleDistribution(Set data, int trials, int sampleSize = 30) : base(0,0)
    {
        int len = data.MemberCount;
        List<double> samples = new List<double>();
        double sampleSum;
        samples.EnsureCapacity(trials);
        for (int i = 0; i < trials; i++)
        {   
            sampleSum = 0;
            for (int j = 0; j < SampleSize; j++)
            {
                sampleSum += data.Members[Generator.Next(len)].MagicNumber;
            }
            samples.Add(sampleSum);
        }
        Mean = samples.Sum() / samples.Count();
        Stdev = 1; // TODO: FIX THIS CRAP!!!
        SampleSize = sampleSize;
    }

    public SampleDistribution(double mean, double stdev, int sampleSize) : base(mean, stdev / Math.Sqrt(sampleSize)) 
    {
        SampleSize = sampleSize;
        fromNormalDistribution = true;
        if (sampleSize < 30) System.Console.WriteLine("WARNING: n < 30. Make sure the data is normally distributed!"); 
    }
    public SampleDistribution(double mean, double stdev) : base(mean, stdev) {
        SampleSize = 1;
        fromNormalDistribution = true;
    }

    public override void Summarize()
    {
        
        System.Console.WriteLine($"μ = {Mean}  (n = {SampleSize})  {Mean}");
        System.Console.WriteLine($"σ = {Math.Round(Stdev * Math.Sqrt(SampleSize), precision + 1)} --------> {Math.Round(Stdev, precision + 1)}");
    }

    public override bool AssertNormalDistribution() => fromNormalDistribution || SampleSize >= 30;

    public static void ResetGenerator(int seed)
    {
        Generator = new Random(seed);
    }
    public static void ResetGenerator() => ResetGenerator(DateTime.Now.Millisecond);

    public double MarginOfError(double confidence)
    {
        if (confidence < 0 || confidence > 1) throw new Exception("Invalid confidence!");
        if (!AssertNormalDistribution()) {
            System.Console.WriteLine("This dataset is not a distribution, aborting...");
            return 0;
        }
        double zCritical = ZScore.CriticalValueForConfidence(confidence);
        double error = zCritical * Stdev;

        System.Console.Write(tab); System.Console.WriteLine($"E = zc * σ = {zCritical} * {Math.Round(Stdev,precision)} = {Math.Round(error,precision)}");
        return error;
    }

    public (double min, double max) ConfidenceIntervalFor(double confidence, bool makeInference = false)
    {
        if (confidence < 0 || confidence > 1) throw new Exception("Invalid confidence!");
        double error = MarginOfError(confidence);
        double min = Mean - error;
        double max = Mean + error;
        System.Console.Write(tab); System.Console.WriteLine($"P(x - E < μ < x + E) = {confidence}");
        System.Console.Write(tab); System.Console.WriteLine($"P({Math.Round(min, precision)} < μ < {Math.Round(max, precision)}) = {confidence}");
        if (makeInference)
        {
            System.Console.Write(tab);
            System.Console.WriteLine($"With {confidence * 100}% confidence, it can be said that the mean {population} is between {Math.Round(min, precision)} and {Math.Round(max, precision)} {unit}.");
        }
        System.Console.WriteLine();
        return (min, max);
    }

    public static (double mean, double error) DetermineIntervals(double min, double max, double confidence)
    {
        if (confidence < 0 || confidence > 1) throw new Exception("Invalid confidence!");
        if (min > max) return DetermineIntervals(max, min, confidence);

        double mean = 0.5 * (max + min);
        double error = 0.5 * (max - min);
        return (mean, error);
    }
}