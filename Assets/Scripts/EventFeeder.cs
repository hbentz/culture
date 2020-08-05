using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFeeder : MonoBehaviour
{
    // Intended to handle EventSystem input only
    void OnMouseDrag()
    {
        GameMasterMain.Instance.GenericDrag(this.transform.gameObject);
    }
}
