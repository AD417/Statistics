namespace Statistics;

class Entity
{
    public int MagicNumber {get;}
    private static Random Generator {get; set;} = new Random(1);

    public Entity()
    {
        this.MagicNumber = Generator.Next(1, 100);
    }

}