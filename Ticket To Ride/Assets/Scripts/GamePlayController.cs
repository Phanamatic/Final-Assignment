using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GamePlayController : MonoBehaviour
{
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public TextMeshProUGUI playerTurnText;
    public GameObject actionPanel;
    public GameObject drawTrainCardButtons;
    public List<Player> players;
    public int currentPlayerIndex = 0;
    public CardManager cardManager;
    public Image[] offerCardImages; 
    public Button[] offerCardButtons;
    public PlayerHandUIController  playerHandUIController;

    public GameObject destinationCardChoicePanel; 
    public Image[] destinationCardChoiceImages; 
    public Button[] destinationCardChoiceButtons; 
    public Button destinationCardChoiceConfirmButton; 
    public Button destinationCardChoiceResetButton; 
    public GameObject destinationCardChoiceConfirmationPrompt; 

    public List<DestinationCard> offeredDestinationCards = new List<DestinationCard>();
    public List<DestinationCard> chosenDestinationCards = new List<DestinationCard>();

    public Board board;
    public RouteManager obj = new RouteManager();
    public TMP_Dropdown dropDown;
    public List<RouteBase> data = new List<RouteBase>();
    

    public GameObject routechoosedropdown;
    public GameObject claimroutebutton;

    public TMP_Text scoreText;

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        players = GameController.Instance.players;

        Debug.Log("Number of players: " + players.Count);

        UpdateTurnUI();

        Debug.Log("Awake method in GamePlayController called");

        
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Start() 
    {
    StartGame();
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void UpdateTurnUI()
    {
        playerTurnText.text = $"{players[currentPlayerIndex].PlayerID}";
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void NextTurn()
    {
    // Check if the current player has picked their initial cards
    if (!players[currentPlayerIndex].hasFinishedInitialTurn)
        {
        OfferInitialDestinationCards();
        }
    else
        {
        players[currentPlayerIndex].HasDrawnFirstCard = false;
        Debug.Log("Current player index before update: " + currentPlayerIndex);

        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        Debug.Log("Current player index after update: " + currentPlayerIndex);
        
        UpdateCardButtons();

        for (int i = 0; i < cardManager.OfferPile.Count; i++)
        {
            offerCardButtons[i].gameObject.SetActive(false);
        }

        drawTrainCardButtons.SetActive(false);

        UpdateTurnUI();
        playerHandUIController.UpdatePlayerHandUI(players[currentPlayerIndex]);

        // Handle destination card picking for the new player
        if (!players[currentPlayerIndex].hasFinishedInitialTurn)
        {
            OfferInitialDestinationCards();
        }

        // Only show the actionPanel if all players have finished their initial turn
        if (AllPlayersFinishedInitialTurn())
        {
            actionPanel.SetActive(true);
        }

        destinationCardChoiceConfirmButton.interactable = false;

        Debug.Log("Entering NextTurn:");
        Debug.Log("Draws left: " + players[currentPlayerIndex].DrawsLeft);
        Debug.Log("Has drawn first card: " + players[currentPlayerIndex].HasDrawnFirstCard);
        }
    }


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void UpdateScoreUI(Player p)
    {
        string score = p.CalculateScore().ToString();
        scoreText.text = score;
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public bool AllPlayersFinishedInitialTurn()
    {
    foreach (Player player in players)
    {
        if (!player.hasFinishedInitialTurn)
        {
            return false;
        }
    }
    return true;
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void DrawTrainCard()
    {
        players[currentPlayerIndex].HasDrawnFirstCard = false;
        players[currentPlayerIndex].DrawsLeft = 2;
        actionPanel.SetActive(false); 
        drawTrainCardButtons.SetActive(true);
        UpdateCardButtons();

        Debug.Log("DrawTrainCard method in GamePlayController called");

    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void DrawCardFromDeck()
    {
    TrainCard drawnCard = cardManager.DrawFromDeck();
    players[currentPlayerIndex].AddTrainCard(drawnCard);
    players[currentPlayerIndex].DrawsLeft--;

    playerHandUIController.UpdatePlayerHandUI(players[currentPlayerIndex]);

    if (players[currentPlayerIndex].HasDrawnFirstCard || players[currentPlayerIndex].DrawsLeft == 0)
    {
        NextTurn();
    }
    else
    {
        players[currentPlayerIndex].HasDrawnFirstCard = true;
        UpdateCardButtons();
    }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void DrawCardFromOffer(int offerIndex)
    {
    TrainCard offerCard = cardManager.TakeFromOffer(offerIndex);
    


    Sprite oldImage = offerCardImages[offerIndex].sprite;

    Sprite newImage = cardManager.OfferPile[offerIndex].image;

    playerHandUIController.UpdatePlayerHandUI(players[currentPlayerIndex]);

    StartCoroutine(FadeIn(newImage, oldImage, offerCardImages[offerIndex]));

    players[currentPlayerIndex].AddTrainCard(offerCard);
    players[currentPlayerIndex].DrawsLeft--;

    playerHandUIController.UpdatePlayerHandUI(players[currentPlayerIndex]);


    if ((offerCard.IsLocomotive && !players[currentPlayerIndex].HasDrawnFirstCard) || players[currentPlayerIndex].DrawsLeft == 0)
    {
        Debug.Log("Going to the next turn...");
        NextTurn();
    }
    else
    {
        if (!players[currentPlayerIndex].HasDrawnFirstCard)
        {
            players[currentPlayerIndex].HasDrawnFirstCard = true;
        }
        UpdateCardButtons();
    }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void UpdateCardButtons()
    {
    bool hasDrawnFirstCard = players[currentPlayerIndex].HasDrawnFirstCard;
    for (int i = 0; i < cardManager.OfferPile.Count; i++)
    {
        bool isLocomotive = cardManager.OfferPile[i].IsLocomotive;
        offerCardButtons[i].gameObject.SetActive(true);
        offerCardButtons[i].interactable = !hasDrawnFirstCard || !isLocomotive;
    }
    drawTrainCardButtons.SetActive(players[currentPlayerIndex].DrawsLeft > 0);
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void DrawDestinationCard()
    {
    actionPanel.SetActive(false);

    offeredDestinationCards.Clear();

    destinationCardChoicePanel.SetActive(true);

    for (int i = 0; i < 3; i++)
    {
        DestinationCard card = cardManager.DrawDestinationCard();
        offeredDestinationCards.Add(card);
        destinationCardChoiceImages[i].sprite = card.image; 
        destinationCardChoiceButtons[i].gameObject.SetActive(true);
        destinationCardChoiceButtons[i].interactable = true;
    }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    public void ClaimRoute()
    {
         routechoosedropdown.SetActive(false);
         claimroutebutton.SetActive(false);

        //Ivy will add systems here
        Player current = players[currentPlayerIndex];
        int cardsleft = current.TrainCards.Count;
        int carsleft = current.TrainCarsLeft;
        bool destn = false;

        DropDownItemSelected(dropDown);
        dropDown.onValueChanged.AddListener(delegate { DropDownItemSelected(dropDown); });

        void DropDownItemSelected(TMP_Dropdown dropdown)
        {

            int index = dropdown.value;
            obj.ClaimRoute(data[index], current);
            Debug.Log(obj.ClaimRoute(data[index], current) + " " + "RouteName:" + data[index].getRouteName());

            if(obj.ClaimRoute(data[index], current)== true)
            {
                current.RoutesClaimed.Add(data[index]);
                current.RoutesConnectedClaimed = obj.ConnectedRoutes(current, data[index]);
                destn = obj.DestinationTicketAcheived(current, current.RoutesConnectedClaimed);
                UpdateScoreUI(current);

               // current.ConnectedCities.Add(data[index].getStart());
               // current.ConnectedCities.Add(data[index].getEnd());
                cardsleft = cardsleft - data[index].getNodes();
                carsleft--;//do a check if there are less than 2 cars, only one more next turn and then game must end...use a while loop

                NextTurn();
            }
            else
            {
                NextTurn();
            }
           
        }
   
    }
    

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void DrawTrainCardFromDeck()
    {
        TrainCard card = cardManager.DrawFromDeck();
        players[currentPlayerIndex].AddTrainCard(card);
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void PopulateOfferCards()
    {    
    for (int i = 0; i < cardManager.OfferPile.Count; i++)
    {
        offerCardImages[i].sprite = cardManager.OfferPile[i].image;
    }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public IEnumerator FadeIn(Sprite newImage, Sprite oldImage, Image imageToFade)
    {
    imageToFade.sprite = oldImage; // Set the old sprite

    // Fade out
    float fadeOutDuration = 0.1f; // Duration for fading out
    while (imageToFade.color.a > 0)
    {
        Color c = imageToFade.color;
        c.a -= Time.deltaTime / fadeOutDuration;
        imageToFade.color = c;
        yield return null;
    }

    // Reset the alpha to 0
    Color tempColor = imageToFade.color;
    tempColor.a = 0;
    imageToFade.color = tempColor;

    imageToFade.sprite = newImage; // Set the new sprite

    // Fade in
    float fadeInDuration = 0.1f; // Duration for fading in
    while (imageToFade.color.a < 1)
    {
        Color c = imageToFade.color;
        c.a += Time.deltaTime / fadeInDuration;
        imageToFade.color = c;
        yield return null;
    }

    tempColor = imageToFade.color;
    tempColor.a = 1;
    imageToFade.color = tempColor;
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void ClearPlayerHand()
    {
        Player currentPlayer = players[currentPlayerIndex];

        foreach (Transform child in currentPlayer.TrainCardHandArea.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in currentPlayer.DestinationCardHandArea.transform)
        {
            Destroy(child.gameObject);
        }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void DrawPlayerHand()
    {
        Player currentPlayer = players[currentPlayerIndex];

        foreach (TrainCard card in currentPlayer.TrainCards)
        {
            currentPlayer.AddTrainCard(card);
        }

        foreach (DestinationCard card in currentPlayer.DestinationCards)
        {
            currentPlayer.AddDestinationCard(card);
        }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void OfferInitialDestinationCards()
    {
    offeredDestinationCards.Clear();

    for (int i = 0; i < 3; i++)
    {
        DestinationCard card = cardManager.DrawDestinationCard();
        offeredDestinationCards.Add(card);

        destinationCardChoiceImages[i].sprite = card.image; 
        destinationCardChoiceButtons[i].gameObject.SetActive(true);
        destinationCardChoiceButtons[i].interactable = true;
    }

    destinationCardChoicePanel.SetActive(true);

    players[currentPlayerIndex].hasFinishedInitialTurn = false;
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void OnDestinationCardChosen(int index)
    {
    DestinationCard chosenCard = offeredDestinationCards[index];
    
    offeredDestinationCards[index] = null;

    chosenDestinationCards.Add(chosenCard);

    destinationCardChoiceButtons[index].interactable = false;

    UpdateConfirmButtonStatus();
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void OnDestinationCardChoiceConfirmed()
    {
    destinationCardChoiceConfirmationPrompt.SetActive(true);
    destinationCardChoicePanel.SetActive(false);
    players[currentPlayerIndex].hasFinishedInitialTurn = true;
    }


    public void OnDestinationCardChoiceConfirmationPromptYes()
    {
    foreach (DestinationCard card in chosenDestinationCards)
    {
        players[currentPlayerIndex].AddDestinationCard(card);
    }

    offeredDestinationCards.Clear();
    chosenDestinationCards.Clear();

    destinationCardChoiceConfirmationPrompt.SetActive(false);

    players[currentPlayerIndex].hasFinishedInitialTurn = true;

    NextTurn();
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void OnDestinationCardChoiceConfirmationPromptNo()
    {
    destinationCardChoiceConfirmationPrompt.SetActive(false);
    destinationCardChoicePanel.SetActive(true);
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void OnDestinationCardChoiceReset()
    {
    for (int i = 0; i < offeredDestinationCards.Count; i++)
    {
        if (offeredDestinationCards[i] == null && chosenDestinationCards.Count > 0)
        {
            DestinationCard chosenCard = chosenDestinationCards[0];
            chosenDestinationCards.RemoveAt(0);

            offeredDestinationCards[i] = chosenCard;
        }
    }

    offeredDestinationCards.RemoveAll(card => card == null);

    for (int i = 0; i < offeredDestinationCards.Count; i++)
    {
        if(i < destinationCardChoiceButtons.Length && i < destinationCardChoiceImages.Length)
        {
            destinationCardChoiceButtons[i].interactable = true;
            destinationCardChoiceImages[i].sprite = offeredDestinationCards[i].image;
        }
    }

    UpdateConfirmButtonStatus();
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void UpdateConfirmButtonStatus()
    {   
    destinationCardChoiceConfirmButton.interactable = chosenDestinationCards.Count >= 1;
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void RouteClaimChosen()
    {
        
        routechoosedropdown.SetActive(true);
       
        claimroutebutton.SetActive(true);

        actionPanel.SetActive(false);


        dropDown = dropDown.transform.GetComponent<TMP_Dropdown>();

        dropDown.ClearOptions();

        //creates a new list
        board = new Board();

        for (int i = 0; i < board.Routes.GeneralList.Count; i++)
        {
            data.Add(board.Routes.GeneralList[i]);
        }

        foreach (RouteBase t in data)
        {
            dropDown.options.Add(new TMP_Dropdown.OptionData() { text = t.getRouteName() });
        }

      



    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void StartGame() 
    {
        
    cardManager.destinationCardDeck.Shuffle();
    cardManager.trainCardDeck.Shuffle();

    foreach (Player player in players)
    {
        
        for (int i = 0; i < 4; i++)
        {
            player.AddTrainCard(cardManager.trainCardDeck.DrawCard());
            RouteClaimChosen();
        }
    }
        
    for (int i = 0; i < players.Count; i++)
    {
        
        players[i].hasFinishedInitialTurn = false;
        currentPlayerIndex = i;
        

        NextTurn();
    }

    // Reset current player to the first player
    currentPlayerIndex = 0;

    UpdateTurnUI();
    playerHandUIController.UpdatePlayerHandUI(players[currentPlayerIndex]);

    Debug.Log("StartGame method in GamePlayController called");
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}


