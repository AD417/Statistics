using System;
using System.IO;
namespace Statistics;

class World
{
    public static int Main()
    {
        System.Console.WriteLine("Testing, Cat!");

        Population World = Population.ImportFromCSV("File.csv");

        System.Console.WriteLine("There are {0} entities on this world.", World.MemberCount);
        System.Console.WriteLine("Their average MagicNumber is {0}.", World.AverageMagicNumber);

        Sample survey = World.SampleEntities(0.05);

        System.Console.WriteLine(String.Format(
            "The average MagicNumber of a sample consisting of 5% of these entities was {0}.", 
            survey.AverageMagicNumber
        ));

        World.ExportToCSV("File.csv");


        return 0;
    }
}