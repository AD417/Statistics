namespace Statistics;

class Leafplot : DataSummary
{
    public int StemSize {get; }
    public int Stems {get; }
    public List<List<int>> Leaf {get; }
    public Leafplot(Set data) : base(data)
    {
        StemSize = (int) (Math.Log10(data.MaxMagicNumber) - 0.2);
        int pow10StemSize = (int) Math.Pow(10, StemSize);
        Stems = data.MaxMagicNumber / pow10StemSize;

        Leaf = new List<List<int>>();
        for (int i = 0; i <= Stems; i++) Leaf.Add(new List<int>());

        foreach (Entity ent in data.Members)
        {
            int index = ent.MagicNumber / pow10StemSize;
            Leaf[index].Add(ent.MagicNumber % pow10StemSize);
        }
        foreach (List<int> leaves in Leaf) leaves.Sort();
    }
    public void PrintPlot() 
    {
        for (int i = 0; i < Stems; i++)
        {
            List<int> trailingLeaves = Leaf[i];
            if (!trailingLeaves.Any()) continue;

            string line = i.ToString().PadLeft(StemSize >= 10 ? 2 : 1) + " |";
            foreach (int trailing in trailingLeaves)
            {
                line += " " + trailing.ToString();
            }
            System.Console.WriteLine(line);
        }
    }
}