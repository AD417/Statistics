namespace Statistics;

class Population : Set
{
    public int MemberCount {get => Members.Count();}
    public double AverageMagicNumber 
    {
        get
        {
            double total = 0;
            for (int i = 0; i < Members.Count(); i++)
            {
                total += Members[i].MagicNumber;
            }
            return total / Members.Count();
        }
    }

    public Population(int entities) : base(entities) {}
}