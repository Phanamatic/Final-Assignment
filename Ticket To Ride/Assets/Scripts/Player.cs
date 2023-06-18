using System.Collections.Generic;

public class Player
{
    public string PlayerName { get; private set; }
    public int PlayerID { get; private set; }
    public int Score { get; set; }
    public int TrainCarsLeft { get; set; }
    public List<TrainCard> TrainCards { get; private set; }
    public List<DestinationCard> DestinationCards { get; private set; }

    public Player(string playerName, int playerID)
    {
        PlayerName = playerName;
        PlayerID = playerID;
        Score = 0;
        TrainCarsLeft = 45;
        TrainCards = new List<TrainCard>();
        DestinationCards = new List<DestinationCard>();
    }

    public void AddTrainCard(TrainCard card)
    {
        TrainCards.Add(card);
    }

    public void AddDestinationCard(DestinationCard card)
    {
        DestinationCards.Add(card);
    }
}
