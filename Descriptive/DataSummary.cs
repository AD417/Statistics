namespace Statistics;

class DataSummary
{
    public int Size {get; }
    public int Sum {get; }
    public int Min {get; }
    public int Max {get; }
    public virtual double Average {get; }
    public double Mean {get => Average; }
    public double Median {get; }
    protected int[] _Mode {get; } = new int[0];
    public int Mode {
        get {
            if (_Mode.Length == 0) return -1;
            return _Mode[0];
            // TODO: figure out how to deal with a multimodal situation. 
        } 
    }
    public bool MultiModal {get => _Mode.Length > 1; }
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
            _Mode = new int[0];
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
            _Mode = ComputeMode(data);
            // Mode = data.Members.GroupBy(x => x)
            //     .OrderByDescending(x => x.Count()).ThenBy(x => x.Key)
            //     .Select(x => (int?)x.Key.MagicNumber)
            //     .FirstOrDefault() ?? 0;
        }
    }

    public static int[] ComputeMode(Set data)
    {
        Dictionary<int, int> counts = new Dictionary<int, int>();
        foreach (Entity ent in data.Members ) {
            int num = ent.MagicNumber;
            if (counts.ContainsKey(num))
                counts[num]++;
            else
                counts[num] = 1;
        }

        int maximum = counts.Max(kvp => kvp.Value);
        if (maximum < 2) return new int[0];

        List<int> modeList = new List<int>();
        foreach (int key in counts.Keys) {
            if (counts[key] == maximum)
                modeList.Add(key);
        }

        int[] modes = new int[modeList.Count];
        for (int i = 0; i < modeList.Count; i++)
            modes[i] = modeList[i];
        
        return modes;
    }

    public virtual void Summarize()
    {
        System.Console.WriteLine($"CHECKSUM: Sum is {Sum}");
        System.Console.WriteLine($"Mean/Average is {Math.Round(Mean, 1)}");
        System.Console.WriteLine($"Median is {Median}");
        System.Console.WriteLine($"Mode is {Mode}");
    }
}