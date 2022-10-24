namespace Statistics;

abstract class Set
{
    public List<Entity> Members {get; set;} = new List<Entity>();

    public int MemberCount {get => Members.Count();}
    public double MeanMagicNumber { get => Members.Average(ent => ent.MagicNumber);}
    public int MinMagicNumber { get => Members.Min(ent => ent.MagicNumber);}
    public int MaxMagicNumber { get => Members.Max(ent => ent.MagicNumber);}

    public abstract bool IsPopulation {get;}

    public Set(int entities) : this(entities, true) {}
    public Set(int entities, bool randomize)
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

    public List<int> AllMagicNumbers()
    {
        List<int> output = new List<int>();
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