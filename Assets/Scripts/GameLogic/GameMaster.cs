using System;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // Make GameMaster a singleton-like class (cannot use constructors as a monobehaivor, startup logic should be in Awake()
    private static GameMaster instance = null;
    public static GameMaster Instance { get { return instance; } }

    // GameState Stuff
    public TurnInfo TurnState;

    // Turn Events in order in which they will occur
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
    }
}
