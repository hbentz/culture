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
    public RaycastHit dragObjectHit;

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
            DebugOverlayText += "\n Cursor is over: " + cursorTarget.name;

            // If the player has clicked on a card
            // Primary click 0, context click 1, middle click 2
            if (Input.GetMouseButtonDown(0) & cursorTargetHit.collider.tag == "Card")
            {
                // Select that object and set it to drag
                // DragObject being a reference to cursorTarget does not currently matter
                // because for as long as the player is dragging the card, the raycast will hit it
                DragObject = cursorTarget;
                IsDragging = true;
                
                // TODO physically move the card towards the camera and scale it
            }
        }

        // While a drag is active
        if (IsDragging)
        {
            // TODO: Actually drag the object

            // Make a ray from the Origin of the DragObject pointing away from the camera
            Ray _dragObjHeading = new Ray(DragObject.transform.position, DragObject.transform.position - Camera.main.transform.position);
            bool _validNest = false;  // Holder to see if dragged objects will be nested

            // See what the ray is coliding with 
            if (Physics.Raycast(_dragObjHeading, out dragObjectHit, 1000))
            {
                // The intent is that all colliders are visual only so to access the actual object it's required to climb up a level
                SnapObject = ObjectClimber(dragObjectHit.transform.gameObject);
                
                // Write that out into the UI
                DebugOverlayText += "\n Cursor is over: " + SnapObject.name;

                // Check if that's a valid nest
                _validNest = VaidNest(DragObject, SnapObject);
                if (_validNest)
                {
                    DebugOverlayText += "\n These objects can be nested together.";
                }
                else
                {
                    DebugOverlayText += "\n These objects CANNOT be nested together.";
                }
            }
            
            // Primary mouse is 0, context click is 1, middle click is 2
            // If there is a mouseup during a drag
            if (Input.GetMouseButtonUp(0))
            {
                // Stop the drag
                IsDragging = false;
                
                // If the nesting is valid nest them together and snap into place
                if (_validNest)
                {
                    DragObject.transform.parent = SnapObject.transform;
                    DragObject.transform.localPosition = Vector3.zero;
                }
                else
                {
                    // Snap back to the card tray
                }
            }
        }

        HoverDebug.text = DebugOverlayText;
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
