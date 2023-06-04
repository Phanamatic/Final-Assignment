public class TrainCard
{
    public enum CardColor
    {
        Red,
        Blue,
        Green,
        Yellow,
        Black,
        White,
        Pink,
        Orange,
        Locomotive // wild card
    }

    public CardColor Color { get; private set; }

    public TrainCard(CardColor color)
    {
        Color = color;
    }
}
