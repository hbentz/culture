using UnityEngine;
using UnityEditor;
using System;

public class FunctionProfiler : MonoBehaviour
{
    public double TestTime = 1;  // Seconds
    
    static void DoIt()
    {
        DateTime start = DateTime.Now;
        Int64 counter = 0;

        while (DateTime.Now < start.AddSeconds(1))
        {
            TestFunction();
            counter++;
        }
        Debug.Log(counter);
    }

    static void TestFunction()
    {
        // Content goes here
    }
}