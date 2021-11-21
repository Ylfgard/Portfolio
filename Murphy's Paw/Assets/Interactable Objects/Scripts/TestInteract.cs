using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteract : MonoBehaviour, IInteractable
{
    public void Activate()
    {
        Debug.Log("Ouch! You hit me!");
        Destroy(gameObject);
    }
}
