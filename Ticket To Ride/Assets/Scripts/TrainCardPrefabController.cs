using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainCardPrefabController : MonoBehaviour
{
    public Image cardImage;
    public TextMeshProUGUI cardTypeText;

    public void SetTrainCard(TrainCard card)
    {
        cardImage.sprite = card.image;
        cardTypeText.text = card.cardType;
    }
}
