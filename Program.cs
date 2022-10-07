namespace Statistics;

class Program
{
    public static int Main()
    {
        Sample OurClass = Sample.ImportFromCSV("example1.csv");

        new DataSummary(OurClass).Summarize();

        return 0;
    }
}