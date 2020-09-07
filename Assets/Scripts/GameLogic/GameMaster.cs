using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // Make GameMaster a singleton-like class (cannot use constructors as a monobehaivor, startup logic should be in Awake()
    private static GameMaster instance = null;
    public static GameMaster Instance { get { return instance; } }

    [Header("Game Settings")]
    public static int NumPlayers;

    [Header("Refernce Objects")]
    public SharedBoard CommonBoard;
    public GameObject PlayerPrefab;

    // Turn change events in the order they will be fired
    public delegate void RoundStart();
    public static event RoundStart OnRoundStarted;

    public delegate void PhaseStart();
    public static event PhaseStart OnPhaseStarted;

    public delegate void TurnStart();
    public static event TurnStart OnTurnStarted;

    public delegate void TurnEnd();
    public static event TurnEnd OnTurnEnded;

    public delegate void PhaseEnd();
    public static event PhaseEnd OnPhaseEnded;

    public delegate void RoundEnd();
    public static event RoundEnd OnRoundEnded;

    private void Awake()
    {
        // Sets the instance of GameMasterMain to this one on game start
        instance = this;

        // Spawn in the specified number of players
        LinkedList<Player> _playerList = new LinkedList<Player>();
        for (int i = 0; i < NumPlayers; i++)
        {
            // Make a new player prefab with location and rotation as defined earlier
            GameObject _newPlayerObj = Instantiate(PlayerPrefab, PlayerSpawnInfo.SpawnLocations[NumPlayers][i], PlayerSpawnInfo.SpawnRoatations[NumPlayers][i]);
            
            // Set the player name and ID upon spawn and then add it to the list
            Player _newPlayer = _newPlayerObj.GetComponent<Player>();
            _newPlayer.name = $"Player {i + 1}";
            _newPlayer.PlayerID = i + 1;
            _playerList.AddLast(_newPlayer);
        }

        // Initialize the turn info
        TurnInfo.Instance.PlayerOrder = _playerList;
    }

    // Once everything has loaded
    private void Start()
    {
        // Fire off the Round Phase and turn start events
        OnRoundStarted?.Invoke();
        OnPhaseStarted?.Invoke();
        OnTurnStarted?.Invoke();
    }

    public void EndTurn()
    {
        // When the turn is over
        OnTurnEnded?.Invoke();
    }
}
