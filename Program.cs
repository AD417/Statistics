namespace Statistics;

class Program
{
    public static int Main()
    {
        ZScore.Between(0, 1.96);
        ZScore.Between(0, 0.67);
        ZScore.Between(-1.23, 0);
        ZScore.Between(-1.43, 0);
        ZScore.RightOf(0.82);
        ZScore.RightOf(2.83);
        ZScore.LeftOf(-1.77);
        ZScore.LeftOf(-1.32);
        ZScore.Between(-0.2, 1.56);
        ZScore.Between(-2.46, 1.74);
        ZScore.Between(1.12, 1.43);
        ZScore.Between(1.46, 2.97);
        ZScore.RightOf(-1.43);
        ZScore.LeftOf(1.42);

        return 0;
    }
}