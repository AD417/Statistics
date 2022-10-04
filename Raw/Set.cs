namespace Statistics;

class Set
{
    public List<EntityBase> Members {get; set;} = new List<EntityBase>();
    public bool IsQualitative {get => !IsQuantitative; }
    public bool IsQuantitative {get; }

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
        IsQuantitative = entityList[0].IsNumeric;
    }

    public List<int> AllMagicNumbers()
    {
        if (IsQualitative) throw new TypeAccessException("Data set is qualitative, and has no numbers");
        return Members.Select(ent => ent.MagicNumber).ToList();
    }
    public List<string> AllMagicValues()
    {
        // NOTE: should I return a list of all the numbers, stringified, anyways? 
        if (IsQuantitative) throw new TypeAccessException("Data set is quantitative, and has no strings");
        return Members.Select(ent => ent.MagicValue).ToList();
    }
}