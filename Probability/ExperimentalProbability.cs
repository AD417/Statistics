namespace Statistics;

class ExperimentalProbability
{
    public static Sample ArbitraryOutcomeTrial(Func<int> outcomeCallback, int trials)
    {
        List<Entity> entityList = new List<Entity>();
        entityList.EnsureCapacity(trials);
        for (int i = 0; i < trials; i++) {
            entityList.Add(new Entity(outcomeCallback()));
        }
        return new Sample(entityList);

    }

    public static Sample CoinFlipTrial(int flips) => ArbitraryOutcomeTrial(RNG.CoinFlip, flips);
    public static Sample DieRollTrial(int rolls) => ArbitraryOutcomeTrial(RNG.DieRoll, rolls);
    public static Sample SetOutcomeTrial(int outcomeCount, int trials) => ArbitraryOutcomeTrial(
        () => RNG.PickFromOutcomes(outcomeCount), 
        trials
    );
    public static Sample WeightedOutcomeTrial(int[] outcomeWeights, int trials) => ArbitraryOutcomeTrial(
        () => RNG.PickFromWeightedOutcomes(outcomeWeights),
        trials
    );
}