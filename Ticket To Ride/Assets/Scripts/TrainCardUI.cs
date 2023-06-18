using UnityEngine;
using UnityEngine.UI;

public class TrainCardUI : MonoBehaviour
{
    public Button cardButton;
    public Image cardImage;
    public Text cardTypeText;

    private TrainCard cardData;

    public void SetCardData(TrainCard cardData)
    {
        this.cardData = cardData;
        cardImage.color = cardData.cardColor;
        cardTypeText.text = cardData.cardType;
    }

    private void Awake()
    {
        cardButton.onClick.AddListener(OnCardSelected);
    }

    private void OnCardSelected()
    {
        // Handle card selection logic here
    }
}
