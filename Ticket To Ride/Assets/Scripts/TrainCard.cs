using UnityEngine;

[CreateAssetMenu(fileName = "New Train Card", menuName = "Cards/TrainCard")]
public class TrainCard : ScriptableObject
{
    public string cardType;
    public Color cardColor;
    public string cardColorString;
    public int count;
    public Sprite image;

    public bool IsLocomotive
    {
        get
        {
            return cardType == "Locomotive";
        }
    }
}