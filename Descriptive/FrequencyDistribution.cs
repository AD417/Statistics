using Plotly.NET.CSharp;

namespace Statistics;
    
class FrequencyDistribution : DataSummary
{
    public override double Average 
    {
        get {
            double EstimatedSum = 0;
            for (int i = 0; i < IntervalCount; i++)
                EstimatedSum = Midpoint[i] * Frequency[i];

            return EstimatedSum / Size;
        }
    }
    public override double StandardDeviation 
    {
        get {
            double EstimatedVariance = 0;
            for (int i = 0; i < IntervalCount; i++)
                EstimatedVariance += Math.Pow(Midpoint[i] - Average, 2) * Frequency[i];
            
            EstimatedVariance /= IntervalCount;

            return Math.Sqrt(EstimatedVariance);
        }
    }

    public int IntervalWidth {get; }
    public int IntervalCount {get; }
    public int[,] Intervals {get; }
    public double[] Midpoint {get; }
    public int[] CumulativeFrequency {get; }
    public decimal[] RelativeFrequency {get; }
    public int[] Frequency {get; }

    public FrequencyDistribution(Set data, int intervalCount) : base(data)
    {
        IntervalCount = intervalCount;
        IntervalWidth = (int)Math.Ceiling((double) Range / IntervalCount);

        Intervals = new int[IntervalCount, 2];
        Midpoint = new double[IntervalCount];
        for (int i = 0; i < IntervalCount; i++)
        {
            Intervals[i, 0] = (int) Min + IntervalWidth * i;
            Intervals[i, 1] = (int) Min + IntervalWidth * i + IntervalWidth - 1;
            Midpoint[i] = (double)(Intervals[i, 0] + Intervals[i, 1]) / 2;
        }

        Frequency = new int[IntervalCount];
        CumulativeFrequency = new int[IntervalCount];
        for (int i = 0; i < data.MemberCount; i++) 
        {
            int firstIndex = (data.Members[i].MagicNumber - (int)Min) / IntervalWidth;
            Frequency[firstIndex]++;
            for (int j = firstIndex; j < IntervalCount; j++)
                CumulativeFrequency[j]++;
        }

        RelativeFrequency = new decimal[IntervalCount];
        for (int i = 0; i < IntervalCount; i++)
        {
            RelativeFrequency[i] = (decimal)Frequency[i] / Size;
        }
    }

    public override void Summarize()
    {
        System.Console.WriteLine($"Sample size: {Size}");
        System.Console.WriteLine($"Total Range: {Range} ({Min}-{Max}).");
        System.Console.WriteLine($"Class Size: {IntervalWidth}");
    }
    public void PrintChart()
    {
        string data = "";
        string[] headers = "Bounds, Mid, Freq, R.F., C.F.".Split(", ");
        string[,] dataParts = new string[IntervalCount, 5];

        // Create padding that will be used to format the data later.
        int[] padding = new int[5];
        for (int i = 0; i < padding.Length; i++)
        {
            padding[i] = headers[i].Length;
        }

        // Ensure that the padding is long enough to accomodate all values in each row. 
        // While we're here, we may as well String-ify everything for later use. 
        for (int i = 0; i < IntervalCount; i++)
        {
            dataParts[i, 0] = String.Format($"{Intervals[i,0]}-{Intervals[i,1]}");
            padding[0] = Math.Max(padding[0], dataParts[i, 0].Length);

            dataParts[i, 1] = Midpoint[i].ToString();
            padding[1] = Math.Max(padding[1], dataParts[i, 1].Length);

            dataParts[i, 2] = Frequency[i].ToString();
            padding[2] = Math.Max(padding[2], dataParts[i, 2].Length);

            dataParts[i, 3] = Math.Round(RelativeFrequency[i], 3).ToString();
            padding[3] = Math.Max(padding[3], dataParts[i, 3].Length);

            dataParts[i, 4] = CumulativeFrequency[i].ToString();
            padding[4] = Math.Max(padding[4], dataParts[i, 4].Length);
        }

        // Create a blank row. It's used a bunch of times, so we may as well make it now. 
        string blankRow = "|";
        for (int i = 0; i < padding.Length; i++)
        {
            padding[i] += 1;
            blankRow += String.Concat(new String('-', padding[i] + 1)) + "|";
        }
        blankRow += "\n";

        // Assemble the table using everything we have created. 
        data += blankRow + "|";
        for (int i = 0; i < padding.Length; i++)
        {
            data += headers[i].PadLeft(padding[i]) + " |";
        }
        data += "\n" + blankRow;
        for (int i = 0; i < IntervalCount; i++)
        {
            data += "|";
            for (int j = 0; j < padding.Length; j++)
            {
                data += dataParts[i, j].PadLeft(padding[j]) + " |";
            }
            data += "\n";
        }
        data += blankRow;

        System.Console.WriteLine(data);
    }

    public void DisplayFrequencyHistogram()
    {
        // I am actually cheating a LOT with this "Histogram" -- it's actually a bar graph. 
        Chart.Column<double, double, string>(
            // Casting shenanigans because Frequency is stored as an int. 
            Frequency.Select(x => (double)x).ToArray(),
            // The "center" of the histogram bars. On paper, it's a bad idea because you can make mistakes this way,
            // but the thing's a computer, and won't screw up unless we give it bad data. 
            Keys: Midpoint,
            // The lynchpin: by setting the width to the width of an interval, we leave no gaps between the bars. 
            // Maybe there is a gap, but that can be chalked up to styling. 
            Width: IntervalWidth
        )
            .WithXAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("MagicNumber"))
            .WithYAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("Frequency"))
            .Show();
    }
    public void DisplayRelativeFrequencyHistogram()
    {
        Chart.Column<double, double, string>(
            RelativeFrequency.Select(x => (double)x).ToArray(),
            Keys: Midpoint,
            Width: IntervalWidth
        )
            .WithXAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("MagicNumber"))
            .WithYAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("Frequency"))
            .Show();
    }

    public void DisplayPolygonGraph() => DisplayFrequencyLineGraph();
    public void DisplayFrequencyLineGraph() 
    {
        // Line graphs require us to have a point of 0 at the beginning and end. 
        // These points occur one midpoint above and below the ends of the data.

        // Midpoints, including the ones we add to the start and end. 
        double[] LineGraphDataX = new double[Midpoint.Length + 2];
        // Frequency including the zeroes at the beginning and end. 
        double[] LineGraphDataY = new double[Midpoint.Length + 2];

        LineGraphDataX[0] = Midpoint[0] - IntervalWidth;
        LineGraphDataX[LineGraphDataX.Length - 1] = Midpoint[Midpoint.Length - 1] + IntervalWidth;

        for (int i = 0; i < Midpoint.Length; i++)
        {
            LineGraphDataX[i + 1] = Midpoint[i];
            LineGraphDataY[i + 1] = Frequency[i];
        }

        Chart.Line<double, double, string>(
            x: LineGraphDataX,
            y: LineGraphDataY
        )
            .WithXAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("MagicNumber"))
            .WithYAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("Frequency"))
            .Show();
    }

    public void DisplayOgive() => DisplayCumulativeFrequencyLineGraph();
    public void DisplayCumulativeFrequencyLineGraph()
    {
        // Midpoints, including the one we add to the start. 
        double[] LineGraphDataX = new double[Midpoint.Length + 1];
        // Frequency including the zeroe at the beginning. 
        double[] LineGraphDataY = new double[Midpoint.Length + 1];

        LineGraphDataX[0] = Midpoint[0] - IntervalWidth;

        for (int i = 0; i < Midpoint.Length; i++)
        {
            LineGraphDataX[i + 1] = Midpoint[i];
            LineGraphDataY[i + 1] = CumulativeFrequency[i];
        }

        Chart.Line<double, double, string>(
            x: LineGraphDataX,
            y: LineGraphDataY
        )
            .WithXAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("MagicNumber"))
            .WithYAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("Frequency"))
            .Show();
    }

    public void ExportToCSV(string filePath) 
    {
        string export = "Class #, Bounds, Midpoint, Frequency, R.F., C.F.\n";
        for (int i = 0; i < IntervalCount; i++)
        {
            export += String.Format(
                "{0}, {1}-{2}, {3}, {4}, {5}, {6}\n", 
                i + 1,
                Intervals[i, 0],
                Intervals[i, 1],
                Midpoint[i],
                Frequency[i],
                RelativeFrequency[i],
                CumulativeFrequency[i]
            );
        }
        File.WriteAllText(filePath, export);
    }
}