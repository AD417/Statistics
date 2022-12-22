namespace Statistics;

class NormalDistribution
{
    public double Mean {get; set; }
    public double Stdev {get; set; }
    public static String tab {get; set; } = "    ";

    public static int precision = 2;
    public static int Precision {
        set
        {
            if (value <= 0) throw new Exception("Invalid number of digits of precision!");
            precision = value;
        }
    }


    public NormalDistribution(double mean, double stdev)
    {
        Mean = mean;
        Stdev = stdev;
    }
    public NormalDistribution() : this(0, 1) {}

    public virtual bool AssertNormalDistribution() => true;

    public virtual void Summarize()
    {
        System.Console.WriteLine($"μ = {Math.Round(Mean, precision)}");
        System.Console.WriteLine($"σ = {Math.Round(Stdev, precision + 1)}");
    }

    public SampleDistribution toSampleDistribution(int sampleSize) 
        => new SampleDistribution(Mean, Stdev, sampleSize);

    public double FromZScore(double z)
    {
        return Mean + z * Stdev;
    }

    public static void setTabLength(int tabSize)
    {
        tab = "";
        for (int i = 0; i < tabSize; i++) tab += " ";
    }

    public virtual double ZScoreFor(double x)
    {
        double output = Math.Round((x - Mean) / Stdev, precision);
        System.Console.WriteLine($"z = (x - u) / s = ({x} - {Math.Round(Mean, precision)}) / {Math.Round(Stdev, precision + 1)} = {output} ");
        return output;
    }

    public virtual double LessThan(double x)
    {
        double z = ZScoreFor(x);
        System.Console.Write(tab); 
        System.Console.Write($"P(x < {x}) = "); 
        double probability = ZScore.LeftOf(z);
        System.Console.WriteLine();
        return probability;
    }

    public virtual double MoreThan(double x)
    {
        double z = ZScoreFor(x);
        System.Console.Write(tab); 
        System.Console.Write($"P(x > {x}) = "); 
        double probability = ZScore.RightOf(z);
        System.Console.WriteLine();
        return probability;
    }

    public virtual double Between(double minX, double maxX)
    {
        if (maxX < minX) return Between(maxX, minX);

        double minZ = ZScoreFor(minX);
        System.Console.Write(tab);
        double maxZ = ZScoreFor(maxX);
        System.Console.Write(tab); 
        System.Console.Write($"P({minX} < x < {maxX}) = "); 
        double probability = ZScore.Between(minZ, maxZ);  
        System.Console.WriteLine();
        return probability;
    }

    public virtual double Outside(double minX, double maxX)
    {
        if (maxX < minX) return Between(maxX, minX);

        double minZ = ZScoreFor(minX);
        System.Console.Write(tab);
        double maxZ = ZScoreFor(maxX);
        System.Console.Write(tab); 
        System.Console.Write($"P(x < {minX} or x > {maxX}) = "); 
        double probability = ZScore.Outside(minZ, maxZ);
        System.Console.WriteLine();
        return probability;
    }

    public double MaximumValueForP(double maxProbability)
    {
        double maxZScore = ZScore.FromProbability(maxProbability);
        double x = FromZScore(maxZScore);
        System.Console.WriteLine($"P(z' < z) = {Math.Round(maxProbability, ZScore.Precision)}");
        System.Console.Write(tab);
        System.Console.WriteLine($"z' = {maxZScore}");
        System.Console.Write(tab);
        System.Console.WriteLine($"x = μ + zσ = {x}");
        return x;
    }

    public void MinimumValueForP(double minProbability)
    {
        double minZScore = ZScore.FromProbability(minProbability);
        double x = FromZScore(minZScore);
        System.Console.WriteLine($"P(z' > z) = {Math.Round(minProbability, ZScore.Precision)}");
        System.Console.Write(tab);
        System.Console.WriteLine($"z' = {minZScore}");
        System.Console.Write(tab);
        System.Console.WriteLine($"x = μ + zσ = {FromZScore(minZScore)}");
        
    }

    public void TopPercent(double percentile)
    {
        double cutoffProbability = 1 - (percentile * 0.01);
        System.Console.WriteLine(
            $"Top {percentile}% = Better than {Math.Round(cutoffProbability, ZScore.Precision)} of data"
        );
        System.Console.Write(tab);
        MinimumValueForP(cutoffProbability);
    }

    public void BottomPercent(double percentile)
    {
        double cutoffProbability = (percentile * 0.01);
        System.Console.WriteLine(
            $"Bottom {percentile}% = At best {Math.Round(cutoffProbability, ZScore.Precision)} of data"
        );
        System.Console.Write(tab);
        MaximumValueForP(cutoffProbability);
    }

    public void MiddlePercent(double percentile)
    {
        double maxProbability = 0.5 + percentile / 200;
        double minProbability = 0.5 - percentile / 200;
        System.Console.WriteLine(
            $"Middle {percentile}% = Between {Math.Round(minProbability, ZScore.Precision)} and {Math.Round(maxProbability, ZScore.Precision)} of data"
        );
        System.Console.Write(tab);
        MaximumValueForP(maxProbability);
    }

    public int MinimumSampleForError(double confidence, double error)
    {
        if (confidence < 0 || confidence > 1) throw new Exception("Invalid confidence!");
        System.Console.WriteLine("n = (zc * σ / E)^2");
        System.Console.WriteLine(tab);
        double zConfidence = ZScore.CriticalValueForConfidence(confidence);
        double sqrtSample = zConfidence * Stdev / error;
        int minimumSample = (int) Math.Ceiling(sqrtSample * sqrtSample);
        System.Console.WriteLine($"n = ({zConfidence} * {Stdev} / {error})^2 = {minimumSample}");
        return minimumSample;
    }
}