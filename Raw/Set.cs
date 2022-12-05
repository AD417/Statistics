namespace Statistics;

abstract class Set
{
    public List<Entity> Members {get; set;} = new List<Entity>();

    public int MemberCount {get => Members.Count();}
    public double MeanMagicNumber { get => Members.Average(ent => ent.MagicNumber);}
    public double MinMagicNumber { get => Members.Min(ent => ent.MagicNumber);}
    public double MaxMagicNumber { get => Members.Max(ent => ent.MagicNumber);}

    public abstract bool IsPopulation {get;}
    public Set(int entities, bool randomize = false)
    {
        if (randomize) 
        {
            for (int i = 0; i < entities; i++)
                this.Members.Add(new Entity());
        }
        else 
        {
            for (int i = 0; i < entities; i++)
                this.Members.Add(new Entity(0));
        }
    }
    public Set(List<Entity> entityList)
    {
        Members = entityList;
        Members.Sort();
    }

    public List<double> AllMagicNumbers()
    {
        List<double> output = new List<double>();
        for (int entity = 0; entity < Members.Count; entity++)
        {
            output.Add(Members[entity].MagicNumber);
        }
        return output;
    }
    public string MagicNumbersAsPrintable()
    {
        return String.Join(",", AllMagicNumbers());
    }
}