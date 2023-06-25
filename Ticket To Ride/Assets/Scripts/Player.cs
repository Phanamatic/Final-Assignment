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

    public List<City> ConnectedCities= new List<City>();
    public List<RouteBase> RoutesClaimed = new List<RouteBase>();
    public List<RouteBase> RoutesConnectedClaimed = new List<RouteBase>();
    
    public Board board;
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

    public int CalculateScore()
    {
        int score = 0;

        // Calculate the score based on the player's actions and achievements
        // You can assign different point values to various actions or achievements

        // Example: Increase score for completing routes
        foreach (RouteBase routeclaimed in RoutesClaimed)
        {
            score += routeclaimed.points();
        }

        return score;
    }

    public int CalculateFinalScore()
    {
        int score = CalculateScore();

        for (int i = 0; i < RoutesConnectedClaimed.Count; i++)
        {
            if (DestinationCards[i].endCity != RoutesConnectedClaimed[i].getEnd().ToString() || DestinationCards[i].startCity != RoutesConnectedClaimed[i].getStart().ToString()
                || DestinationCards[i].endCity != RoutesConnectedClaimed[i].getStart().ToString() || DestinationCards[i].startCity != RoutesConnectedClaimed[i].getEnd().ToString())
            {
                score -= DestinationCards[i].points;
            }
            else
            {
                score += DestinationCards[i].points;
            }
        }

        return 0;
    }


}

