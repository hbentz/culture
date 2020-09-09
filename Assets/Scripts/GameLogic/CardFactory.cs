using System;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory : MonoBehaviour
{
    public GameObject GenericCardPrefab;
    
    // Objects that 
    public Dictionary<CardType, Dictionary<CardTrigger, Action>> PrefixEffects = new Dictionary<CardType, Dictionary<CardTrigger, Action>>();
    public Dictionary<CardType, Dictionary<CardTrigger, Action>> SuffixEffects = new Dictionary<CardType, Dictionary<CardTrigger, Action>>();

    public Card MakeCard(CardType _type, CardReference.CardName _name)
    {
        // Make a new prefab instance
        GameObject _newCardObject = Instantiate(GenericCardPrefab);

        // Give it a card and get the reference to it
        Card _newCard = _newCardObject.AddComponent(CardReference.CardLookup[_name]) as Card;
        return _newCard;
    }
}
