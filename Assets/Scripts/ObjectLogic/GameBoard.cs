using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GameBoard : MonoBehaviour
{
    public Dictionary<BoardType, List<GameBoard>> NestedBoards;
    public Dictionary<CardType, Dictionary<DeckType, List<Deck>>> CardHosts;
    public BoardType EnumBoardType;

    /// <summary>
    /// Makes nPrefabs of prefab as a child of object where it's called
    /// </summary>
    /// <param name="prefab">Which prefab to make</param>
    /// <param name="nPrefabs">How many prefabs to make</param>
    /// <returns>List of GameObjects it made</returns>
    public List<GameObject> MakeNPrefabs(GameObject prefab, int nPrefabs)
    {
        List<GameObject> returnList = new List<GameObject>();

        for (int i = 0; i < nPrefabs; i++)
        {
            GameObject _newPrefab = Instantiate(prefab);
            _newPrefab.transform.parent = this.transform;
            _newPrefab.transform.localPosition = Vector3.zero;
            _newPrefab.transform.localRotation = Quaternion.identity;
            returnList.Add(_newPrefab);
        }

        return returnList;
    }
}

