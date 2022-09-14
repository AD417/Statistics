namespace Statistics;

class Set
{
    public List<Entity> Members {get; set;} = new List<Entity>();

    public Set(int entities)
    {
        for (int i = 0; i < entities; i++)
        {
            this.Members.Add(new Entity());
        }
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
}