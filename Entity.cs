namespace Statistics;

class Entity
{
    public int MagicNumber {get; set;}
    public int MagicSecondNumber {get; set;}
    private static Random Generator {get; set;} = new Random(1);

    public Entity() : this(Generator.Next(1, 100)) {}
    public Entity(int MagicNumber) : this(MagicNumber, Generator.Next(1, 100)) {}
    public Entity(int MagicNumber, int MagicSecondNumber)
    {
        this.MagicNumber = MagicNumber;
        this.MagicSecondNumber = 100 - (MagicSecondNumber * MagicSecondNumber / 100);
    }

    // This was a waste of time. If something breaks, feel free to remove this. 
    public static bool operator >(Entity a, Entity b) => a.MagicNumber > b.MagicNumber;
    public static bool operator <(Entity a, Entity b) => a.MagicNumber < b.MagicNumber;
    public static bool operator ==(Entity a, Entity b) => a.MagicNumber == b.MagicNumber;
    public static bool operator !=(Entity a, Entity b) => a.MagicNumber != b.MagicNumber;
    public override bool Equals(object? obj)
    {
        // Avoid a NullPtrException.
        if (ReferenceEquals(null, obj)) return false;
        // Skip if if they are the same reference.
        if (ReferenceEquals(this, obj)) return true;
        // Make sure they are the same type. 
        if (obj.GetType() != this.GetType()) return false;

        return Equals((Entity) obj);
    }
    public bool Equals(Entity ent) => this.MagicNumber == ent.MagicNumber;
    public override int GetHashCode() => base.GetHashCode();
}