namespace Statistics;

static class ZScore
{
    // Hardcoded constant that seems like frivolous math. 
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

    private static double f(double x)
    {
        // e^(-x^2/2) / sqrt(2pi)
        return Math.Pow(Math.E, -(x * x * 0.5)) * denominator;
    }

    private static double Integrate(double x)
    {
        // Integrate the normal distribution function from 0 to x, for any value of x, to the precison 
        // required by ZScore.precision.
        
        // The normal distribution function is e^(-x^2/2) / sqrt(2pi). This function has no elementary integral.
        // To circumvent this problem, we convert that function to a taylor series, integrate that series,
        // and then sum up the terms when we put in x. 

        // The taylor series is:
        // f(x) = (1 - x^2 + x^4 / 2 - x^6 / 6 + x^8 / 24 - ...) / sqrt(2pi)
        // Which is exactly the same as e^x, but we replace x with -x^2/2, and divide the entire
        // expression by sqrt(2pi) at the end. 

        // The integral is therefore:
        // F(x) = x - x^3/3 + x^5/10 - x^7 / 42 + x^9 / 216 - ...) / sqrt(2pi).

        // Since this can go on for an arbitrary number of terms, we must allow the series to continue 
        // for as long as necessary, until acceptable precision is reached. 

        double exponent = x * x * -0.5;
        double sum = x;
        double term = x;
        double epsilon = Math.Pow(0.1, precision + 1);

        for (int i = 1; Math.Abs(term) > epsilon; i++)
        {
            // Conversion between terms in the series. Minimizes floating point errors. 
            term *= exponent;
            term *= (2 * (double)i - 1) / (2 * (double)i + 1) / i;

            sum += term;
        }
        return sum / Math.Sqrt(2 * Math.PI);
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
        if (probability > 1 || probability < 0) throw new Exception($"Invalid Probability: {probability}");
        double currentGuess = 0;
        double lastGuess = -5;
        while (Math.Abs(lastGuess - currentGuess) > 0.004) 
        {
            lastGuess = currentGuess;
            currentGuess = NewtonStep(currentGuess, probability);
        }
        return Math.Round(currentGuess, 2);
    }

    public static double CriticalValueForConfidence(double confidence)
    {
        double lowerProbability = Math.Round(0.5 * (1 - confidence), precision);
        double zCritical = -FromProbability(lowerProbability);
        System.Console.WriteLine($"P(-zc < z < zc) = {confidence}");
        System.Console.Write(BinomialDistribution.tab); 
        System.Console.WriteLine($"P(z < -zc) = {lowerProbability}; zc = {zCritical}");
        return zCritical;
    }

    private static double Score(double z) => Math.Round(0.5 + Integrate(z), precision);

    public static double LeftOf(double z)
    {
        double probability = Score(z);
        System.Console.WriteLine($"P(z < {z}) = {probability}");
        return probability;
    }

    public static double RightOf(double z)
    {
        double probability = Score(-z);
        System.Console.WriteLine($"P(z > {z}) = P(z < {-z}) = {probability}");
        return probability;
    }

    public static double Between(double minZ, double maxZ)
    {
        if (minZ > maxZ) return Between(maxZ, minZ);

        double Pmax = Score(maxZ);
        double Pmin = Score(minZ);
        double probability = Math.Round(Pmax - Pmin, precision);
        System.Console.WriteLine(
            $"P({minZ} < z < {maxZ}) = P(z < {maxZ}) - P(z < {minZ}) = {Pmax} - {Pmin} = {probability}"
        );
        return probability;
    }

    public static double Outside(double minZ, double maxZ)
    {
        if (minZ > maxZ) return Between(maxZ, minZ);

        double Pmax = Score(-maxZ);
        double Pmin = Score(minZ);
        double probability = Math.Round(Pmin + Pmax, precision);
        System.Console.WriteLine(
            $"P(z < {minZ} or z > {maxZ}) = P(z < {minZ}) + P(z < {-maxZ}) = {Pmin} + {Pmax} = {probability}"
        );
        return probability;
    }
}