using UnityEngine;

[CreateAssetMenu(fileName = "New Train Card", menuName = "Cards/TrainCard")]
public class TrainCard : ScriptableObject
{
    public string cardType;
    public Color cardColor;
    public int count;
}
