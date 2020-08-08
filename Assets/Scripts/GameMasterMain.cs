﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using System.Linq;

public class GameMasterMain : MonoBehaviour
{
    // Prevents additional instances of GameMasterMain
    public static GameMasterMain Instance;
    
    public int PlayerIDTurn = 1;
    public Text HoverDebug;
    public Plane DragPlane = new Plane(Vector3.down, 5.0f);

    public string MouseOverName = "";

    // Not sure if these should be public?
    public GameObject HoverItem;
    public bool IsDragging = false;
    public GameObject DragObject;
    public GameObject SnapToObject;
    public Ray CursorRay;

    // Awake runs before start
    private void Awake()
    {
        Instance = this;
    }

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
        CursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // While a drag is active
        if (IsDragging)
        {
            // Make a ray from the Origin of the DragObject pointing away from the camera
            // (DragObject.transform.position - Camera.main.transform.position) should be the same a cursorRay.direction for this purpose?
            Ray _dragObjHeading = new Ray(DragObject.transform.position, CursorRay.direction);

            // See what the ray is coliding with 
            // TODO: On the first frame of card pickup. dragObjectHit htis the visuals for the card.
            if (Physics.Raycast(_dragObjHeading, out RaycastHit dragObjectHit, 1000))
            {
                // The intent is that all colliders are visual only so to access the actual object it's required to climb up a level
                SnapToObject = ObjectClimber(dragObjectHit.transform.gameObject);
                Debug.Log("Hit" + SnapToObject.transform.name);

                // Write that out into the UI
                DebugOverlayText += "\n" + "Card is over: " + SnapToObject.name;

                // Check if that's a valid nest
                if (HostChildOnParent(DragObject, SnapToObject, Vector3.zero, true))
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

                // If there is a target
                if (dragObjectHit.collider != null)
                {
                    // Check the nesting is valid nest them together and snap into place
                    if (HostChildOnParent(DragObject, SnapToObject, Vector3.zero))
                    {
                        Debug.Log("Hosted " + DragObject.name + " inside " + SnapToObject.name);
                    }
                    else
                    {
                        Debug.Log("Failed to host " + DragObject.name + " inside " + SnapToObject.name + " due to attach error.");
                        DragObject.transform.localPosition = Vector3.zero;  // TODO: Exception for returning to original hand
                    }
                }
                else
                {
                    Debug.Log("Can't host " + DragObject.name + " inside null");
                    DragObject.transform.localPosition = Vector3.zero;  // TODO: Exception for returning to original hand
                }
            }
        }

        DebugOverlayText += MouseOverName;
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Child"></param>
    /// <param name="Parent"></param>
    /// <param name="Offset"></param>
    /// <param name="IsTest"></param>
    /// <returns>false if objects cannot be hosted together, or have failed </returns>
    public bool HostChildOnParent(GameObject Child, GameObject Parent, Vector3 Offset, bool IsTest = false)
    {
        // TODO: NEED TO UNHOST CHILD FROM ORIGINAL PARENT
        AdvancedProperties _childProperties = Child.GetComponent<AdvancedProperties>();
        AdvancedProperties _parentProperties = Parent.GetComponent<AdvancedProperties>();
        IEnumerable<string> _sharedTags = _childProperties.GetResrouceTypeTags().Intersect(_parentProperties.GetHostableResources());

        // If there aren't any common entries between the ResourceTypeTags of the Child and this one 
        if (!_sharedTags.Any())
        {
            Debug.Log("Can't host due to no shared features");
            return false;
        }
        else
        {
            // Otherwise iterate through the shared tags and see if somewhere exceeds
            foreach (string tag in _sharedTags)
            {
                if (_parentProperties.HousingStatus[tag] >= _parentProperties.HousingMaxes[tag])
                {
                    Debug.Log("Can't host because it would exceed max housing for " + tag);
                    return false;
                }
            }

            // The child object must now share at least one tag and will not overflow any housing capacities
            if (!IsTest)
            {
                // Unhost the object from the previous parent
                if (Child.transform.parent.TryGetComponent<AdvancedProperties>(out AdvancedProperties _oldParentProperties))
                {
                    // TODO: Custom Unhost Functions
                    IEnumerable<string> _oldSharedTags = _childProperties.GetResrouceTypeTags().Intersect(_oldParentProperties.GetHostableResources());

                    // Decrement the old housing statuses
                    foreach (string tag in _oldSharedTags)
                    {
                        _oldParentProperties.HousingStatus[tag]--;
                    }
                    Debug.Log("Unhost from " + Child.transform.parent.name + "successful");
                }

                // Increment the housing status
                foreach (string tag in _sharedTags)
                {
                    _parentProperties.HousingStatus[tag]++;
                }

                // Attach the object and snap it to the offset
                Child.transform.parent = Parent.transform;
                Child.transform.localPosition = Offset;
                Child.GetComponent<AudioSource>().Play();
                Debug.Log("Sucessfully Hosted");
            }

            // Give the all clear
            return true;
        }
    }
    
    public void GenericHover(GameObject _eventGameObject)
    {
        MouseOverName = "\n" + " Cursor last over: " + _eventGameObject.name;
        // TODO: Add some more detailed info
    }

    public void GenericUnHover(GameObject _eventGameObject)
    {
        // TODO: remove detailed info
    }

    public void GenericPickup(GameObject _eventGameObject)
    {
        // TODO: Put onclick logic from above
    }

    /// <summary>
    /// Intented to be the default drag option for components using the EventSysyem
    /// Simply snaps the object to the cursor position if it's dragable
    /// </summary>
    /// <param name="EventDragObject">GameObject that is being dragged</param>
    public void GenericDrag(GameObject _eventGameObject)
    {
        // Only drag the object if it has the tag that allows it
        if (_eventGameObject.GetComponent<AdvancedProperties>().HasPropertyTag("Dragable"))
        {
            // Move the DragObject on the DragPlane by figuring out where the ray from the mouse towards the scene 
            if (DragPlane.Raycast(CursorRay, out float DragSnapDist))
            {
                _eventGameObject.transform.position = CursorRay.GetPoint(DragSnapDist);
            }
        }
    }

    public void GenericRelease(GameObject _eventGameObject)
    {
        IsDragging = false;
        // TODO: Release logic from above
    }
}
