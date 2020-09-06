using System.Collections.Generic;
using UnityEngine;

public class CardSlot : Deck
{
    private void Awake()
    {
        MaxDeckSize = 1;
        TopNCardsVisible = -1;
    }
    
}
