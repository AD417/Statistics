namespace Statistics;

class ZScore
{
    // Hardcoded constant that I don't feel like recalculating. 
    private static double denominator = 1 / Math.Sqrt(2 * Math.PI);

    private static int precision = 4;
    public static int Precision {
        set
        {
            if (value <= 0) throw new Exception("Invalid number of digits of precision!");
            precision = value;
        }
        get => precision;
    }

    private static double[] cache = new double[800];

    private static double f(double x)
    {
        return Math.Pow(Math.E, -(x * x * 0.5)) * denominator;
    }

    private static double Integrate(double min, double max)
    {
        double sum = 0;
        double delta = (max - min) / 2000; 
        double thismin = min;
        // Simpson's rule. There exists no elementary integral for f(x). 
        for (int i = 0; i < 2000; i++)
        {
            sum += delta / 6 * (f(thismin) + 4 * f(thismin + delta * 0.5) + f(thismin + delta));
            thismin += delta;
        }
        return Math.Round(sum, precision);
    }

    private static double NewtonStep(double currentGuess, double desiredProbability)
    {
        // a2 = f(a1) / f'(a1);

        // f(a1);
        double probabilityError = Score(currentGuess) - desiredProbability;
        // f'(a1)
        // Since Score(x) is effectively the integral of f(x), we can get away with this.
        double slope = f(currentGuess);
        // a2
        return currentGuess - (probabilityError / slope);
    }

    public static double FromProbability(double probability)
    {
        double currentGuess = 0;
        double lastGuess = -5;
        while (Math.Abs(lastGuess - currentGuess) > 0.004) 
        {
            lastGuess = currentGuess;
            currentGuess = NewtonStep(currentGuess, probability);
        }
        return Math.Round(currentGuess, 2);
    }

    public static void FillCache()
    {
        for (int i = 0; i < 800; i++) cache[i] = f((i - 400) * 0.01);
        for (int i = 0; i < 800; i++) System.Console.WriteLine(cache[i]);
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
        double Pmax = Score(maxZ);
        double Pmin = Score(minZ);
        System.Console.WriteLine(
            $"P({minZ} < z < {maxZ}) = P(z < {maxZ}) - P(z < {minZ}) = {Pmax} - {Pmin} = {Math.Round(Pmax - Pmin, precision)}"
        );
    }

    public static void Outside(double minZ, double maxZ)
    {
        if (minZ > maxZ) 
        {
            Between(maxZ, minZ);
            return;
        }
        double Pmax = Score(-maxZ);
        double Pmin = Score(minZ);
        System.Console.WriteLine(
            $"P(z < {minZ} or z > {maxZ}) = P(z < {minZ}) + P(z < {-maxZ}) = {Pmin} + {Pmax} = {Math.Round(Pmin + Pmax, precision)}"
        );
    }
}