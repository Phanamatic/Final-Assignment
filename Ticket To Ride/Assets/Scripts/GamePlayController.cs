using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GamePlayController : MonoBehaviour
{

    public TextMeshProUGUI playerTurnText;
    public GameObject actionPanel;
    public GameObject drawTrainCardButtons;
 //   public GameObject offerCardButtons;
 //   public GameObject drawDestinationCardButton;

    public List<Player> players;
    public int currentPlayerIndex = 0;
    public CardManager cardManager;

    public GameObject cardParent; // The parent GameObject where the cards will be spawned.
    public GameObject cardPrefab; // The card prefab.
 

    private void Start()
    {
        players = GameController.Instance.players;

        Debug.Log("Number of players: " + players.Count);

        // Initialize the CardManager
        cardManager = GetComponent<CardManager>();

        // Initialize UI
        UpdateTurnUI();
        actionPanel.SetActive(true);

        // Debugging
        Debug.Log("Start method in GamePlayController called");
    }

    public void UpdateTurnUI()
    {
        playerTurnText.text = $"{players[currentPlayerIndex].PlayerID}";
    }

    public void NextTurn()
    {
        Debug.Log("Current player index before update: " + currentPlayerIndex);
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        Debug.Log("Current player index after update: " + currentPlayerIndex);
        UpdateTurnUI();
        actionPanel.SetActive(true);
    }

    public void DrawTrainCard()
    {
        actionPanel.SetActive(false);
        drawTrainCardButtons.SetActive(true);
    //    offerCardButtons.SetActive(true);

        // Debugging
        Debug.Log("DrawTrainCard method in GamePlayController called");
    }

    public void DrawCardFromDeck()
    {
        TrainCard drawnCard = cardManager.DrawFromDeck();
        players[currentPlayerIndex].AddTrainCard(drawnCard);
        // After drawing the first card, the player can't take a locomotive card from the offer pile
        for (int i = 0; i < 5; i++)
        {
            if (cardManager.OfferPile[i].IsLocomotive)
            {
    //            offerCardButtons.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void DrawCardFromOffer(int offerIndex)
    {
        TrainCard offerCard = cardManager.TakeFromOffer(offerIndex);
        players[currentPlayerIndex].AddTrainCard(offerCard);
        PopulateOfferCards();
        if (offerCard.IsLocomotive)
        {
            NextTurn();
        }
        else
        {
            drawTrainCardButtons.SetActive(false);
            // The player can't take another locomotive card from the offer pile
            for (int i = 0; i < 5; i++)
            {
                if (cardManager.OfferPile[i].IsLocomotive)
                {
             //       offerCardButtons.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    public void DrawDestinationCard()
    {
        actionPanel.SetActive(false);
     //   drawDestinationCardButton.SetActive(true);
        // TODO: Implement the draw destination card logic
    }

    public void ClaimRoute()
    {
    actionPanel.SetActive(false);
    // TODO: Implement the claim route logic

    // After claiming the route, move to the next player's turn
    NextTurn();
    }

    public void DrawTrainCardFromDeck()
    {
        TrainCard card = cardManager.DrawFromDeck();
        players[currentPlayerIndex].AddTrainCard(card);
    }

    public void PopulateOfferCards()
{
    // Destroy old cards
    foreach (Transform child in cardParent.transform) 
    {
        Destroy(child.gameObject);
    }
    
    // Spawn new cards
    for (int i = 0; i < cardManager.OfferPile.Count; i++)
    {
        GameObject cardObject = Instantiate(cardPrefab, cardParent.transform);
        
        // Set the card's image
        cardObject.GetComponent<Image>().sprite = Sprite.Create(cardManager.OfferPile[i].image, new Rect(0.0f, 0.0f, cardManager.OfferPile[i].image.width, cardManager.OfferPile[i].image.height), new Vector2(0.5f, 0.5f), 100.0f);

        // Set up the button's onClick event
        int offerIndex = i; // Create a copy of i to use in the delegate

        Button button = cardObject.GetComponent<Button>();
        button.onClick.RemoveAllListeners(); // Remove all existing listeners
        button.onClick.AddListener(delegate { DrawCardFromOffer(offerIndex); });
    }
}


}