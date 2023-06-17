using System.Collections.Generic;
using UnityEngine;

public class CardDeck<T> where T : ScriptableObject
{
    private List<T> cards = new List<T>();

    public CardDeck(List<T> startingCards)
    {
        cards = new List<T>(startingCards);
    }

    public void AddCard(T card, int copies = 1)
    {
        for (int i = 0; i < copies; i++)
        {
            cards.Add(card);
        }
    }

    public void Shuffle()
    {
        for (int t = 0; t < cards.Count; t++ )
        {
            T tmp = cards[t];
            int r = Random.Range(t, cards.Count);
            cards[t] = cards[r];
            cards[r] = tmp;
        }
    }

    public T DrawCard()
    {
        if (cards.Count == 0)
        {
            Debug.LogError("Deck is empty. Cannot draw card.");
            return null;
        }

        T drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }

    public int Count
    {
        get { return cards.Count; }
    }
}
