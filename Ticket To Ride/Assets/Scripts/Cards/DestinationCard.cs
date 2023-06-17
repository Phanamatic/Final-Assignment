using UnityEngine;

[CreateAssetMenu(fileName = "New Destination Card", menuName = "Cards/DestinationCard")]
public class DestinationCard : ScriptableObject
{
    public string startCity;
    public string endCity;
    public int points;
    [SerializeField] public Vector2 sCity;
    [SerializeField] public Vector2 eCity;
}
