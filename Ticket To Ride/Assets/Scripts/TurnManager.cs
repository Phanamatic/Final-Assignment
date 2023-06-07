using UnityEngine.UI
using UnityEngine

public class TurnManager
{
    private int currentPlayerIndex;
    private List<Player> players;

    public TurnManager(List<Player> players)
    {
        this.players = players;
        currentPlayerIndex = 0;
    }

    public Player GetCurrentPlayer()
    {
        return players[currentPlayerIndex];
    }

    public void MoveToNextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
    }
}