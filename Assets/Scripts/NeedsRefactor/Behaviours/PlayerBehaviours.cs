using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviours : MonoBehaviour
{
    // This player's camera's GameObject
    public GameObject CameraHolder;

    private void OnEnable()
    {
        // Toggle the camerea based on the active player whenever there is a turn change
        GameMasterMain.OnTurnStart += UpdateCameraState;
    }

    void Start()
    {
        
    }

    private void OnDisable()
    {
        // Remove listeners to avoid errors
        GameMasterMain.OnTurnStart -= UpdateCameraState;
    }
    
    void Update()
    {
        
    }

    public void UpdateCameraState(GameObject ActivePlayer)
    {
        // If this is the active player, enable the camera, otherwise disable it
        CameraHolder.GetComponent<Camera>().enabled = ActivePlayer == this.gameObject;
        CameraHolder.GetComponent<AudioListener>().enabled = ActivePlayer == this.gameObject;
    }
}
