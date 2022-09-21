namespace Statistics;

class DataSummary
{
    public int Size {get; }
    public int Sum {get; }
    public int Min {get; }
    public int Max {get; }
    public double Average {get; }
    public int Median {get; }
    public int Mode {get; }
    public int Range {get; }

    public DataSummary(Set data)
    {
        Size = data.MemberCount;
        Min = data.MinMagicNumber;
        Max = data.MaxMagicNumber;
        Range = Max - Min;
        Average = data.AverageMagicNumber;
        Sum = (int)Average * Size;
        if (data.Members.Count == 0) 
        {
            Median = 0;
            Mode = 0;
        } 
        else 
        {
            Median = data.Members[(Size + 1) / 2].MagicNumber;
            Mode = data.Members.GroupBy(x => x)
             .OrderByDescending(x => x.Count()).ThenBy(x => x.Key)
             .Select(x => (int?)x.Key.MagicNumber)
             .FirstOrDefault() ?? 0;
        }
    }
}