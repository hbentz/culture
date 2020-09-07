using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DraftBoard : GameBoard
{
    public CardType EnumCardType;
    public List<GameObject> CardSlots;

    public GameObject BusinessSlotPrefab;
    public GameObject ChallengeSlotPrefab;
    public GameObject ProjectSlotPrefab;

    public void SetDraftMode(CardType cardType, int nSlots)
    {
        EnumCardType = cardType;
        GameObject _prefab;

        // Set the reference to the correct prefab
        _prefab = (cardType == CardType.Business) ? BusinessSlotPrefab :
            (cardType == CardType.Challenge) ? ChallengeSlotPrefab : ProjectSlotPrefab;

        CardSlots = MakeNPrefabs(_prefab, nSlots);
        RearrangeSlots();
    }

    public void RearrangeSlots()
    {
        // Get the current size of this object
        Vector3 _selfSize = VisualComponent.GetComponent<Renderer>().bounds.size;

        // Figure out the center-center distance of each Cardslot
        float _slotSpacing = _selfSize.z / (CardSlots.Count + 1);

        // Set the place for the card slots to start at the lowest point in the card
        float _currentSlotZ= -_selfSize.z;

        foreach(GameObject CardSlotObject in CardSlots)
        {
            // Set the next spacing, if not set on the first loop the center of the bottom card will be on the edge
            _currentSlotZ += _slotSpacing;
            CardSlotObject.transform.localPosition = new Vector3(0, 0, _currentSlotZ);
        }

    }
}
