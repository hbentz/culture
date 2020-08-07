using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaivors : MonoBehaviour
{
    // Intended to hold custom events about the card
    // Start is called before the first frame update
    void Start()
    {
    }

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

    // Update is called once per frame
    void Update()
    {
    }
}
