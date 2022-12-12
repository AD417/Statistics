namespace Statistics;

class Program
{
    public static int Main(string[] args)
    {
        System.Console.WriteLine(ZScore.LeftOf(3));
        System.Console.WriteLine(ZScore.LeftOf(2));
        System.Console.WriteLine(ZScore.LeftOf(1));
        System.Console.WriteLine(ZScore.LeftOf(0));
        System.Console.WriteLine(ZScore.LeftOf(-1));
        System.Console.WriteLine(ZScore.LeftOf(-2));
        System.Console.WriteLine(ZScore.LeftOf(-3));


        return 0;
    }
}