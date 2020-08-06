using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFeeder : MonoBehaviour
{
    // Intended to handle EventSystem input only
    void OnMouseOver()
    {
        GameMasterMain.Instance.GenericHover(this.transform.gameObject);
    }
    void OnMouseDown()
    {
        GameMasterMain.Instance.GenericPickup(this.transform.gameObject);
    }
    void OnMouseDrag()
    {
        GameMasterMain.Instance.GenericDrag(this.transform.gameObject);
    }
    void OnMouseUpAsButton()
    {
        GameMasterMain.Instance.GenericRelease(this.transform.gameObject);
    }
}
