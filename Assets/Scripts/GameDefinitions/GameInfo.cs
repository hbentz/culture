using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    // Turn Logic Holders
    public int RoundCounter = 1; // Display 
    public int TurnCounter = 0; // Who in the turn order
    public int PhaseCounter = 0; // Index of Phase.PhaseOrder
    public List<GameObject> PlayerList = new List<GameObject>();  // List of active players
    public List<int> PlayerTurnOrder = new List<int>();  // ints represent player ID

    // Phase order
    public static List<string> PhaseOrder = new List<string>()
    {
    "Setup",
    "ChallengeSelection",
    "Bidding",
    "Drafting",
    "Development",
    "ResourceSpend",
    "ResourceGain",
    "ChallengeResolution",
    "Cleanup",
    };

    public Dictionary<int, List<Vector3>> SpawnLocations = new Dictionary<int, List<Vector3>>()
    {
        { 1, new List<Vector3>()
            {Vector3.zero, // TODO Single Player MainBoard Location
             Vector3.zero} // TODO Single Player1 Position
        },
        { 2, new List<Vector3>()
            {Vector3.zero, // TODO TwoPlayer Player MainBoard Location
             Vector3.zero, // TODO TwoPlayer Player1 Position
             Vector3.zero} // TODO TwoPlayer Player2 Position
        },
        { 3, new List<Vector3>()
            {Vector3.zero, // TODO ThreePlayer Player MainBoard Location
             Vector3.zero, // TODO ThreePlayer Player1 Position
             Vector3.zero} // TODO ThreePlayer Player3 Position
        },
        { 4, new List<Vector3>()
            {Vector3.zero, // TODO FourPlayer Player MainBoard Location
             Vector3.zero, // TODO FourPlayer Player1 Position
             Vector3.zero, // TODO FourPlayer Player2 Position
             Vector3.zero, // TODO FourPlayer Player3 Position
             Vector3.zero} // TODO FourPlayer Player4 Position
        },
        { 5, new List<Vector3>()
            {Vector3.zero, // TODO FivePlayer Player MainBoard Location
             Vector3.zero, // TODO FivePlayer Player1 Position
             Vector3.zero, // TODO FivePlayer Player2 Position
             Vector3.zero, // TODO FivePlayer Player3 Position
             Vector3.zero, // TODO FivePlayer Player4 Position
             Vector3.zero} // TODO FivePlayer Player5 Position
        },
        { 6, new List<Vector3>()
            {Vector3.zero, // TODO SixPlayer Player MainBoard Location
             Vector3.zero, // TODO SixPlayer Player1 Position
             Vector3.zero, // TODO SixPlayer Player2 Position
             Vector3.zero, // TODO SixPlayer Player3 Position
             Vector3.zero, // TODO SixPlayer Player4 Position
             Vector3.zero, // TODO SixPlayer Player5 Position
             Vector3.zero} // TODO SixPlayer Player6 Position
        },
    };
}