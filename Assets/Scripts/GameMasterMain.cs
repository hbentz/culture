using System;
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
    public Plane DragPlane = new Plane(Vector3.down, 1.2f);

    // Should only be proceduarlly set
    public string LastOver = "";
    public string LastNestInfo = "";
    public string NestInfo = "";
    public bool LastNestPossible = false;

    public GameObject HoverItem;
    public GameObject DragObject;
    public GameObject LastNestObject;
    public Ray CursorRay;

    // Awake runs before start
    private void Awake()
    {
        // Creates the one and only instance of GameMasterMain
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

        DebugOverlayText += LastOver;
        DebugOverlayText += LastNestInfo;
        DebugOverlayText += NestInfo;
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
    /// Sets the LastOver overlay element and TOOO: detailed info overlay
    /// </summary>
    /// <param name="_eventGameObject">GameObject that fired OnMouseOver()</param>
    public void GenericHover(GameObject _eventGameObject)
    {
        LastOver = "\n" + " Cursor last over: " + _eventGameObject.name;
        // TODO: Add some more detailed info
    }

    /// <summary>
    /// TODO: Removes detailed info placed by GenericHover
    /// </summary>
    /// <param name="_eventGameObject">GameObject that fired OnMouseExit()</param>
    public void GenericUnHover(GameObject _eventGameObject)
    {
        LastOver = "";
        // TODO: remove detailed info
    }

    /// <summary>
    /// Sets DragObject to _eventGameObject and sets it into Dragging mode if true
    /// </summary>
    /// <param name="_eventGameObject">GameObject that fired OnMouseDown()</param>
    public void GenericPickup(GameObject _eventGameObject)
    {
        // If the item can be dragged
        if (_eventGameObject.GetComponent<GameProperties>().IsDragable)
        {
            // Set it into dragging mode and give GameMasterMain access to it
            _eventGameObject.GetComponent<GameProperties>().IsDragging = true;
            DragObject = _eventGameObject;
            DragObject.GetComponent<AudioSource>().PlayOneShot(DragObject.GetComponent<GameProperties>().PickUpSound);
            // TODO: trigger OnPickup() from _eventGameObject
        }
    }

    /// <summary>
    /// Hosts _eventGameObject on object it's hovering over if possible
    /// </summary>
    /// <param name="_eventGameObject">GameObject that fired OnMouseUpAsButton()</param>
    public void GenericRelease(GameObject _eventGameObject)
    {
        GameProperties _objProps = _eventGameObject.GetComponent<GameProperties>();
        if (_objProps.IsDragging)
        {
            _objProps.IsDragging = false;

            NestInfo += "\n";
            NestInfo += !LastNestPossible ?
                "Cannot Host " : // If the last nest wasn't possible
                LastNestObject.GetComponent<GameProperties>().Host(_eventGameObject) ? // If it was try to host
                    "Hosted " : "Failed to Host "; // If/else the host was sucessful
            NestInfo += DragObject.name + " On " + LastNestObject.name;
            
            // Rearrange the object and its siblings on it's parent
            _eventGameObject.transform.parent.gameObject.GetComponent<GameProperties>().RearrangeChildren();
            DragObject.GetComponent<AudioSource>().PlayOneShot(DragObject.GetComponent<GameProperties>().PlaceSound);
        }
    }

    /// <summary>
    /// Snaps the _evenGameObject to cursor position on DragPlane if possible
    /// </summary>
    /// <param name="_eventGameObject">GameObject that fired OnMouseDrag()</param>
    public void GenericDrag(GameObject _eventGameObject)
    {
        // Only drag the object if it has the tag that allows it
        if (_eventGameObject.GetComponent<GameProperties>().IsDragging)
        {
            // Move the _eventGameObject on the DragPlane by figuring out where the ray from the mouse towards the scene 
            if (DragPlane.Raycast(CursorRay, out float DragSnapDist))
            {
                _eventGameObject.transform.position = CursorRay.GetPoint(DragSnapDist);
            }
            
            // Make a ray from the Origin of the DragObject pointing away from the camera
            // This will collide with whatever DragObject appeart to be over
            Ray _dragObjHeading = new Ray(DragObject.transform.position, CursorRay.direction);
            if (Physics.Raycast(_dragObjHeading, out RaycastHit dragObjectHit, 1000))
            {
                // all colliders are visual only and the parent object is desired
                LastNestObject = ObjectClimber(dragObjectHit.transform.gameObject);
                // TODO: Trigger an OnHoverObjectOver(GameObject) custom event from LastNestObject

                LastNestInfo = "\n" + "Card is over: " + LastNestObject.name;
                // LastNestPossible = true if LastNestObject has Gameproperties and it can host DragObject
                LastNestPossible = LastNestObject.TryGetComponent<GameProperties>(out GameProperties _objProp) && _objProp.CanHost(DragObject);
                LastNestInfo += "\n" + "These objects ";
                LastNestInfo += LastNestPossible ? "CAN" : "CANNOT";
                LastNestInfo += " be nested together.";

                // Preview the new location of the objects
                LastNestObject.GetComponent<GameProperties>().RearrangeChildren(DragObject);
            }
        }
    }
}
