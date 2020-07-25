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
        // Performance profiler (very serious)
        DateTime _timeLoaded = DateTime.Now;
        while((DateTime.Now.Ticks - _timeLoaded.Ticks) < 10000000)
        {
            NumberTicks++;
        }
        Debug.Log(NumberTicks);

        string _testParse = "5";
        string _evilInt = "3246sasdf874";
        int _chrissInt = 0;

        // TryParse never throws execption
        // "out" actually modifies the object directly as if it
        // were a public var ONLY for that function
        int.TryParse(_testParse, out _chrissInt);
        Debug.Log(_chrissInt + 5);

        // TryParse returns 0 when it cannot parse
        int.TryParse(_evilInt, out _chrissInt);
        Debug.Log(_chrissInt + 5);
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
