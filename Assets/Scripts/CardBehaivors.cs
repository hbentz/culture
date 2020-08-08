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
        // Pickup the object in GameMasterMain and maybe play a sound
        GameMasterMain.Instance.GenericPickup(this.transform.gameObject);
        PlayClipIfTag(PickUpSound, "Dragable");
    }
    private void OnMouseUpAsButton()
    {
        // Place this object in GameMasterMain and maybe play a sound
        GameMasterMain.Instance.GenericRelease(this.transform.gameObject);
        PlayClipIfTag(PlaceSound, "Dragging");
    }
    private void OnMouseDrag()
    {
        GameMasterMain.Instance.GenericDrag(this.transform.gameObject);
    }

    void PlayClipIfTag(AudioClip _soundClip, string _tag)
    {
        if (GetComponent<AdvancedProperties>().HasPropertyTag(_tag))
        {
            GetComponent<AudioSource>().PlayOneShot(_soundClip);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
