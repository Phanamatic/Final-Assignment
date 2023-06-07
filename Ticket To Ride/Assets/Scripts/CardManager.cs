using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private List<TrainCard> uniqueTrainCards;
    [SerializeField] private List<DestinationCard> destinationCards;

    private CardDeck<TrainCard> trainCardDeck;
    private CardDeck<DestinationCard> destinationCardDeck;

    private void Awake()
    {
        trainCardDeck = new CardDeck<TrainCard>(new List<TrainCard>());
        destinationCardDeck = new CardDeck<DestinationCard>(new List<DestinationCard>(destinationCards));

        foreach (TrainCard card in uniqueTrainCards)
        {
            trainCardDeck.AddCard(card, card.count);
        }

        trainCardDeck.Shuffle();
        destinationCardDeck.Shuffle();
    }
}
