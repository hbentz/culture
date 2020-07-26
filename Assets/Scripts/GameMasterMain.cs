using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class GameMasterMain : MonoBehaviour
{
    public int PlayerIDTurn = 1;
    public Text HoverDebug;
    public Plane DragPlane = new Plane(Vector3.down, 5.0f);

    // Not sure if these should be public?
    public GameObject HoverItem;
    public bool IsDragging = false;
    public GameObject DragObject;
    public GameObject SnapObject;

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
        if (Physics.Raycast (cursorDirection, out RaycastHit cursorTargetHit, 1000))
        {
            // I intend for all collider components to be attached to the visual mesh
            // so ObjectClimber isn't strictly necessary here, it is safer
            GameObject cursorTarget = ObjectClimber(cursorTargetHit.transform.gameObject);

            // Update the UI with info about the hovered element
            DebugOverlayText += "\n" + " Cursor is over: " + cursorTarget.name;

            // If the player has clicked on a card
            // Primary click 0, context click 1, middle click 2
            if (Input.GetMouseButtonDown(0) & cursorTarget.GetComponent<AdvancedProperties>().HasUITag("Dragable"))
            {
                // Select that object and set it to drag
                // DragObject being a reference to cursorTarget does not currently matter
                // because for as long as the player is dragging the card, the raycast will hit it
                DragObject = cursorTarget;
                
                // Hold it under this game object unitl it can go back to it's own home
                DragObject.transform.parent = this.transform;

                IsDragging = true;
                Debug.Log("Set " + DragObject.name + " to drag mode.");
            }
        }

        // While a drag is active
        if (IsDragging)
        {
            // Move the DragObject on the DragPlane by figuring out where the ray from the mouse towards the scene 
            if (DragPlane.Raycast(cursorDirection, out float DragSnapDist))
            {
                DragObject.transform.position = cursorDirection.GetPoint(DragSnapDist);
            }

            // Make a ray from the Origin of the DragObject pointing away from the camera
            // (DragObject.transform.position - Camera.main.transform.position) should be the same a cursor Direction for this purpose?
            Ray _dragObjHeading = new Ray(DragObject.transform.position, DragObject.transform.position - Camera.main.transform.position);

            // See what the ray is coliding with 
            // TODO: On the first frame of card pickup. dragObjectHit is the visuals for the card.
            if (Physics.Raycast(_dragObjHeading, out RaycastHit dragObjectHit, 1000))
            {
                // The intent is that all colliders are visual only so to access the actual object it's required to climb up a level
                SnapObject = ObjectClimber(dragObjectHit.transform.gameObject);
                Debug.Log("Hit" + SnapObject.transform.name);

                // Write that out into the UI
                DebugOverlayText += "\n" + "Card is over: " + SnapObject.name;

                // Check if that's a valid nest
                if (DragObject.GetComponent<AdvancedProperties>().TryHostObject(SnapObject, Vector3.zero, true))
                {
                    DebugOverlayText += "\n" + "These objects can be nested together.";
                }
                else
                {
                    DebugOverlayText += "\n" + "These objects CANNOT be nested together.";
                }
            }

            // Primary mouse is 0, context click is 1, middle click is 2
            // If there is a mouseup during a drag
            if (Input.GetMouseButtonUp(0))
            {
                // Stop the drag
                IsDragging = false;
                Debug.Log("Released " + DragObject.name + " to drag mode.");

                // Check the nesting is valid nest them together and snap into place
                if (DragObject.GetComponent<AdvancedProperties>().TryHostObject(SnapObject, Vector3.zero) & (dragObjectHit.collider != null))
                {
                    Debug.Log("Hosted " + DragObject.name + " inside " + SnapObject.name);
                }
                else
                {
                    Debug.Log("Failed to host " + DragObject.name + " inside " + SnapObject.name + " due to attach error.");
                    //TODO: Snap back to object back to where it was
                }
        }
        }

        HoverDebug.text = DebugOverlayText.Trim('\n', ' ');
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
