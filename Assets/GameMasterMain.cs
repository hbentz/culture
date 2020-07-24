using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameMasterMain : MonoBehaviour
{
    public int PlayerIDTurn = 1;
    public bool PlayerMouseButtonDown = false;
    public int NumberTicks = 0;
    public GameObject


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

        int.TryParse(_evilInt, out _chrissInt);
        Debug.Log(_chrissInt + 5);
    }

    // Keep all updates here for readability
    void Update()
    {
        /* TODO
         * Have card area for hand 
        On mousedown
        Start raycast from mouse away a camera
        Get first intersect
        Move until mouseup
        determine which cardslot the center is over
        Clamp to center the card onto that
        If the center is not over return
        */

        // Primary click 0, context click 1, middle click 2
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 UserMousePos = Input.mousePosition;
            Physics.Raycast.
        }
    }
}
