using System;
namespace Statistics;

class World
{
    public static int Main()
    {
        System.Console.WriteLine("Testing, Cat!");

        Population World = new Population(10);

        System.Console.WriteLine(String.Format("There are {0} entities on this world.", World.MemberCount));
        System.Console.WriteLine(String.Format("Their average MagicNumber is {0}", World.AverageMagicNumber));

        return 0;
    }
}