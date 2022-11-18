namespace Statistics;

class Program
{
    public static int Main()
    {
        ProbabilityDistribution pd = BinomialDistribution.AllOutcomesChance(9, 0.6);
        System.Console.WriteLine(pd.Exactly(5));
        System.Console.WriteLine(pd.AtLeast(6));
        System.Console.WriteLine(pd.LessThan(4));
        System.Console.WriteLine(); // Line break

        pd = BinomialDistribution.AllOutcomesChance(6, 0.39);
        System.Console.WriteLine(pd.Exactly(2));
        System.Console.WriteLine(pd.AtLeast(4));
        System.Console.WriteLine(pd.LessThan(3));
        System.Console.WriteLine();

        pd = BinomialDistribution.AllOutcomesChance(12, 0.27);
        System.Console.WriteLine(pd.Exactly(3));
        System.Console.WriteLine(pd.AtLeast(4));
        System.Console.WriteLine(pd.LessThan(8));
        System.Console.WriteLine();

        pd = BinomialDistribution.AllOutcomesChance(10, 0.63);
        System.Console.WriteLine(pd.Exactly(6));
        System.Console.WriteLine(pd.AtLeast(5));
        System.Console.WriteLine(pd.LessThan(8));
        System.Console.WriteLine();

        pd = BinomialDistribution.AllOutcomesChance(8, 0.56);
        System.Console.WriteLine(pd.Exactly(5));
        System.Console.WriteLine(pd.MoreThan(5));
        System.Console.WriteLine(pd.AtMost(5));
        System.Console.WriteLine();

        pd = BinomialDistribution.AllOutcomesChance(20, 0.68);
        System.Console.WriteLine(pd.Exactly(1));
        System.Console.WriteLine(pd.MoreThan(1));
        System.Console.WriteLine(pd.AtMost(1));
        System.Console.WriteLine();

        pd = BinomialDistribution.AllOutcomesChance(10, 0.51);
        System.Console.WriteLine(pd.Exactly(2));
        System.Console.WriteLine(pd.MoreThan(2));
        System.Console.WriteLine(pd.ProbabilityBetween(2,5));
        System.Console.WriteLine();
        
        pd = BinomialDistribution.AllOutcomesChance(12, 0.43);
        System.Console.WriteLine(pd.Exactly(4));
        System.Console.WriteLine(pd.MoreThan(4));
        System.Console.WriteLine(pd.ProbabilityBetween(4,8));
        System.Console.WriteLine();

        return 0;
    }
}