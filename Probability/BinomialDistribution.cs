namespace Statistics;

class BinomialDistribution : NormalDistribution
{
    private int trials;
    private double successChance;
    public int Trials
    {
        get => trials;
        set
        {
            if (value <= 0) throw new Exception("Invalid trials count!");
            if (value * Math.Min(successChance, 1 - successChance) < 5)
                System.Console.WriteLine("Warning: Insufficient checksum to force a Normal Approximation!");
            
            trials = value;
        }
    }
    public double SuccessChance
    {
        get => successChance;
        set
        {
            if (value < 0 || value > 1) throw new Exception("Invalid success Chance!");
            if (trials * Math.Min(value, 1 - value) < 5)
                System.Console.WriteLine("Warning: Insufficient checksum to force a Normal Approximation!");

            successChance = value;
        }
    }
    public double FailureChance
    {
        get => 1 - successChance;
    }
    public double Checksum 
    {
        get => Trials * Math.Min(SuccessChance, FailureChance);
    }
    public BinomialDistribution(int trials, double successChance) : base() 
    { 
        double failureChance = 1 - successChance;
        double checksum = trials * Math.Min(successChance, failureChance);

        if (checksum < 5)
            System.Console.WriteLine("Warning: Insufficent trial count to force a Normal Approximation.");
        
        this.trials = trials;
        this.successChance = successChance;
        Mean = trials * successChance;
        Stdev = Math.Sqrt(Mean * failureChance);
    }

    public override bool AssertNormalDistribution() => Checksum >= 5;

    public override void Summarize()
    {
        System.Console.WriteLine($"Trials = {Trials}");
        System.Console.WriteLine(
            $"p = {Math.Round(successChance, precision)}, q = {Math.Round(FailureChance, precision)}"
        );
        base.Summarize();
        System.Console.WriteLine($"Checksum = {Math.Round(Checksum, precision)}");
        System.Console.WriteLine(); // Add a line at the end.
    }


    public override double LessThan(double c)
    {
        double x = c - 0.5;
        System.Console.WriteLine($"P(c < {c}) = P(x < {x})");
        System.Console.Write(tab);
        return base.LessThan(x);
    }
    
    public double AtMost(double c)
    {
        double x = c + 0.5;
        System.Console.WriteLine($"P(c <= {c}) = P(x < {x})");
        System.Console.Write(tab);
        return base.LessThan(x);
    }

    public override double MoreThan(double c)
    {
        double x = c + 0.5;
        System.Console.WriteLine($"P(c > {c}) = P(x > {x})");
        System.Console.Write(tab);
        return base.MoreThan(x);
    }
    
    public double AtLeast(double c)
    {
        double x = c - 0.5;
        System.Console.WriteLine($"P(c >= {c}) = P(x > {x})");
        System.Console.Write(tab);
        return base.MoreThan(x);
    }

    public override double Between(double minC, double maxC)
    {
        if (maxC < minC) return Between(maxC, minC);
        // Exclusive -- use BetweenInclusive to include the bounds.
        double minX = minC + 0.5;
        double maxX = maxC - 0.5;

        System.Console.WriteLine($"P({minC} < c < {maxC}) = P({minX} < x < {maxX})");
        System.Console.Write(tab);
        return base.Between(minX, maxX);

    }

    public double BetweenInclusive(double minC, double maxC)
    {
        if (maxC < minC) return BetweenInclusive(maxC, minC);
        // Exclusive -- use BetweenInclusive to include the bounds.
        double minX = minC - 0.5;
        double maxX = maxC + 0.5;

        System.Console.WriteLine($"P({minC} <= c <= {maxC}) = P({minX} < x < {maxX})");
        System.Console.Write(tab);
        return base.Between(minX, maxX);

    }

    public double Exactly(double c)
    {
        double minX = c - 0.5;
        double maxX = c + 0.5;
        System.Console.WriteLine($"P(c = {c}) = P({minX} < x < {maxX})");
        System.Console.Write(tab);
        return base.Between(minX, maxX);
    }

    // Legacy stuff. Still useful for low trial count stuff, and at least the naming scheme is out of the way, 
    // but I'm not sure what to do with it. Move it to another class? 
    public static double ExactOutcome(int trials, int successes, double successChance)
    {
        if (successes > trials) 
            throw new Exception($"Invalid outcome: number of successes ({successes}) exceeds number of trials ({trials})");
        if (successChance < 0 || successChance > 1) 
            throw new Exception($"Invalid probability: {successChance}");
        return 
        (
            // nCx * p^x + (1-p)^(n-x)
            Permutation.nCr(trials, successes) *
            Math.Pow(successChance, successes) * 
            Math.Pow(1 - successChance, trials - successes)
        );
    }

    public static double AtLeastXOutcomes(int trials, int minSuccesses, double successChance)
    {
        double chance = 0.0;
        for (int i = minSuccesses; i <= trials; i++) chance += ExactOutcome(trials, i, successChance);
        return chance;
    }

    public static double AtMostXOutcomes(int trials, int maxSuccesses, double successChance)
    {
        double chance = 0.0;
        for (int i = 0; i <= maxSuccesses; i++) chance += ExactOutcome(trials, i, successChance);
        return chance;
    }

    public static double GreaterThanXOutcomes(int trials, int minSuccesses, double successChance)
    {
        double chance = 0.0;
        for (int i = minSuccesses; i <= trials; i++) chance += ExactOutcome(trials, i, successChance);
        return chance;
    }

    public static double LessThanXOutcomes(int trials, int maxSuccesses, double successChance)
    {
        double chance = 0.0;
        for (int i = 0; i <= maxSuccesses; i++) chance += ExactOutcome(trials, i, successChance);
        return chance;
    }

    public static double OutcomeRangeExclusive(int trials, int minSuccesses, int maxSuccesses, double successChance)
    {
        double chance = 0.0;
        for (int i = minSuccesses + 1; i < maxSuccesses; i++) chance += ExactOutcome(trials, i, successChance);
        return chance;
    }

    public static double OutcomeRangeInclusive(int trials, int minSuccesses, int maxSuccesses, double successChance)
    {
        double chance = 0.0;
        for (int i = minSuccesses; i <= maxSuccesses; i++) chance += ExactOutcome(trials, i, successChance);
        return chance;
    }

    public static ProbabilityDistribution AllOutcomesChance(int trials, double successChance)
    {
        double[] outcomes = new double[trials + 1];
        for (int i = 0; i <= trials; i++) outcomes[i] = ExactOutcome(trials, i, successChance);
        
        return new ProbabilityDistribution(outcomes);
    }
}