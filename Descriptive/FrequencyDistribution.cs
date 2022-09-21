using Plotly.NET.CSharp;
using Plotly.NET.TraceObjects;

namespace Statistics;
    
class FrequencyDistribution : DataSummary
{
    public int IntervalWidth {get; }
    public int IntervalCount {get; }
    public int[,] Intervals {get; }
    public double[] Midpoint {get; }
    public int[] CumulativeFrequency {get; }
    public decimal[] RelativeFrequency {get; }
    public int[] Frequency {get; }

    private Set RawData {get; }

    public FrequencyDistribution(Set data, int intervalCount) : base(data)
    {
        IntervalCount = intervalCount;
        IntervalWidth = (int)Math.Ceiling((decimal) Range / IntervalCount);

        Intervals = new int[IntervalCount, 2];
        Midpoint = new double[IntervalCount];
        for (int i = 0; i < IntervalCount; i++)
        {
            Intervals[i, 0] = Min + IntervalWidth * i;
            Intervals[i, 1] = Min + IntervalWidth * i + IntervalWidth - 1;
            Midpoint[i] = (double)(Intervals[i, 0] + Intervals[i, 1]) / 2;
        }

        Frequency = new int[IntervalCount];
        CumulativeFrequency = new int[IntervalCount];
        for (int i = 0; i < data.MemberCount; i++) 
        {
            int firstIndex = (data.Members[i].MagicNumber - Min) / IntervalWidth;
            Frequency[firstIndex]++;
            for (int j = firstIndex; j < IntervalCount; j++)
                CumulativeFrequency[j]++;
        }

        RelativeFrequency = new decimal[IntervalCount];
        for (int i = 0; i < IntervalCount; i++)
        {
            RelativeFrequency[i] = (decimal)Frequency[i] / Size;
        }
        
        RawData = data;
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
            dataParts[i, 0] = String.Format("{0}-{1}", Intervals[i,0], Intervals[i,1]);
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

    public void DisplayHistogram()
    {
        double[] MagicNumbersRaw = new double[Size];
        for (int i = 0; i < Size; i++)
        {
            Entity e = RawData.Members[i];
            MagicNumbersRaw[i] = (double) e.MagicNumber;
        }
        Bins bins = Bins.init(Min, Max, IntervalWidth);

        Chart.Histogram<double, double, string>(
            X: MagicNumbersRaw, 
            Line: Plotly.NET.Line.init(Color: Plotly.NET.Color.fromHex("000000"), Width: 1),
            XBins: bins
        )
            .WithTraceInfo("Testcat", ShowLegend: true)
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