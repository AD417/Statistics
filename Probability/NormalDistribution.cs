namespace Statistics;

class NormalDistribution
{
    public double Mean {get; }
    public double Stdev {get; }

    private bool showWork {get; } = false;
    private String tab {get; set; } = "    ";
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
            System.Console.WriteLine($"μ = {Mean}");
            System.Console.WriteLine($"σ = {Stdev}");
            System.Console.WriteLine();
        }
    }

    public void setTabLength(int tabSize)
    {
        tab = "";
        for (int i = 0; i < tabSize; i++) tab += " ";
    }

    public double ZScoreFor(double x)
    {
        double output = Math.Round((x - Mean) / Stdev, 2);
        if (showWork) System.Console.WriteLine($"z = (z - u) / s = ({x} - {Mean}) / {Stdev} = {output} ");
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
}