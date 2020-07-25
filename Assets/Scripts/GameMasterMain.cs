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
    public bool IsDragging = false;
    public GameObject DragObject;
    public GameObject SnapObject;
    public RaycastHit cursorTargetHit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Keep all updates here for readability
    void Update()
    {
        // Figure out what the player it pointing at:
        Ray cursorDirection = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Updates cursorTarget at the same time as checking for a collision - how handy!
        if (Physics.Raycast (cursorDirection, out cursorTargetHit, 1000))
        {
            // I intend for all collider components to be attached to the visual mesh
            // so ObjectClimber isn't strictly necessary here, it is safer
            GameObject cusorTarget = ObjectClimber(cursorTargetHit.transform.gameObject);

            // Update the UI with info about the hovered element
            HoverDebug.text = cusorTarget.name;
        }

        // Primary click 0, context click 1, middle click 2
        if (Input.GetMouseButtonDown(0))
        {
            
        }
        else if (Input.GetMouseButtonUp(0))
        {

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
    }

    void SnapTo(GameObject Source, GameObject Target, float ZOffset = 0.3f)
    {

    }

    GameObject ObjectClimber(GameObject Child)
    {
        // Grabs the parent object if the object is a Visual
        if (Child.name == "Visuals")
        {
            Child = Child.transform.parent.gameObject;
        }
        return Child;
    }
}
