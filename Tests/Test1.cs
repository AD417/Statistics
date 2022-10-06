namespace Statistics;

class Test1
{
    public static int TestsRun {get; set;} = 0;
    public static int TestsPassed {get; set;} = 0;

    public static void run()
    {
        Population World = Population.ImportFromCSV("Tests/test1.csv");

        DataSummary data = new DataSummary(World);  

        AssertEqual<int>(data.Sum, 45,"FAILED: Sum of elements came out as {0}, expected {1}");
        AssertEqual<int>(data.Range, 8,"FAILED: Range of elements came out as {0}, expected {1}");
        AssertEqual<double>(data.Mean, 5d,"FAILED: Average of elements came out as {0}, expected {1}");
        AssertEqual<int>(data.Sum, 45,"FAILED: Sum of elements came out as {0}, expected {1}");
        AssertEqual<int>(data.Sum, 45,"FAILED: Sum of elements came out as {0}, expected {1}");
        AssertEqual<int>(data.Sum, 45,"FAILED: Sum of elements came out as {0}, expected {1}");
        AssertEqual<int>(data.Sum, 45,"FAILED: Sum of elements came out as {0}, expected {1}");
        AssertEqual<int>(data.Sum, 45,"FAILED: Sum of elements came out as {0}, expected {1}");
    }

    public static void AssertEqual<T>(T actual, T expected, string ErrorMessage)
    {
        if (ReferenceEquals(null, expected)) throw new ArgumentNullException("Expected Argument for AssertEqual was null");
        TestsRun++;

        if (!expected.Equals(actual)) System.Console.WriteLine(String.Format(ErrorMessage, actual, expected));
        else TestsPassed++;


    }
    
    public static void Results()
    {
        System.Console.WriteLine($"Tests run: {TestsRun}");
        System.Console.WriteLine($"Tests passed: {TestsPassed} ({(double)TestsPassed / TestsRun})");
        System.Console.WriteLine($"Tests Failed: {TestsRun - TestsPassed}");
    }
}