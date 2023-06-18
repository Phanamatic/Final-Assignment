using UnityEngine;

[CreateAssetMenu(fileName = "New Train Card", menuName = "Cards/TrainCard")]
public class TrainCard : ScriptableObject
{
    public string cardType;
    public Color cardColor;
    public int count;
    public Texture2D image;

    public bool IsLocomotive
    {
        get
        {
            return cardType == "Locomotive";
        }
    }
}