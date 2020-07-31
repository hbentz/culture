using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaivors : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameMasterMain.Instance.GenericDrag(this.transform.gameObject);
    }
}
