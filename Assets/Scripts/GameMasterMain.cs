using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.UIElements;

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
        // Reset the overlay text
        string DebugOverlayText = "";

        // Figure out what the player it pointing at:
        Ray cursorDirection = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Updates cursorTarget at the same time as checking for a collision - how handy!
        if (Physics.Raycast (cursorDirection, out cursorTargetHit, 1000))
        {
            // I intend for all collider components to be attached to the visual mesh
            // so ObjectClimber isn't strictly necessary here, it is safer
            GameObject cursorTarget = ObjectClimber(cursorTargetHit.transform.gameObject);

            // Update the UI with info about the hovered element
            DebugOverlayText = "" + cursorTarget.name + "\n";

            // If the player has clicked on a card
            // Primary click 0, context click 1, middle click 2
            if (Input.GetMouseButtonDown(0) & cursorTargetHit.collider.tag == "Card")
            {
                // Select that object and set it to drag
                // DragObject being a reference to cursorTarget does not currently matter
                // because for as long as the player is dragging the card, the raycast will hit it
                DragObject = cursorTarget;
                IsDragging = true;
                
                //TODO physically move the card towards the camera and scale it
            }
        }

        // If the player is click and dragging something
        if (IsDragging)
        {
            Ray DraggingRay = Camera.main.ScreenPointToRay(new Vector3(DragObject.transform.position,,));
            GameObject _cardTarget;

            /* TODO: 
             * Drag object
             * Check for where the card will snap to (_cardTarget)
             * Update the UI with that info
             * 
             */
            if (Input.GetMouseButtonUp(0))
            {
                SnapTo(DragObject.transform, _cardTarget.transform.position, new Vector3(0.0f, 0.3f, 0.0f));
                IsDragging = false;
            }
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
        HoverDebug.text = DebugOverlayText;
        }

    void SnapTo(Transform Source, Vector3 Target, Vector3 Offset)
    {
        // Snaps Source to Target with an offset of Offset
        Source.position = new Vector3(Target.x + Offset.x, Target.y + Offset.y, Target.z + Offset.z);
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

    bool VaidNest(GameObject Child, GameObject Parent)
    {
        // Checks to see if an attempted nesting procedure is valid
        // Is this the best way to do this?
        switch (Parent.tag)
        {
            // If the parent is a Cardslot
            case ("CardSlot"):
                {
                    // Check its children for cards
                    foreach (Transform childTransform in Parent.transform)
                    {
                        // If there is already one this is not a valid nes
                        if (childTransform.gameObject.tag == "Card")
                        {
                            return false;
                        }
                    }
                    // If there are no cards it IS a valid nest
                    return true;
                }
            default:
                {
                    return false;
                }
        }
    }
}
