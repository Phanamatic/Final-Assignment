using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player
{
    public string PlayerName { get; private set; }
    public int PlayerID { get; private set; }
    public int Score { get; set; }
    public int TrainCarsLeft { get; set; }
    public List<TrainCard> TrainCards { get; set; }
    public List<DestinationCard> DestinationCards { get; set; }
    public int DrawsLeft { get; set; }
    public bool HasDrawnFirstCard { get; set; }

    public GameObject TrainCardHandArea; 
    public GameObject DestinationCardHandArea; 

    public bool hasFinishedInitialTurn;

    public Player(string playerName, int playerID)
    {
        PlayerName = playerName;
        PlayerID = playerID;
        Score = 0;
        TrainCarsLeft = 45;
        TrainCards = new List<TrainCard>();
        DestinationCards = new List<DestinationCard>();
        DrawsLeft = 2;
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

