using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBoard : MonoBehaviour
{
    public Dictionary<BoardType, List<GameBoard>> NestedBoards;
    public Dictionary<CardType, Dictionary<DeckType, List<Deck>>> CardHosts;
}

