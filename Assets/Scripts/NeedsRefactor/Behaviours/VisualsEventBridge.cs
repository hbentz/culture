using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualsEventBridge : MonoBehaviour
{
    // Intended to throw event system input up to GameMasterMain and give reference to the parent object
    private void OnMouseOver()
    {
        GameMasterMain.Instance.GenericHover(this.transform.parent.gameObject);
    }
    private void OnMouseExit()
    {
        //this.transform.parent.gameObject.GetComponent<GameResource>().WhenClicked();
        GameMasterMain.Instance.GenericUnHover(this.transform.parent.gameObject);
    }
    private void OnMouseDown()
    {
        GameMasterMain.Instance.GenericPickup(this.transform.parent.gameObject);
    }
    private void OnMouseUpAsButton()
    {
        GameMasterMain.Instance.GenericRelease(this.transform.parent.gameObject);
    }
    private void OnMouseDrag()
    {
        GameMasterMain.Instance.GenericDrag(this.transform.parent.gameObject);
    }
}
