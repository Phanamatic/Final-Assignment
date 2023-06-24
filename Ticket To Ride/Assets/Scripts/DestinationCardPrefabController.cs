using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DestinationCardPrefabController : MonoBehaviour
{
    public TextMeshProUGUI startCityText;
    public TextMeshProUGUI endCityText;
    public TextMeshProUGUI pointsText;
    public Image cardImage;

    public void SetDestinationCard(DestinationCard card)
    {
        startCityText.text = card.startCity;
        endCityText.text = card.endCity;
        pointsText.text = card.points.ToString();
        
        cardImage.sprite = card.image;
    }
}
