namespace Statistics;

class Unit5Lesson2
{
    public static void run()
    {
        NormalDistribution nd = new NormalDistribution(174, 20);
        Console.Write(" 1) "); nd.LessThan(170);
        Console.Write(" 2) "); nd.LessThan(200);
        Console.Write(" 3) "); nd.MoreThan(182);
        Console.Write(" 4) "); nd.MoreThan(155);
        Console.Write(" 5) "); nd.Between(160, 170);
        Console.Write(" 6) "); nd.Between(172, 192);

        nd = new NormalDistribution(69.4, 2.9);
        NormalDistribution.setTabLength(7);

        Console.Write(" 7) a) "); nd.LessThan(66);
        Console.Write("    b) "); nd.Between(66, 72);
        Console.Write("    c) "); nd.MoreThan(72);

        nd = new NormalDistribution(64.2, 2.9);
        
        Console.Write(" 8) a) "); nd.LessThan(56.5);
        Console.Write("    b) "); nd.Between(61, 67);
        Console.Write("    c) "); nd.MoreThan(70.5);

        nd = new NormalDistribution(21.3, 6.2);
        
        Console.Write(" 9) a) "); nd.LessThan(15);
        Console.Write("    b) "); nd.Between(18, 25);
        Console.Write("    c) "); nd.MoreThan(34);

        nd = new NormalDistribution(21.1, 5.3);
        
        Console.Write("10) a) "); nd.LessThan(16);
        Console.Write("    b) "); nd.Between(19, 24);
        Console.Write("    c) "); nd.MoreThan(26);

        nd = new NormalDistribution(100, 12);
        
        Console.Write("11) a) "); nd.LessThan(70);
        Console.Write("    b) "); nd.Between(90, 120);
        Console.Write("    c) "); nd.MoreThan(140);

        nd = new NormalDistribution(20, 5);
        
        Console.Write("12) a) "); nd.LessThan(17);
        Console.Write("    b) "); nd.Between(20, 28);
        Console.Write("    c) "); nd.MoreThan(30);
    }
}