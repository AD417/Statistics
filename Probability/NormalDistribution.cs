namespace Statistics;

class NormalDistribution
{
    public double Mean {get; }
    public double Stdev {get; }

    private bool showWork {get; } = false;
    private String tab {get; set; } = "    ";

    private static int precision = 4;
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

    public NormalDistribution(double mean, double stdev, bool _showWork) : this(mean, stdev)
    {
        showWork = _showWork;
        if (showWork)
        {
            System.Console.WriteLine(); // Free newline.
            System.Console.WriteLine($"μ = {Mean}");
            System.Console.WriteLine($"σ = {Stdev}");
            System.Console.WriteLine();
        }
    }

    public double FromZScore(double z)
    {
        return Mean + z * Stdev;
    }

    public void setTabLength(int tabSize)
    {
        tab = "";
        for (int i = 0; i < tabSize; i++) tab += " ";
    }

    public double ZScoreFor(double x)
    {
        double output = Math.Round((x - Mean) / Stdev, precision);
        if (showWork) System.Console.WriteLine($"z = (x - u) / s = ({x} - {Mean}) / {Stdev} = {output} ");
        return output;
    }

    public void LessThan(double x)
    {
        double z = ZScoreFor(x);
        if (showWork) System.Console.Write(tab); 
        System.Console.Write($"P(x < {x}) = "); ZScore.LeftOf(z);
        System.Console.WriteLine();
    }

    public void MoreThan(double x)
    {
        double z = ZScoreFor(x);
        if (showWork) System.Console.Write(tab); 
        System.Console.Write($"P(x > {x}) = "); ZScore.RightOf(z);
        System.Console.WriteLine();
    }

    public void Between(double minX, double maxX)
    {
        if (maxX < minX) 
        {
            Between(maxX, minX);
            return;
        }
        double minZ = ZScoreFor(minX);
        if (showWork) System.Console.Write(tab);
        double maxZ = ZScoreFor(maxX);
        if (showWork) System.Console.Write(tab); 
        System.Console.Write($"P({minX} < x < {maxX}) = "); ZScore.Between(minZ, maxZ);  
        System.Console.WriteLine();
    }

    public void Outside(double minX, double maxX)
    {
        if (maxX < minX) 
        {
            Between(maxX, minX);
            return;
        }
        double minZ = ZScoreFor(minX);
        if (showWork) System.Console.Write(tab);
        double maxZ = ZScoreFor(maxX);
        if (showWork) System.Console.Write(tab); ZScore.Between(minZ, maxZ);
        System.Console.WriteLine();
    }

    public void MaximumValueForP(double maxProbability)
    {
        double maxZScore = ZScore.FromProbability(maxProbability);

        System.Console.WriteLine($"P(z' < z) = {maxProbability}");
        System.Console.Write(tab);
        System.Console.WriteLine($"z' = {maxZScore}");
        System.Console.Write(tab);
        System.Console.WriteLine($"x = μ + zσ = {FromZScore(maxZScore)}");
    }

    public void MinimumValueForP(double minProbability)
    {
        double minZScore = ZScore.FromProbability(minProbability);

        System.Console.WriteLine($"P(z' > z) = {minProbability}");
        System.Console.Write(tab);
        System.Console.WriteLine($"z' = {minZScore}");
        System.Console.Write(tab);
        System.Console.WriteLine($"x = μ + zσ = {FromZScore(minZScore)}");
    }

    public void TopPercent(double percentile)
    {
        double cutoffProbability = 1 - (percentile * 0.01);
        System.Console.WriteLine($"Top {percentile}% = Better than {cutoffProbability} of data");
        System.Console.Write(tab);
        MinimumValueForP(cutoffProbability);
    }

    public void BottomPercent(double percentile)
    {
        double cutoffProbability = (percentile * 0.01);
        System.Console.WriteLine($"Bottom {percentile}% = At best {cutoffProbability} of data");
        System.Console.Write(tab);
        MaximumValueForP(cutoffProbability);
    }
}