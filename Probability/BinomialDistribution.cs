namespace Statistics;

class BinomialDistribution
{
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