using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class CardManager : MonoBehaviour
{
    [SerializeField] private List<TrainCard> uniqueTrainCards;
    [SerializeField] private List<DestinationCard> destinationCards;

    private CardDeck<TrainCard> trainCardDeck;
    private CardDeck<DestinationCard> destinationCardDeck;

    public List<TrainCard> OfferPile { get; private set; } = new List<TrainCard>();
    private const int OfferSize = 5;

    public GamePlayController gamePlayController;

    private bool isFirstUpdate = true;

    private void Awake()
    {
    trainCardDeck = new CardDeck<TrainCard>(new List<TrainCard>());
    destinationCardDeck = new CardDeck<DestinationCard>(destinationCards);

    foreach (TrainCard card in uniqueTrainCards)
    {
        trainCardDeck.AddCard(card, card.count);
    }
    }

    private void Start()
    {
    trainCardDeck.Shuffle();
    destinationCardDeck.Shuffle();

    for (int i = 0; i < OfferSize; i++)
        {
            OfferPile.Add(trainCardDeck.DrawCard());
        }

        CheckAndReplaceOfferPile();
        gamePlayController.PopulateOfferCards();
        
        
        // Debugging
        Debug.Log("Start method in CardManager called. OfferPile count: " + OfferPile.Count);
    }
    
    public TrainCard DrawFromDeck()
    {
        return trainCardDeck.DrawCard();
    }

    public TrainCard TakeFromOffer(int offerIndex)
    {
        TrainCard card = OfferPile[offerIndex];
        OfferPile[offerIndex] = trainCardDeck.DrawCard();  // Replace the card taken from the offer pile

        CheckAndReplaceOfferPile();

        return card;
    }


    private void CheckAndReplaceOfferPile()
    {
        if (OfferPile.Count(card => card.IsLocomotive) >= 3)
        {
            // Replace all cards in the offer pile
            for (int i = 0; i < OfferSize; i++)
            {
                OfferPile[i] = trainCardDeck.DrawCard();
            }
        }

        gamePlayController.PopulateOfferCards();

    // Debugging
    Debug.Log("CheckAndReplaceOfferPile method in CardManager called. OfferPile count: " + OfferPile.Count);
    }

}
