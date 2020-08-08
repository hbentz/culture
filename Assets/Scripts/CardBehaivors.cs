using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaivors : MonoBehaviour
{
    // Intended to hold custom events about the card
    public AudioClip PickUpSound;
    public AudioClip PlaceSound;

    void Start()
    {
    }

    private void OnMouseOver()
    {
        GameMasterMain.Instance.GenericHover(this.transform.gameObject);
    }
    private void OnMouseExit()
    {
        GameMasterMain.Instance.GenericUnHover(this.transform.gameObject);
    }
    private void OnMouseDown()
    {
        // Maybe play a sound and Pickup the object in GameMasterMain
        if (GetComponent<AdvancedProperties>().IsDragable)
        {
            GetComponent<AudioSource>().PlayOneShot(PickUpSound);
        }
        GameMasterMain.Instance.GenericPickup(this.transform.gameObject);
        
    }
    private void OnMouseUpAsButton()
    {
        // Maybe play a sound and Place this object in GameMasterMain
        if (GetComponent<AdvancedProperties>().IsDragging)
        {
            GetComponent<AudioSource>().PlayOneShot(PlaceSound);
        }
        GameMasterMain.Instance.GenericRelease(this.transform.gameObject);
    }
    private void OnMouseDrag()
    {
        GameMasterMain.Instance.GenericDrag(this.transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
