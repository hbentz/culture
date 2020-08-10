using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEventLogger : MonoBehaviour
{
    // Spit out generic logs when events happen
    private void OnEnable()
    {
        GameMasterMain.OnHost += OnHostLogger;
        GameMasterMain.OnUnHost += UnHostLogger;
        GameMasterMain.OnTurnStart += TurnStartLogger;
        GameMasterMain.OnTurnEnd += TurnEndLogger;
        GameMasterMain.OnPhaseStart += PhaseStartLogger;
        GameMasterMain.OnPhaseEnd += PhaseEndLogger;
        GameMasterMain.OnRoundStart += RoundStartLogger;
        GameMasterMain.OnRoundEnd += RoundEndLogger;
        GameMasterMain.OnChallengeSuccess += ChallengeSuccessLogger;
        GameMasterMain.OnChallengeFail += ChallengeFailLogger;
    }

    // Update is called once per frame
    void OnDisable()
    {
        GameMasterMain.OnHost -= OnHostLogger;
        GameMasterMain.OnUnHost -= UnHostLogger;
        GameMasterMain.OnTurnStart -= TurnStartLogger;
        GameMasterMain.OnTurnEnd -= TurnEndLogger;
        GameMasterMain.OnPhaseStart -= PhaseStartLogger;
        GameMasterMain.OnPhaseEnd -= PhaseEndLogger;
        GameMasterMain.OnRoundStart -= RoundStartLogger;
        GameMasterMain.OnRoundEnd -= RoundEndLogger;
        GameMasterMain.OnChallengeSuccess -= ChallengeSuccessLogger;
        GameMasterMain.OnChallengeFail -= ChallengeFailLogger;
    }

    void OnHostLogger(GameObject _child, GameObject _parent)
    {
        Debug.Log("Host Event Triggered. " + _child.name + " on " + _parent.name);
    }

    void UnHostLogger(GameObject _child, GameObject _parent)
    {
        Debug.Log("UnHost Event Triggered. " + _child.name + " from " + _parent.name);
    }

    void TurnStartLogger(GameObject ActivePlayer)
    {
        Debug.Log("Turn Start Event Detected. It is now " + ActivePlayer + "'s Turn.");
    }

    void TurnEndLogger(GameObject ActivePlayer)
    {
        Debug.Log("Turn End Event Detected. " + ActivePlayer + "'s Turn is over.");
    }

    void PhaseStartLogger(int PhaseID)
    {
        Debug.Log("Phase Start Event Detected. " + TurnInfo.PhaseOrder[PhaseID] + " has begun.");
    }

    void PhaseEndLogger(int PhaseID)
    {
        Debug.Log("Phase End Event Detected. " + TurnInfo.PhaseOrder[PhaseID] + " is over.");
    }

    void RoundStartLogger(int RoundNum)
    {
        Debug.Log("Phase Start Event Detected. " + RoundNum + " has begun.");
    }

    void RoundEndLogger(int RoundNum)
    {
        Debug.Log("Phase End Event Detected. " + RoundNum + " is over.");
    }

    void ChallengeSuccessLogger(GameObject ChallengeCard)
    {
        Debug.Log("Challenge Success Event Detected. " + ChallengeCard.name + " has resolved in a success.");
    }

    void ChallengeFailLogger(GameObject ChallengeCard)
    {
        Debug.Log("Challenge Fail Event Detected. " + ChallengeCard.name + " has resolved in a failure.");
    }
}
