using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameMasterMain : MonoBehaviour
{
    public int PlayerIDTurn = 1;
    public bool PlayerMouseButtonDown = false;
    public int NumberTicks = 0;
    public Text HoverDebug;

    // Not sure if these should be public?
    public GameObject HoverItem;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Keep all updates here for readability
    void Update()
    {
        // Figure out what the player it pointing at:
        Ray cursorDirection = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit cursorTarget;

        // Updates cursorTarget at the same time as checking for a collision - how handy!
        if (Physics.Raycast (cursorDirection, out cursorTarget, 1000))
        {
            
            GameObject targetObject = cursorTarget.transform.gameObject;
            // I intend for all collider components to be attached to the visual mesh
            // incase this isn't the case in the future, check to make sure it's collided
            // with a visual instead of an actual object
            if (targetObject.name == "Visuals")
            {
                targetObject = targetObject.transform.parent.gameObject;
            }
            HoverDebug.text = targetObject.name;
        }

        /* TODO 
         * On Mousedown
         * Drag card in XZ
         * On Mouseup
         * If there is a dragged object
         * Check if that object can be placed there
         * If if can, figure out where the center is over
         * and snap it to to there (over nothing goes to next)
         * Otherwise snap it back to the original position
         */
        // Primary click 0, context click 1, middle click 2
        // Input.GetMouseButtonDown(0)
    }
}
