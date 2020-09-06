using System;
using UnityEngine;

public class DiscardPile : Deck
{
    private void Awake()
    {
        MaxDeckSize = 1;
        TopNCardsVisible = -1;
    }
}
