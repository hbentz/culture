using System;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory : MonoBehaviour
{
    public GameObject GenericCardPrefab;

    private static CardFactory instance = null;
    public static CardFactory Instance { get { return instance; } }

    // Objects that 
    public Dictionary<CardType, Dictionary<CardTrigger, Action>> PrefixEffects = new Dictionary<CardType, Dictionary<CardTrigger, Action>>();
    public Dictionary<CardType, Dictionary<CardTrigger, Action>> SuffixEffects = new Dictionary<CardType, Dictionary<CardTrigger, Action>>();

    public Card MakeCard(CardType _type, CardReference.CardName _name)
    {
        // Make a new prefab instance
        GameObject _newCardObject = Instantiate(GenericCardPrefab);

        // Give it a card and get the reference to it
        Card _newCard = _newCardObject.AddComponent(CardReference.CardLookup[_name]) as Card;
        GameMaster.OnCardPlayed += _newCard.DoWhenPlayed;
        return _newCard;
    }

    private void Awake()
    {
        instance = this;
    }
}
