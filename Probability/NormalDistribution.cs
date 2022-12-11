namespace Statistics;

class NormalDistribution
{
    public double Mean {get; set; }
    public double Stdev {get; set; }
    private static String tab {get; set; } = "    ";

    protected static int precision = 2;
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
        System.Console.WriteLine(); // Free newline.
        System.Console.WriteLine($"μ = {Mean}");
        System.Console.WriteLine($"σ = {Math.Round(Stdev, precision + 1)}");
        System.Console.WriteLine();
    }
    public NormalDistribution() : this(0, 1) {}

    public double FromZScore(double z)
    {
        return Mean + z * Stdev;
    }

    public static void setTabLength(int tabSize)
    {
        tab = "";
        for (int i = 0; i < tabSize; i++) tab += " ";
    }

    public double ZScoreFor(double x)
    {
        double output = Math.Round((x - Mean) / Stdev, precision);
        System.Console.WriteLine($"z = (x - u) / s = ({x} - {Mean}) / {Math.Round(Stdev, precision + 1)} = {output} ");
        return output;
    }

    public double LessThan(double x)
    {
        double z = ZScoreFor(x);
        System.Console.Write(tab); 
        System.Console.Write($"P(x < {x}) = "); 
        double probability = ZScore.LeftOf(z);
        System.Console.WriteLine();
        return probability;
    }

    public double MoreThan(double x)
    {
        double z = ZScoreFor(x);
        System.Console.Write(tab); 
        System.Console.Write($"P(x > {x}) = "); 
        double probability = ZScore.RightOf(z);
        System.Console.WriteLine();
        return probability;
    }

    public double Between(double minX, double maxX)
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

    public double Outside(double minX, double maxX)
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
}