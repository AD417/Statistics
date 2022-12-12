namespace Statistics;

class Unit5Lesson3
{
    public void run()
    {
        NormalDistribution[] gestation = {
            // 0: 28- weeks
            new NormalDistribution(1.90, 1.23),
            // 1: 28-31 weeks
            new NormalDistribution(4.10, 1.88),
            // 2: 32-33 weeks
            new NormalDistribution(5.08, 1.56),
            // 3: 34-36 weeks
            new NormalDistribution(6.14, 1.29),
            // 4: 37-38 weeks
            new NormalDistribution(7.06, 1.09),
            // 5: 39 weeks
            new NormalDistribution(7.48, 1.02),
            // 6: 40-41 weeks
            new NormalDistribution(7.67, 1.03),
            // 7: 42+ weeks
            new NormalDistribution(7.56, 1.10)
        };
        NormalDistribution.setTabLength(7);

        System.Console.Write(" 1)");
        System.Console.Write(   " a) "); gestation[0].LessThan(5.5);
        System.Console.Write("    b) "); gestation[2].LessThan(5.5);
        System.Console.Write("    c) "); gestation[5].LessThan(5.5);
        System.Console.Write("    d) "); gestation[7].LessThan(5.5);

        
        System.Console.Write(" 2)");
        System.Console.Write(   " a) "); gestation[0].TopPercent(10);
        System.Console.Write("    b) "); gestation[3].TopPercent(10);
        System.Console.Write("    c) "); gestation[6].TopPercent(10);
        System.Console.Write("    d) "); gestation[7].TopPercent(10);
        
        System.Console.Write(" 3)");
        System.Console.Write(   " a) "); gestation[0].Between(6,9);
        System.Console.Write("    b) "); gestation[1].Between(6,9);
        System.Console.Write("    c) "); gestation[3].Between(6,9);
        System.Console.Write("    d) "); gestation[5].Between(6,9);

        System.Console.Write(" 4)");
        System.Console.Write(   " a) "); gestation[0].LessThan(3.25);
        System.Console.Write("    b) "); gestation[1].LessThan(3.25);
        System.Console.Write("    c) "); gestation[2].LessThan(3.25);
        System.Console.Write("    d) "); gestation[5].LessThan(3.25);
    }
}