using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandUIController : MonoBehaviour
{
    public GameObject trainCardPrefab;
    public GameObject destinationCardPrefab;
    public Transform trainCardContainer;
    public Transform destinationCardContainer;

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void UpdatePlayerHandUI(Player player)
    {
        CleanUpPlayerHandUI();
        
        foreach (TrainCard card in player.TrainCards)
        {
            GameObject cardObject = Instantiate(trainCardPrefab, trainCardContainer);
            TrainCardPrefabController cardController = cardObject.GetComponent<TrainCardPrefabController>();
            cardController.SetTrainCard(card);
        }

        foreach (DestinationCard card in player.DestinationCards)
        {
            GameObject cardObject = Instantiate(destinationCardPrefab, destinationCardContainer);
            DestinationCardPrefabController cardController = cardObject.GetComponent<DestinationCardPrefabController>();
            cardController.SetDestinationCard(card);
        }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void CleanUpPlayerHandUI()
    {
        foreach (Transform child in trainCardContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in destinationCardContainer)
        {
            Destroy(child.gameObject);
        }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
