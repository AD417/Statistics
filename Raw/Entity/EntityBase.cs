namespace Statistics;

abstract class EntityBase
{
    public abstract bool IsNumeric {get; }

    public abstract int MagicNumber {get; }
    public abstract string MagicValue {get; }

}