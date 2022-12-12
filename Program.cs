namespace Statistics;

class Program
{
    public static int Main(string[] args)
    {
        BinomialDistribution nd = new BinomialDistribution(20, 0.5);

        nd.Summarize();
        System.Console.Write(" 1) "); nd.Exactly(10);

        return 0;
    }
}