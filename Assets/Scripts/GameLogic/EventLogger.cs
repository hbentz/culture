using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLogger : MonoBehaviour
{

    public TurnInfo TurnInfo = TurnInfo.Instance;

    private void OnEnable()
    {
        GameMaster.OnRoundStarted += RoundStart;
        GameMaster.OnPhaseStarted += PhaseStart;
        GameMaster.OnTurnStarted += TurnStart;
        GameMaster.OnTurnEnded += TurnEnd;
        GameMaster.OnPhaseEnded += PhaseEnd;
        GameMaster.OnRoundEnded += RoundEnd;
    }

    private void OnDisable()
    {
        GameMaster.OnRoundStarted -= RoundStart;
        GameMaster.OnPhaseStarted -= PhaseStart;
        GameMaster.OnTurnStarted -= TurnStart;
        GameMaster.OnTurnEnded -= TurnEnd;
        GameMaster.OnPhaseEnded -= PhaseEnd;
        GameMaster.OnRoundEnded -= RoundEnd;
    }

    private void RoundStart()
    {
        Debug.Log($"OnRoundStarted event fired begining round {TurnInfo.CurrentRound}.");
    }

    private void PhaseStart()
    {
        Debug.Log($"OnPhaseStarted event fired begining phase: {TurnInfo.CurrentPhase}.");
    }

    private void TurnStart()
    {
        Debug.Log($"OnTurnStarted event fired begining the turn for {TurnInfo.ActivePlayer.PlayerName}.");
        Debug.Log($"This is turn {TurnInfo.TurnCounter} in the phase.");
    }

    private void TurnEnd()
    {
        Debug.Log($"OnTurnEnded event fired ending the turn for {TurnInfo.ActivePlayer.PlayerName}.");
    }

    private void PhaseEnd()
    {
        Debug.Log($"OnPhaseEnded event fired ending the phase: {TurnInfo.CurrentPhase}.");
    }

    private void RoundEnd()
    {
        Debug.Log($"OnRoundEnded event fired ending round {TurnInfo.CurrentRound}.");
    }
}
