using System;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // Make GameMaster a singleton-like class (cannot use constructors as a monobehaivor, startup logic should be in Awake()
    private static GameMaster instance = null;
    public static GameMaster Instance { get { return instance; } }

    private void Awake()
    {
        // Sets the instance of GameMasterMain to this one on game start
        instance = this;
    }
}
