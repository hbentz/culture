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

    private void OnMouseOver()
    {
        GameMasterMain.Instance.GenericHover(this.transform.gameObject);
    }
    private void OnMouseExit()
    {
    }
    private void OnMouseDown()
    {
        GameMasterMain.Instance.GenericPickup(this.transform.gameObject);
    }
    private void OnMouseUpAsButton()
    {
        GameMasterMain.Instance.GenericRelease(this.transform.gameObject);
    }
    private void OnMouseDrag()
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
