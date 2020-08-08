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
        GetComponent<AudioSource>().PlayOneShot(PickUpSound);
    }

    void OnPlace()
    {
        GetComponent<AudioSource>().PlayOneShot(PlaceSound);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
