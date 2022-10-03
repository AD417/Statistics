namespace Statistics;

class Set
{
    public List<EntityBase> Members {get; set;} = new List<EntityBase>();
    public bool IsQualitative {get; }

    public int MemberCount {get => Members.Count();}

    public double MeanMagicNumber 
    { 
        get 
        {
            if (IsQualitative) throw new TypeAccessException("cannot get the mean of a qualitative set of data");
            return Members.Average(ent => ent.MagicNumber);
        }
    }
    public int MinMagicNumber 
    { 
        get 
        {
            if (IsQualitative) throw new TypeAccessException("cannot get the minimum of a qualitative set of data");
            return Members.Min(ent => ent.MagicNumber);
        }
    }
    public int MaxMagicNumber 
    { 
        get 
        {
            if (IsQualitative) throw new TypeAccessException("cannot get the maximum of a qualitative set of data");
            return Members.Max(ent => ent.MagicNumber);
        }
    }

    public Set(List<EntityBase> entityList)
    {
        Members = entityList;
        IsQualitative = !entityList[0].IsNumeric;
    }

    public List<int> AllMagicNumbers()
    {
        List<int> output = new List<int>();
        foreach (EntityBase ent in Members)
        {
            output.Add(ent.MagicNumber);
        }
        return output;
    }
    public List<string> AllMagicValues()
    {
        List<string> output = new List<string>();
        foreach (EntityBase ent in Members)
        {
            output.Add(ent.MagicValue);
        }
        return output;
    }
}