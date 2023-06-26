using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
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
    public TMP_Text carsText;

    public TextMeshProUGUI[] startCityTexts;
    public TextMeshProUGUI[] endCityTexts;
    public TextMeshProUGUI[] pointsTexts;

    public GameObject emptyTrainDeckPanel;
    public GameObject emptyDestinationDeckPanel;
    public Button drawTrainButton;
    public Button drawDestinationButton;

    public TMPro.TextMeshProUGUI deckCountText;
    public Button trainDrawTurnButton;

    public Button endTurnButton;

    List<RouteBase> possibleRoutes = new List<RouteBase>();

    public Button claimRouteButton;


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

    private void Update()
    {
        UpdateDeckButton();

        Player current = players[currentPlayerIndex];

        claimRouteButton.interactable = AreThereClaimableRoutes(current);
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
        UpdateDeckButton();

        for (int i = 0; i < cardManager.OfferPile.Count; i++)
        {
            offerCardButtons[i].gameObject.SetActive(false);
        }

        drawTrainCardButtons.SetActive(false);

        UpdateDeckCountText();
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
        int scoreint = p.CalculateScore();
        string score = scoreint.ToString();
        scoreText.text = score;
    }

    public void UpdateTrainCars(Player p)
    {
        //string cars = p.RemoveTrainCars().ToString();
        //carsText.text = cars;
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
        drawTrainButton.interactable = (cardManager.trainCardDeck.Count > 0);

        Debug.Log("DrawTrainCard method in GamePlayController called");

    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void DrawCardFromDeck()
    {
    TrainCard drawnCard = cardManager.DrawFromDeck();
    UpdateDeckCountText();
    players[currentPlayerIndex].AddTrainCard(drawnCard);
    players[currentPlayerIndex].DrawsLeft--;

    UpdateDeckButton();

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
        if (offerCard == null)
        {
        offerCardButtons[offerIndex].gameObject.SetActive(false);
        offerCardImages[offerIndex].gameObject.SetActive(false);
        return;
        }

        // Check if the offer card was replaced. If not, disable its button.
        if (cardManager.OfferPile[offerIndex] == null)
        {
        offerCardButtons[offerIndex].gameObject.SetActive(false);
        }

        if (offerCard == null)
        {
        offerCardButtons[offerIndex].gameObject.SetActive(false);
        offerCardImages[offerIndex].gameObject.SetActive(false);
        return;
        }

    Sprite oldImage = offerCardImages[offerIndex].sprite;

    Sprite newImage = cardManager.OfferPile[offerIndex]?.image;

    playerHandUIController.UpdatePlayerHandUI(players[currentPlayerIndex]);

    StartCoroutine(FadeIn(newImage, oldImage, offerCardImages[offerIndex]));

    players[currentPlayerIndex].AddTrainCard(offerCard);
    players[currentPlayerIndex].DrawsLeft--;

    playerHandUIController.UpdatePlayerHandUI(players[currentPlayerIndex]);

    UpdateDeckButton();
    UpdateCardButtons();
    UpdateDeckCountText();

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
        bool isLocomotive = cardManager.OfferPile[i]?.IsLocomotive ?? false;
        offerCardButtons[i].gameObject.SetActive(cardManager.OfferPile[i] != null);
        offerCardButtons[i].interactable = !hasDrawnFirstCard || !isLocomotive;
    }
    drawTrainButton.interactable = players[currentPlayerIndex].DrawsLeft > 0;
    UpdateDeckCountText();
    }


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void DrawDestinationCard()
{
    int cardsToDraw = 3;

    actionPanel.SetActive(false);

    offeredDestinationCards.Clear();

    destinationCardChoicePanel.SetActive(true);

    for (int i = 0; i < cardsToDraw; i++)
    {
        if (cardManager.destinationCardDeck.IsEmpty() && i == 0)
        {
            destinationCardChoicePanel.SetActive(false);
            emptyDestinationDeckPanel.SetActive(true);
            return;
        }

        DestinationCard card;

        if (cardManager.destinationCardDeck.IsEmpty())
        {
            break;
        }
        else
        {
            card = cardManager.DrawDestinationCard();
            offeredDestinationCards.Add(card);
        }

        if(card != null)
        {
            destinationCardChoiceImages[i].sprite = card.image;
            destinationCardChoiceButtons[i].gameObject.SetActive(true);
            destinationCardChoiceButtons[i].interactable = true;

            startCityTexts[i].text = card.startCity;
            endCityTexts[i].text = card.endCity;
            pointsTexts[i].text = card.points.ToString();
        }
    }

    for (int i = offeredDestinationCards.Count; i < cardsToDraw; i++)
    {
        destinationCardChoiceButtons[i].gameObject.SetActive(false);
        destinationCardChoiceImages[i].gameObject.SetActive(false);
    }
}





//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    public void ClaimRoute()
    {
    Player current = players[currentPlayerIndex];
    int index = dropDown.value;

    bool claim = obj.ClaimRoute(data[index], current);
    Debug.Log(claim + " " + "RouteName:" + data[index].getRouteName());

    if (claim)
    {
        current.RoutesClaimed.Add(data[index]);
        current.RoutesConnectedClaimed = obj.ConnectedRoutes(current, data[index]);
        bool destn = obj.DestinationTicketAcheived(current, current.RoutesConnectedClaimed);

        UpdateScoreUI(current);
        UpdateTrainCars(current);

        current.ConnectedCities.Add(data[index].getStart());
        current.ConnectedCities.Add(data[index].getEnd());

        obj.GeneralList.Add(data[index]);

        data.Remove(data[index]); // Remove from dropdown list when a route is claimed 

        // Update dropDown options
        dropDown.ClearOptions();
        List<string> options = new List<string>();
        foreach (RouteBase route in data) 
        {
            options.Add(route.getRouteName());
        }
        dropDown.AddOptions(options);

        NextTurn();
    }
    else
    {
        NextTurn();
    }
    }



//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*void DropDownItemSelected(TMP_Dropdown dropdown)
    {
        Player current = players[currentPlayerIndex];

        int carsleft = current.TrainCarsLeft;

        bool destn = false;
        bool claim = false;


        int index = dropdown.value;
        claim = obj.ClaimRoute(data[index], current);
        Debug.Log(claim + " " + "RouteName:" + data[index].getRouteName());

        if (claim == true)
        {
            current.RoutesClaimed.Add(data[index]);
            current.RoutesConnectedClaimed = obj.ConnectedRoutes(current, data[index]);
            destn = obj.DestinationTicketAcheived(current, current.RoutesConnectedClaimed);

            UpdateScoreUI(current);
            UpdateTrainCars(current);

            current.ConnectedCities.Add(data[index].getStart());
            current.ConnectedCities.Add(data[index].getEnd());

            obj.GeneralList.Add(data[index]);

            data.Remove(data[index]);//remove from dropdown list whe a route is claimed 
            claim = false;
            NextTurn();
        }
        else
        {
            NextTurn();
        }

    }*/


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
        if (cardManager.OfferPile[i] != null)
        {
            offerCardImages[i].sprite = cardManager.OfferPile[i].image;
        }
        else
        {
            offerCardImages[i].gameObject.SetActive(false);
        }
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

        startCityTexts[i].text = card.startCity;
        endCityTexts[i].text = card.endCity;
        pointsTexts[i].text = card.points.ToString();
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

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void OnDestinationCardChoiceConfirmationPromptYes()
    {
    foreach (DestinationCard card in chosenDestinationCards)
    {
        players[currentPlayerIndex].AddDestinationCard(card);
    }
    
    foreach (DestinationCard card in offeredDestinationCards)
    {
        if (card != null)
        {
            cardManager.destinationCardDeck.PutCardBack(card);
        }
    }
    
    offeredDestinationCards.Clear();
    chosenDestinationCards.Clear();
    
    cardManager.destinationCardDeck.Shuffle();

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

            startCityTexts[i].text = offeredDestinationCards[i].startCity;
            endCityTexts[i].text = offeredDestinationCards[i].endCity;
            pointsTexts[i].text = offeredDestinationCards[i].points.ToString();
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

    Player current = players[currentPlayerIndex];

    // Get the list of possible routes the player can claim
    List<RouteBase> possibleRoutes = GetPossibleRoutesForPlayer(current);

    // Populate the dropdown with the possible routes
    foreach (RouteBase route in possibleRoutes)
    {
        dropDown.options.Add(new TMP_Dropdown.OptionData() { text = route.getRouteName() + " ----- " + route.getColour() });
    }

    data.Clear();

    // Populate the data list with possible routes
    foreach (RouteBase route in board.Routes.GeneralList)
    {
        if (CanPlayerClaimRoute(current, route))
        {
            data.Add(route);
            dropDown.options.Add(new TMP_Dropdown.OptionData() { text = route.getRouteName() + " ----- " + route.getColour() });
        }
    }
    }


    private List<RouteBase> GetPossibleRoutesForPlayer(Player player)
{
    List<RouteBase> possibleRoutes = new List<RouteBase>();

    // Loop through all the routes on the board
    for (int i = 0; i < board.Routes.GeneralList.Count; i++)
    {
        RouteBase route = board.Routes.GeneralList[i];

        // Check if the player has enough train cars to claim the route
    if (player.TrainCarsLeft >= route.getNodes())
    {
    // Check if the player has the necessary train cards to claim the route
    int colorCardCount = 0;
    int locomotiveCount = 0;
    foreach (TrainCard card in player.TrainCards)
    {
        if (card.cardColorString == route.getColour())
        {
            colorCardCount++;
        }
        else if (card.IsLocomotive)
        {
            locomotiveCount++;
        }
    }

    // Check if the player has enough cards of the required color or locomotives to claim the route
    if (colorCardCount >= route.getNodes() || (colorCardCount + locomotiveCount) >= route.getNodes())
    {
        // If the player can claim the route, add it to the list of possible routes
        possibleRoutes.Add(route);
    }
    }

    }

    return possibleRoutes;
}

    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  
    public int getLongestRoute(Player player)
    {

        Player highestPlayer;
        int highestVal = 0;

        foreach (Player p in players)
        {
            int routeCheck = getLongestRoute(player);
            if (highestVal < routeCheck)
            {
                highestVal = routeCheck;
                highestPlayer = player;
                return highestVal;
            }
        }

        return 0;
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

   /* public bool checkEndGame()
    {
        Player current = players[currentPlayerIndex];
        if (current.TrainCarsLeft < 2)
        {
            NextTurn();
            return true;
        }

        return false;

    }*/
  
   /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    public void StartGame() 
    {
        board = new Board();
        cardManager.destinationCardDeck.Shuffle();
    cardManager.trainCardDeck.Shuffle();

    foreach (Player player in players)
    {
        
        for (int i = 0; i < 4; i++)
        {
            player.AddTrainCard(cardManager.trainCardDeck.DrawCard());
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

    public void HideTrainDeckPanelAndDisableButton()
    {
    emptyTrainDeckPanel.SetActive(false);

    drawTrainButton.interactable = false;
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void HideDestinationDeckPanelAndDisableButton()
    {
    emptyDestinationDeckPanel.SetActive(false);

    drawDestinationButton.interactable = false;

    actionPanel.SetActive(true);
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void UpdateDeckCountText()
    {
    deckCountText.text = "" + cardManager.trainCardDeck.Count;
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void UpdateDeckButton() 
    {
    drawTrainButton.interactable = !cardManager.trainCardDeck.IsEmpty();

    bool shouldDrawButtonBeInteractable = cardManager.trainCardDeck.Count > 0 ||cardManager.OfferPile.Any(card => card != null);
    trainDrawTurnButton.interactable = shouldDrawButtonBeInteractable;

    bool shouldEndButtonBeActive = players[currentPlayerIndex].HasDrawnFirstCard && !shouldDrawButtonBeInteractable;
    endTurnButton.gameObject.SetActive(shouldEndButtonBeActive);
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private bool CanPlayerClaimRoute(Player player, RouteBase route)
    {
    // Check if the player has enough train cars to claim the route
    if (player.TrainCarsLeft < route.getNodes())
    {
        return false;
    }

    // Check if the player has the necessary train cards to claim the route
    int colorCardCount = 0;
    int locomotiveCount = 0;
    foreach (TrainCard card in player.TrainCards)
    {
        if (card.cardColorString == route.getColour())
        {
            colorCardCount++;
        }
        else if (card.IsLocomotive)
        {
            locomotiveCount++;
        }
    }

    // Check if the player has enough cards of the required color or locomotives to claim the route
    if (colorCardCount >= route.getNodes() || (colorCardCount + locomotiveCount) >= route.getNodes())
    {
        return true;
    }

    return false;
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

private void UpdateClaimButtonStatus(Player player, List<RouteBase> allRoutes)
{
    bool canClaimAnyRoute = false;
    
    foreach (RouteBase route in allRoutes)
    {
        if (CanPlayerClaimRoute(player, route))
        {
            canClaimAnyRoute = true;
            break;  // No need to check further, we found at least one route the player can claim
        }
    }
    
    // Assuming claimButton is your button component
    claimRouteButton.interactable = canClaimAnyRoute;
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


private bool AreThereClaimableRoutes(Player player)
    {
        foreach (var route in data)
        {
            if (CanPlayerClaimRoute(player, route))
            {
                return true;
            }
        }

        return false;
    }


}

