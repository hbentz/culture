using System;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // Prevents additional instances of GameMasterMain
    public static GameMaster Instance;

    private void Awake()
    {
        // Sets the instance of GameMasterMain to this one
        Instance = this;
    }
}
