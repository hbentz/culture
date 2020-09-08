using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class TurnInfo
{
    // The cosntructor for GameInfo is private so only this class can create instances of itself
    private TurnInfo()
    {
        // Create the phase list
        Phase[] phases = { Phase.Start, Phase.Drafting, Phase.ResourceAllocation, Phase.ChallengeResolution, Phase.ChallengeSelection, Phase.End };
        phaseOrder = new LinkedList<Phase>(phases);
        
        // Initialize the empty playerOrder
        playerOrder = new LinkedList<Player>();

        // Set the publicly available properties
        currentPhase = phaseOrder.First;

        TurnCounter = 1;
        CurrentRound = 1;
    }

    private static TurnInfo instance = null;
    public static TurnInfo Instance
    {
        // Get the current instance of this class if there is one, otherwise create it
        get
        {
            if (instance == null) instance = new TurnInfo();
            return instance;
        }
    }

    private LinkedList<Phase> phaseOrder; // The order of the phases
    private LinkedListNode<Phase> currentPhase; // The current phase

    private LinkedList<Player> playerOrder; // The turn order of all the players
    private LinkedListNode<Player> activePlayer; // Whose turn it is

    public LinkedList<Player> PlayerOrder { get; private set; }
    public int CurrentRound { get; private set; } // How many rounds have completed since the start of the game
    public int TurnCounter { get; private set; } // How many turns have completed in this phase
    public Phase CurrentPhase { get { return currentPhase.Value;  } } // The current phase enum
    public Player ActivePlayer { get { return activePlayer.Value; } } // The Player whose turn it is

    public void UpdatePlayerTurnOrder(Player[] newTurnOrder)
    {
        // Update the player order
        playerOrder = new LinkedList<Player>(newTurnOrder);

        // If the current player order is empty, set the activePlayer to be the first player
        activePlayer = (activePlayer == null)? playerOrder.First :
            // Otherwise if the active player is still in the game, it's still their turn, otherwise it's the first player in the list
            (playerOrder.Contains(activePlayer.Value)) ? playerOrder.Find(activePlayer.Value) : playerOrder.First;
    }

    public void AdvancePlayerTurn()
    {
        activePlayer = activePlayer.Next;
        TurnCounter++;
    }

    public bool AdvancePhase()
    {
        // Returns false if the phase cycle has been reset, otherwise true
        TurnCounter = 1;

        // If this last phase
        if (currentPhase.Next == null)
        {
            currentPhase = phaseOrder.First;
            CurrentRound++;
            return false;
        }
        else
        {
            currentPhase = currentPhase.Next;
            return true;
        }
    }
}
