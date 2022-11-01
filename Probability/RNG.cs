namespace Statistics;

class RNG 
{
    public static Random Generator = new Random();

    public static void SetGeneratorSeed(int seed) 
    {
        Generator = new Random(seed);
    }

    public static int CoinFlip() => Generator.Next(0,2);
    public static int DieRoll() => Generator.Next(1,6);
    public static int PickFromOutcomes(int outcomeCount) => Generator.Next(1, outcomeCount);

    public static int PickFromWeightedOutcomes(int[] outcomeWeights)
    {
        // +1 is necessary because the top value is exclusive. 
        int weightedSum = outcomeWeights.Sum() + 1; 
        int outcome = Generator.Next(0, weightedSum);
        int runningSum = 0;
        for (int i = 0; i < outcomeWeights.Length; i++)
        {
            runningSum += outcomeWeights[i];
            if (runningSum >= outcome) return i;
        }

        return Int32.MaxValue;
    }
}