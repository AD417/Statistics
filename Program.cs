namespace Statistics;

class Program
{
    public static int Main()
    {
        System.Console.Write(" 1) "); ZScore.LeftOf(2);
        System.Console.Write(" 2) "); ZScore.Between(-0.13, 1.75);
        System.Console.Write(" 3) "); ZScore.RightOf(1.86);
        

        NormalDistribution nd = new NormalDistribution(70, 3, true);

        System.Console.Write(" 4) "); nd.ZScoreFor(83);
        System.Console.Write(" 5) "); nd.MoreThan(83);

        nd = new NormalDistribution(150, 10, true);

        System.Console.Write(" 6) "); nd.LessThan(130);
        System.Console.Write(" 7) "); nd.MoreThan(125);
        System.Console.Write(" 8) "); nd.Between(125, 155);
        System.Console.Write(" 9) "); nd.TopPercent(2);
        return 0;
    }
}