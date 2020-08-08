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

    void OnPickup()
    {
        // Play PickUpSound from this object's AudioSource
        GetComponent<AudioSource>().PlayOneShot(PickUpSound);
    }

    void OnPlace()
    {
        // Play PlaceSound from this object's AudioSource
        GetComponent<AudioSource>().PlayOneShot(PlaceSound);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
