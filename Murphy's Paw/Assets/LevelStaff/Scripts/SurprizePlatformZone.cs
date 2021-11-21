using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurprizePlatformZone : MonoBehaviour
{
    [SerializeField] private MovingPlatform[] platforms;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            foreach(MovingPlatform platform in platforms)
                platform.moveOnTrigger = false;
    }
}
