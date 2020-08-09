using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using System.Linq;
using UnityEditor;

public class GameMasterMain : MonoBehaviour
{
    // Prevents additional instances of GameMasterMain
    public static GameMasterMain Instance;
    
    // Game setting stuff
    public int NumPlayers = 4;
    public Text HoverDebug;

    // Required for game handling
    public GameObject Player1;
    public GameObject CommonBoard;

    // Debug Text Holders
    public string LastOver = "";
    public string LastNestInfo = "";
    public string NestInfo = "";
    public bool LastNestPossible = false;

    // Unity Specific Holders
    public Plane DragPlane = new Plane(Vector3.down, 1.2f);
    public GameObject DragObject;
    public GameObject LastNestObject;
    public Ray CursorRay;

    // Holds stuff like Round Counter and the like
    public GameInfo GameInfo;

    // Event Definitions
    public delegate void HostAction(GameObject _child, GameObject _parent);
    public static event HostAction OnHost;

    public delegate void UnHostAction(GameObject _child, GameObject _parent);
    public static event UnHostAction OnUnHost;

    public delegate void TurnStartAction(GameObject ActivePlayer);
    public static event TurnStartAction OnTurnStart;

    public delegate void TurnEndAction();
    public static event TurnEndAction OnTurnEnd;

    public delegate void PhaseStartAction();
    public static event PhaseStartAction OnPhaseStart;

    public delegate void PhaseEndAction();
    public static event PhaseEndAction OnPhaseEnd;

    public delegate void ChallengeResolutionAction();
    public static event ChallengeResolutionAction OnChallengeResolved;

    public delegate void RoundStartAction();
    public static event RoundStartAction OnRoundStart;

    public delegate void RoundEndAction();
    public static event RoundEndAction OnRoundEnd;

    // Awake runs before start
    private void Awake()
    {
        // Sets the instance of GameMasterMain to this one
        Instance = this;
    }

    private void OnEnable()
    {
        // Set the common board to the correct spawn position
        CommonBoard.transform.position = GameInfo.SpawnLocations[NumPlayers][0];
        CommonBoard.transform.rotation = GameInfo.SpawnRoatations[NumPlayers][0];

        // Set the player 1 board to the correct spawn position and add it to the GameInfo
        Player1.transform.position = GameInfo.SpawnLocations[NumPlayers][1];
        Player1.transform.rotation = GameInfo.SpawnRoatations[NumPlayers][1];
        GameInfo.PlayerList.Add(Player1);
        GameInfo.PlayerTurnOrder.Add(0);

        for (int i = 2; i <= NumPlayers; i++)
        {
            // Create the Player Prefab
            GameObject _newPlayer = Instantiate(Player1,
                                                GameInfo.SpawnLocations[NumPlayers][i],
                                                GameInfo.SpawnRoatations[NumPlayers][i]);
            GameInfo.PlayerList.Add(_newPlayer);
            GameInfo.PlayerTurnOrder.Add(i - 1);
        }
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
        DebugOverlayText += "\n" + "It's " + GameInfo.GetActivePlayer().name + "'s Turn";
        DebugOverlayText += "\n" + "Phase: " + GameInfo.PhaseOrder[GameInfo.PhaseCounter];
        DebugOverlayText += ", Turn: " + GameInfo.TurnCounter;

        HoverDebug.text = DebugOverlayText.Trim('\n', ' ');
    }
    
    public void EndTurn()
    {
        // Fire off the OnTurnEnd event
        OnTurnEnd?.Invoke();

        // If the turn counter would roll over
        if (GameInfo.TurnCounter + 1 == NumPlayers)
        {
            // Inoke the phase ennd event
            OnPhaseEnd?.Invoke();
            
            // If this is the last phase in the round
            if (GameInfo.PhaseCounter + 1  == GameInfo.PhaseOrder.Count())
            {
                // Trigger the round end
                OnRoundEnd?.Invoke();
                
                // Reset all the counters
                GameInfo.RoundCounter++;
                GameInfo.PhaseCounter = 0;
                GameInfo.TurnCounter = 0;

                // Update the iniative:
                GameInfo.UpdatePlayerInitiative();

                // Trigger the on round phase and turn start events
                OnRoundStart?.Invoke();
                OnPhaseStart?.Invoke();
                OnTurnStart?.Invoke(GameInfo.GetActivePlayer());
            }
            else
            {
                // If the phase counter won't roll over Increment it
                GameInfo.PhaseCounter++;
                GameInfo.TurnCounter = 0;
                // Then invoke the phase start and turn start
                OnPhaseStart?.Invoke();
                OnTurnStart?.Invoke(GameInfo.GetActivePlayer());
            }
        }
        else
        {
            // If the TurnOrder order tracker won't roll over
            GameInfo.TurnCounter++;
            OnTurnStart?.Invoke(GameInfo.GetActivePlayer());
        }
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
