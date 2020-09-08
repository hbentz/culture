using System;
using UnityEngine;

public class CardFactory : MonoBehaviour
{
    public GameObject GenericCardPrefab;
    
    public Card MakeCard<Card>()
    {
        // Make a new prefab instance
        GameObject _newCardObject = Instantiate(GenericCardPrefab);
        Type asdf = typeof(card);   
        // Give it a card and get the reference too it
        _newCardObject.AddComponent<Card>();
        Card _newCard = _newCardObject.GetComponent<typeOfCard>();

        // Set the 
    }
}
