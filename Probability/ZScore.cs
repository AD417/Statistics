namespace Statistics;

class ZScore
{
    // Hardcoded constant that I don't feel like recalculating. 
    private static double denominator = 1 / Math.Sqrt(2 * Math.PI);
    private static double f(double x)
    {
        return Math.Pow(Math.E, -(x * x * 0.5)) * denominator;
    }

    private static double Integrate(double min, double max)
    {
        double sum = 0;
        double delta = (max - min) * 0.001; 
        double thismin = min;
        // Simpson's rule. There exists no elementary integral for f(x). 
        for (int i = 0; i < 1000; i++)
        {
            sum += delta / 6 * (f(thismin) + 4 * f(thismin + delta * 0.5) + f(thismin + delta));
            thismin += delta;
        }
        return Math.Round(sum, 4);
    }

    private static double Score(double z) => Integrate(-4.5, z);

    public static void LeftOf(double z)
    {
        System.Console.WriteLine($"P(z < {z}) = {Score(z)}");
    }

    public static void RightOf(double z)
    {
        System.Console.WriteLine($"P(z > {z}) = P(z < {-z}) = {Score(-z)}");
    }

    public static void Between(double minZ, double maxZ)
    {
        if (minZ > maxZ) 
        {
            Between(maxZ, minZ);
            return;
        }
        System.Console.WriteLine(
            $"P({minZ} < z < {maxZ}) = P(z < {maxZ}) - P(z < {minZ}) = {Score(maxZ)} - {Score(minZ)} = {Integrate(minZ, maxZ)}"
        );
    }

    public static void Outside(double minZ, double maxZ)
    {
        if (minZ > maxZ) 
        {
            Between(maxZ, minZ);
            return;
        }
        System.Console.WriteLine(
            $"P(z < {minZ} or z > {maxZ}) = P(z < {minZ}) + P(z < {-maxZ}) = {Score(minZ)} + {Score(-maxZ)} = {Score(minZ) + Score(-maxZ)}"
        );
    }
}