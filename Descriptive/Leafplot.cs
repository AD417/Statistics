namespace Statistics;

class Leafplot : DataSummary
{
    public int StemSize {get; }
    public int Stems {get; }
    public int SmallestStem {get; } 
    public List<List<int>> Leaves {get; }

    private string Plot {
        get {
            string output = "";
            for (int i = 0; i < Stems; i++)
            {
                List<int> Leaf = Leaves[i];

                string line = (i + SmallestStem).ToString().PadLeft(StemSize >= 10 ? 2 : 1) + " |";
                foreach (int value in Leaf)
                {
                    line += " " + value.ToString();
                }
                output += line + "\n";
            }
            return output;
        }
    }

    public Leafplot(Set data) : base(data)
    {
        // TODO: figure out why this stemsize thing SUCKS. 
        StemSize = (int) Math.Max((Math.Log10(data.MaxMagicNumber - data.MinMagicNumber) - 0.2), (Math.Log10(data.MaxMagicNumber) - 0.2));
        int pow10StemSize = (int) Math.Pow(10, StemSize);

        SmallestStem = (int) data.MinMagicNumber / pow10StemSize;
        Stems = (int)Math.Ceiling((float)data.MaxMagicNumber / pow10StemSize) - SmallestStem;

        Leaves = new List<List<int>>();
        for (int i = 0; i <= Stems; i++) Leaves.Add(new List<int>());

        foreach (Entity ent in data.Members)
        {
            // Same patchwork as in DotPlot. Assume the Magic is always an int. 
            int index = (int) (ent.MagicNumber / pow10StemSize - SmallestStem);
            Leaves[index].Add((int) ent.MagicNumber % pow10StemSize);
        }
        foreach (List<int> leaves in Leaves) leaves.Sort();
    }
    public void PrintPlot() 
    {
        System.Console.WriteLine(Plot);
    }
    public void ExportToFile(string filePath)
    {
        File.WriteAllText(filePath, Plot);
    }
}