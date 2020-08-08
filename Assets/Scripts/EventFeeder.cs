using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFeeder : MonoBehaviour
{
    // Intended to handle EventSystem input only
    private void OnMouseOver()
    {
        GameMasterMain.Instance.GenericHover(this.transform.parent.gameObject);
    }
    private void OnMouseExit()
    {
        GameMasterMain.Instance.GenericUnHover(this.transform.parent.gameObject);
    }
    private void OnMouseDown()
    {
        GameMasterMain.Instance.GenericPickup(this.transform.parent.gameObject);
    }
    private void OnMouseUpAsButton()
    {
        GameMasterMain.Instance.GenericRelease(this.transform.gameObject);
    }
    private void OnMouseDrag()
    {
        GameMasterMain.Instance.GenericDrag(this.transform.gameObject);
    }
}
