using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player") StartCoroutine(other.GetComponent<PlayerMovement>().ChangeReverseState(true));
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player") StartCoroutine(other.GetComponent<PlayerMovement>().ChangeReverseState(false));
    }
}
