public class DestinationCard
{
    public string StartCity { get; private set; }
    public string EndCity { get; private set; }
    public int PointValue { get; private set; }

    public DestinationCard(string startCity, string endCity, int pointValue)
    {
        StartCity = startCity;
        EndCity = endCity;
        PointValue = pointValue;
    }
}
