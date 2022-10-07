namespace Statistics;

class Sample : Set 
{
    public override bool IsPopulation {get => false; }
    public Sample(int entities) : base(entities) {}
    public Sample(List<Entity> entityList) : base(entityList) {}
    // Do it twice, grumble about it, but fine. 
    public static Sample ImportFromCSV(string filePath)
    {
        string import = File.ReadAllText(filePath);
        string[] importByEntity = import.Split("\n");
        List<Entity> entities = new List<Entity>();


        for (int i = 0; i < importByEntity.Length; i++)
        {
            string line = importByEntity[i];
            if (line.Length == 0) continue;
            string[] values = line.Split(", ");
            if (values.Length < 1) continue;
            entities.Add(new Entity(Int32.Parse(values[0])));
        }
        return new Sample(entities);    
    }
}