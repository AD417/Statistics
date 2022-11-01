namespace Statistics;

class Program
{
    public static int Main()
    {
        string output = "\"C r\nn\", ";
        for (int i = 1; i <= 30; i++) 
            output += i.ToString() + ", ";
        output += "\n";

        for (int n = 1; n <= 30; n++)
        {
            string line = n.ToString() + ", ";
            int r;
            for (r = 1; r <= n; r++)
            {
                line += Math.Round(Permutation.nPr(n, r)).ToString() + ", ";
            }
            for (; r <= 30; r++) line += ",";
            output += line + "\n";
        }

        File.WriteAllText("nprCombinations.csv", output);

        return 0;
    }
}