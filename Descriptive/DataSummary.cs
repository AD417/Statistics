namespace Statistics;

class DataSummary
{
    public int Size {get; }
    public int Sum {get; }

    private double[] _criticalPositions = new double[5];
    public double Min {get => _criticalPositions[0]; }
    public double Q1 {get => _criticalPositions[1]; }
    public double Q2 {get => _criticalPositions[2]; }
    public double Median {get => _criticalPositions[2]; }
    public double Q3 {get => _criticalPositions[3]; }
    public double Max {get => _criticalPositions[4]; }

    public double Range {get => Max - Min; }
    public double InterquartileRange {get => Q3 - Q1; }
    public double IQR {get => InterquartileRange; }

    public virtual double Average {get; }
    public double Mean {get => Average; }
    protected double[] _Mode {get; } = new double[0];
    public double Mode {
        get {
            if (_Mode.Length == 0) return -1;
            return _Mode[0];
            // TODO: figure out how to deal with a multimodal situation. 
        } 
    }
    public bool MultiModal {get => _Mode.Length > 1; }
    public virtual double StandardDeviation {get; }
    public double Stdev {get => StandardDeviation; }
    public double Variance {get => StandardDeviation * StandardDeviation; }

    public DataSummary(Set data)
    {
        if (data.MemberCount == 0) throw new ArgumentException("Cannot summarize a data set with 0 elements");
        Size = data.MemberCount;
        Average = data.MeanMagicNumber;
        Sum = (int)(Average * Size);
        StandardDeviation = ComputeStandardDeviation(data);

        Set[] subsets = new Set[2];
        _criticalPositions = new double[5];

        (_criticalPositions[1], _criticalPositions[2], _criticalPositions[3]) = computeQuartiles(data);
        _criticalPositions[0] = data.MinMagicNumber;
        _criticalPositions[4] = data.MaxMagicNumber;

        _Mode = ComputeMode(data);
        // Mode = data.Members.GroupBy(x => x)
        //     .OrderByDescending(x => x.Count()).ThenBy(x => x.Key)
        //     .Select(x => (int?)x.Key.MagicNumber)
        //     .FirstOrDefault() ?? 0;
    }

    public static double[] ComputeMode(Set data)
    {
        Dictionary<double, int> counts = new Dictionary<double, int>();
        foreach (Entity ent in data.Members ) {
            double num = ent.MagicNumber;
            if (counts.ContainsKey(num))
                counts[num]++;
            else
                counts[num] = 1;
        }

        int maximum = counts.Max(kvp => kvp.Value);
        if (maximum < 2) return new double[0];

        List<double> modeList = new List<double>();
        foreach (int key in counts.Keys) {
            if (counts[key] == maximum)
                modeList.Add(key);
        }

        double[] modes = new double[modeList.Count];
        for (int i = 0; i < modeList.Count; i++)
            modes[i] = modeList[i];
        
        return modes;
    }

    public static (double q1, double q2, double q3) computeQuartiles(Set data)
    {
        Set[] subsets = new Set[2];
        double q1, q2, q3;
        int size = data.MemberCount;


        if (size % 2 == 1) 
        {
            q2 = data.Members[(size - 1) / 2].MagicNumber;
            subsets[0] = new Sample(data.Members.GetRange(0, size / 2));
            subsets[1] = new Sample(data.Members.GetRange(size / 2 + 1,size / 2));
        }
        else 
        {
            q2 = 0.5 * (double)(
                data.Members[size / 2 - 1].MagicNumber + 
                data.Members[size / 2].MagicNumber
            );
            subsets[0] = new Sample(data.Members.GetRange(0, size / 2));
            subsets[1] = new Sample(data.Members.GetRange(size / 2, size / 2));
        }

        int subsetSize = subsets[0].MemberCount;
        if (subsetSize % 2 == 1)
        {
            q1 = subsets[0].Members[(subsetSize - 1) / 2].MagicNumber;
            q3 = subsets[1].Members[(subsetSize - 1) / 2].MagicNumber;
        }
        else 
        {
            q1 = 0.5 * (double)(
                subsets[0].Members[subsetSize / 2 - 1].MagicNumber + 
                subsets[0].Members[subsetSize / 2].MagicNumber
            );
            q3 = 0.5 * (double)(
                subsets[1].Members[subsetSize / 2 - 1].MagicNumber + 
                subsets[1].Members[subsetSize / 2].MagicNumber
            );
        }
        return (q1, q2, q3);
    }

    public static double ComputeStandardDeviation(Set data)
    {
        // Store MeanMagicNumber so it's not constantly recalculated.
        double dataMean = data.MeanMagicNumber; 
        int VarianceDivisor = data.MemberCount;
        if (!data.IsPopulation) VarianceDivisor--;
        return Math.Sqrt(data.Members.Sum(x => Math.Pow(x.MagicNumber - dataMean, 2)) / VarianceDivisor);
    }

    public virtual void Summarize()
    {
        System.Console.WriteLine($"Count:   {Size}");
        System.Console.WriteLine($"Sum:     {Sum}");
        System.Console.WriteLine($"Mean:    {Math.Round(Mean, 2)}");
        System.Console.WriteLine($"Mode:    {Mode}");
        System.Console.WriteLine($"Min:     {Min}");
        System.Console.WriteLine($"Q1:      {Q1}");
        System.Console.WriteLine($"Median:  {Median}");
        System.Console.WriteLine($"Q3:      {Q3}");
        System.Console.WriteLine($"Max:     {Max}");
        System.Console.WriteLine($"Range:   {Range} ({Max} - {Min})");
        System.Console.WriteLine($"IQR:     {IQR} ({Q3} - {Q1})");
        System.Console.WriteLine($"Stdev:   {Math.Round(Stdev, 8)}");
        System.Console.WriteLine($"Stdev^2: {Math.Round(Variance, 8)}");
    }
}