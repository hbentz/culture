using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBoardBehaviours : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        // Whenever a turn starts set the board to face the player
        GameMasterMain.OnTurnStart += RotateBoardtoActivePlayer;
    }
    void Start()
    {
    }

    private void OnDisable()
    {
        // Remove SetBoardFacePlayer as a listener to the OnTurnStart method to avoid errors
        GameMasterMain.OnTurnStart -= RotateBoardtoActivePlayer;
    }

    // Update is called once per frame
    void Update()
    {   
    }

    public void RotateBoardtoActivePlayer(GameObject _activePlayer)
    {
        // Gets the active player's rotation and sets the baord to face them
        this.transform.rotation = _activePlayer.transform.rotation;
    }
}
