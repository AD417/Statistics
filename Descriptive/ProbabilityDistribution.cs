using Plotly.NET.CSharp;

namespace Statistics;

class ProbabilityDistribution 
{
    public bool IsDiscrete {get; }
    public bool IsContinuous {get => !IsDiscrete; }
    public bool IsBinomial {get; }

    public int[] Intervals {get; }
    public int MemberCount {get; }

    public double[] Probability {get; }
    public double[] CumulativeProbability {get; }

    public ProbabilityDistribution(int[] outcomes, double[] probability) : this(probability)
    {
        if (outcomes.Length != probability.Length) 
            throw new Exception("Dimension Mismatch: invalid probability distribution constructor.");
        Intervals = outcomes;
    }

    public ProbabilityDistribution(double[] probability)
    {

        IsDiscrete = false;
        MemberCount = probability.Length;
        Intervals = new int[MemberCount];

        for (int i = 0; i < MemberCount; i++) Intervals[i] = i;

        Probability = probability;
        
        double cumProb = 0.0;
        CumulativeProbability = new double[MemberCount];
        for (int i = 0; i < MemberCount; i++)
        {
            cumProb += Probability[i];
            CumulativeProbability[i] = cumProb;
        }

    }

    public bool Validate() 
    {
        foreach (double chance in Probability) 
            if (chance < 0 || chance > 1) return false;
        return CumulativeProbability[MemberCount - 1] == 1;
    }

    public double ProbabilityBetween(int min, int max)
    {
        // if (min == max) return Probability[max];
        if (max <= min) return 0.0;
        
        int minIndex = -1, maxIndex = -1;
        for (int i = 0; i < MemberCount; i++)
        {
            if (minIndex == -1 && Intervals[i] >= min) minIndex = i;
            if (Intervals[i] >= max)
            {
                maxIndex = i;
                break;
            }
        }
        return CumulativeProbability[maxIndex] - CumulativeProbability[minIndex] + Probability[minIndex];
    }

    public double AtMost(int max) => ProbabilityBetween(0, max);
    public double LessThan(int max) => ProbabilityBetween(0, max - 1);
    public double AtLeast(int min) => ProbabilityBetween(min, MemberCount - 1);
    public double MoreThan(int min) => ProbabilityBetween(min + 1, MemberCount - 1);
    public double Exactly(int val)
    {
        for (int i = 0; i < MemberCount; i++)
        {
            if (Intervals[i] == val) return Probability[i];
        }
        return 0.0;
    }


    public void DisplayHistogram()
    {
        // I am actually cheating a LOT with this "Histogram" -- it's actually a bar graph. 
        Chart.Column<double, int, string>(
            // Casting shenanigans because Frequency is stored as an int. 
            Probability.ToArray(),
            // The "center" of the histogram bars. On paper, it's a bad idea because you can make mistakes this way,
            // but the thing's a computer, and won't screw up unless we give it bad data. 
            Keys: Intervals,
            // The lynchpin: by setting the width to the width of an interval, we leave no gaps between the bars. 
            // Maybe there is a gap, but that can be chalked up to styling. 
            Width: Intervals[1] - Intervals[0]
        )
            .WithXAxisStyle<double, int, string>(Title: Plotly.NET.Title.init("x"))
            .WithYAxisStyle<double, int, string>(Title: Plotly.NET.Title.init("P(x)"))
            .Show();
    }

    public static ProbabilityDistribution ImportFromCSV(string filePath)
    {
        string import = File.ReadAllText(filePath);
        string[] importLines = import.Split("\n");
        List<int> events = new List<int>();
        List<double> outcomes = new List<double>();
    
        for (int i = 0; i < importLines.Length; i++)
        {
            string line = importLines[i];
            if (line.Length == 0) continue;
            string[] values = line.Split(", ");
            if (values.Length != 2) throw new Exception("Dimension Mismatch: invalid CSV format for PD import.");
            events.Add(Int32.Parse(values[0]));
            outcomes.Add(Double.Parse(values[1]));
        }
        return new ProbabilityDistribution(events.ToArray(), outcomes.ToArray());
    }

    public void ExportToCSV(string filePath) 
    {
        string export = "";
        for (int i = 0; i < MemberCount; i++)
        {
            export += $"{Intervals[i]},{Probability[i]}";
            if (i != MemberCount - 1) export += '\n';
        }

        File.WriteAllText(filePath, export);
    }
}