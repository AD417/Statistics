namespace Statistics;

class EntityInt : EntityBase, IComparable<EntityInt>
{
    public override int MagicNumber {get; }

    public override bool IsNumeric {get; } = true;
    public override string MagicValue => throw new NotImplementedException();
    private static Random Generator {get;} = new Random(1);

    public EntityInt() : this(Generator.Next(1, 100)) {}
    public EntityInt(int MagicNumber) : this(MagicNumber, Generator.Next(1, 100)) {}
    public EntityInt(int MagicNumber, int MagicSecondNumber)
    {
        this.MagicNumber = MagicNumber;
    }

    // Comparison operators that are useless, but I'm too lazy to remove. 
    public static bool operator >(EntityInt a, EntityInt b) => a.MagicNumber > b.MagicNumber;
    public static bool operator <(EntityInt a, EntityInt b) => a.MagicNumber < b.MagicNumber;
    public static bool operator ==(EntityInt a, EntityInt b) => a.MagicNumber == b.MagicNumber;
    public static bool operator !=(EntityInt a, EntityInt b) => a.MagicNumber != b.MagicNumber;
    public override bool Equals(object? obj)
    {
        // Avoid a NullPtrException.
        if (ReferenceEquals(null, obj)) return false;
        // Skip if if they are the same reference.
        if (ReferenceEquals(this, obj)) return true;
        // Make sure they are the same type. 
        if (obj.GetType() != this.GetType()) return false;

        return Equals((EntityInt) obj);
    }
    public bool Equals(EntityInt ent) => this.MagicNumber == ent.MagicNumber;
    public override int GetHashCode() => base.GetHashCode();

    // Screw it, IComparable. 
    public int CompareTo(EntityInt? other) 
    {
        if (ReferenceEquals(null, other)) return this.MagicNumber;
        return this.MagicNumber.CompareTo(other.MagicNumber);   
    }
}