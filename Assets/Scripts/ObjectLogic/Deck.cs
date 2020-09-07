using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject VisualComponent;
    private LinkedList<Card> cards;
    public LinkedList<Card> Cards { get; }

    public int MaxDeckSize = -1; // Unlimited
    public int TopNCardsVisible = 0;
    public CardType HostedCardType;
}