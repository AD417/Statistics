namespace Statistics;

class DataSummary
{
    public int Size {get; }
    public int Sum {get; }
    public int Min {get; }
    public int Max {get; }
    public double Average {get; }
    public double Median {get; }
    public int Mode {get; }
    public int Range {get; }

    public DataSummary(Set data)
    {
        Size = data.MemberCount;
        Min = data.MinMagicNumber;
        Max = data.MaxMagicNumber;
        Range = Max - Min;
        Average = data.MeanMagicNumber;
        Sum = (int)(Average * Size);
        if (data.Members.Count == 0) 
        {
            Median = 0;
            Mode = 0;
        } 
        else 
        {
            if (Size % 2 == 1) 
            {
                Median = data.Members[(Size + 1) / 2].MagicNumber;
            }
            else 
            {
                Median = (double)(data.Members[Size / 2].MagicNumber + data.Members[Size / 2 + 1].MagicNumber) / 2;
            }
            Mode = data.Members.GroupBy(x => x)
                .OrderByDescending(x => x.Count()).ThenBy(x => x.Key)
                .Select(x => (int?)x.Key.MagicNumber)
                .FirstOrDefault() ?? 0;
        }
    }

    public virtual void Summarize()
    {
        System.Console.WriteLine(String.Format("CHECKSUM: Sum is {0}", Sum));
        System.Console.WriteLine(String.Format("Mean/Average is {0}", Math.Round(Average, 1)));
        System.Console.WriteLine(String.Format("Median is {0}", Median));
        System.Console.WriteLine(String.Format("Mode is {0}", Mode));
    }
}