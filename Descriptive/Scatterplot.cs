using Plotly.NET.CSharp;

namespace Statistics;

class Scatterplot
{
    public Set SetX {get; }
    public Set SetY {get; }
    public Scatterplot(Set dataX, Set dataY)
    {
        SetX = dataX;
        SetY = dataY;
    }
    public static Scatterplot ImportFromCSV(string filePath)
    {
        string import = File.ReadAllText(filePath);
        string[] importByEntity = import.Split("\n");
        List<Entity> dataX = new List<Entity>();
        List<Entity> dataY = new List<Entity>();

        for (int i = 0; i < importByEntity.Length; i++)
        {
            string line = importByEntity[i];
            string[] values = line.Split(", ");
            if (values.Length < 2) continue;
            dataX.Add(new Entity(Int32.Parse(values[0])));
            dataY.Add(new Entity(Int32.Parse(values[1])));
        }
        return new Scatterplot(new Population(dataX), new Population(dataY)); 
    }

    public void DisplayScatterplot()
    {
        Chart.Point<double, double, string>(
            x: SetX.Members.Select(x => (double)x.MagicNumber).ToArray(),
            y: SetY.Members.Select(x => (double)x.MagicNumber).ToArray()
        )
            .WithXAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("Students per teacher"))
            .WithYAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("Salary per hour"))
            .Show();
    }
}