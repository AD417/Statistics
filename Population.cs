namespace Statistics;

class Population : Set
{
    public Population(int entities) : base(entities) {}

    public Sample SampleEntities(int sampleSize) 
    {
        if (sampleSize > MemberCount) throw new ArgumentException("cannot create a sample larger than the population");
        int leftToSample = sampleSize;
        List<Entity> sampled = new List<Entity>();
        Random generator = new Random(0);
        for (int i = MemberCount - 1; i >= 0; i--)
        {
            if (generator.Next(i) <= leftToSample)
            {
                sampled.Add(Members[i]);
                leftToSample--;
                if (leftToSample == 0) break;
            }
        }
        Sample output = new Sample(sampled);
        return output;
    }
    public Sample SampleEntities(double sampleSize)
    {
        if (sampleSize > 1.0f) return SampleEntities((int) sampleSize);
        return SampleEntities((int)(sampleSize * MemberCount));
    }
    public Sample SampleEntities(float sampleSize) => SampleEntities((float) sampleSize);

}