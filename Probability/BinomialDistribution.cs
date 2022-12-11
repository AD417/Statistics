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
                System.Console.WriteLine("Warning: Insufficient certainty to force a Normal Approximation!");
            
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
                System.Console.WriteLine("Warning: Insufficient certainty to force a Normal Approximation!");

            successChance = value;
        }
    }
    public double FailureChance
    {
        get => 1 - successChance;
    }


    private BinomialDistribution(int trials, double successChance) : base() 
    { 
        double failureChance = 1 - successChance;
        double certainty = trials * Math.Min(successChance, failureChance);

        System.Console.WriteLine($"Binomial Distribution certainty: {Math.Round(certainty, 4)}");

        if (certainty < 5)
            System.Console.WriteLine("Warning: Insufficent certainty to force a Normal Approximation.");
        
        this.trials = trials;
        this.successChance = successChance;
        Mean = trials * successChance;
        Stdev = Mean * failureChance;
    }

    public static BinomialDistribution GenerateApproximateDistribution(int trials, double successChance)
    {
        double failureChance = 1 - successChance;

        double certainty = trials * Math.Min(successChance, failureChance);

        System.Console.WriteLine($"Binomial Distribution certainty: {Math.Round(certainty, 4)}");

        if (certainty < 5)
            System.Console.WriteLine("Warning: Insufficent certainty to force a Normal Approximation.");
        
        return new BinomialDistribution(trials, successChance);
    }

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