namespace Statistics;

class EntityString : EntityBase, IComparable<EntityString>
{
    public override string MagicValue {get; }

    public override bool IsNumeric {get; } = false;
    public override int MagicNumber => throw new NotImplementedException();
    public EntityString(string magicValue)
    {
        this.MagicValue = magicValue;
    }

    // Comparison operators that are useless, but I'm too lazy to remove. 
    public static bool operator ==(EntityString a, EntityString b) => a.MagicValue == b.MagicValue;
    public static bool operator !=(EntityString a, EntityString b) => a.MagicValue != b.MagicValue;
    public override bool Equals(object? obj)
    {
        // Avoid a NullPtrException.
        if (ReferenceEquals(null, obj)) return false;
        // Skip if if they are the same reference.
        if (ReferenceEquals(this, obj)) return true;
        // Make sure they are the same type. 
        if (obj.GetType() != this.GetType()) return false;

        return Equals((EntityString) obj);
    }
    public bool Equals(EntityString ent) => this.MagicValue == ent.MagicValue;
    public override int GetHashCode() => base.GetHashCode();

    // Screw it, IComparable. 
    public int CompareTo(EntityString? other) 
    {
        if (ReferenceEquals(null, other)) return -1;
        return this.MagicValue.CompareTo(other.MagicValue);   
    }
}