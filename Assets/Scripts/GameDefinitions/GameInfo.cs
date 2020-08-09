using System.Collections.Generic;
using System.Dynamic;
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
            {Vector3.zero, // Single Player MainBoard Location
             new Vector3(0, 0, -22)} // Single Player1 Position
        },
        { 2, new List<Vector3>()
            {Vector3.zero, // TwoPlayer Player MainBoard Location
             new Vector3(0, 0, -22), // TwoPlayer Player1 Position
             new Vector3(0, 0, 22)} // TwoPlayer Player2 Position
        },
        { 3, new List<Vector3>()
            {Vector3.zero, // ThreePlayer Player MainBoard Location
             new Vector3(0, 0, -22), // ThreePlayer Player1 Position
             new Vector3(-22, 0, 0), // ThreePlayer Player1 Position
             new Vector3(0, 0, 22)} // ThreePlayer Player3 Position
        },
        { 4, new List<Vector3>()
            {Vector3.zero, // FourPlayer Player MainBoard Location
             new Vector3(0, 0, -22), // FourPlayer Player1 Position
             new Vector3(-22, 0, 0), // ThreePlayer Player1 Position
             new Vector3(0, 0, 22), // ThreePlayer Player3 Position
             new Vector3(22, 0, 0)} // FourPlayer Player4 Position
        },
    };

    public Dictionary<int, List<Quaternion>> SpawnRoatations = new Dictionary<int, List<Quaternion>>()
    {
        { 1, new List<Quaternion>()
            {new Quaternion(0, 0, 0, 1), // Single Player MainBoard Location
             new Quaternion(0, 0, 0, 1)} // Single Player1 Position
        },
        { 2, new List<Quaternion>()
            {new Quaternion(0, 0, 0, 1), // TwoPlayer Player MainBoard Location
             new Quaternion(0, 0, 0, 1), // TwoPlayer Player1 Position
             new Quaternion(0, 1, 0, 0)} // TwoPlayer Player2 Position
        },
        { 3, new List<Quaternion>()
            {new Quaternion(0, 0, 0, 1), // ThreePlayer Player MainBoard Location
             new Quaternion(0, 0, 0, 1), // ThreePlayer Player1 Position
             new Quaternion(0, -0.7071068f, 0, 0.7071068f), // ThreePlayer Player2 Position
             new Quaternion(0, 1, 0, 0)} // ThreePlayer Player3 Position
        },
        { 4, new List<Quaternion>()
            {new Quaternion(0, 0, 0, 1), // FourPlayer Player MainBoard Location
             new Quaternion(0, 0, 0, 1), // FourPlayer Player1 Position
             new Quaternion(0, -0.7071068f, 0, 0.7071068f), // FourPlayer Player2 Position
             new Quaternion(0, 1, 0, 0), // FourPlayer Player3 Position
             new Quaternion(0, 0.7071068f, 0, 0.7071068f)} // FourPlayer Player4 Position
        },
    };

    public GameObject GetActivePlayer()
    {
        return PlayerList[PlayerTurnOrder[TurnCounter]];
    }

    public void UpdatePlayerInitiative()
    {
        for (int i = 0; i < PlayerTurnOrder.Count; i++)
        {
            PlayerTurnOrder[i] = (PlayerTurnOrder[i] + 1) % PlayerTurnOrder.Count;

        }
    }
}