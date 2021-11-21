using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private float deathHight;
    private Transform playerTransf;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            playerTransf = other.transform;
            PlayerDeath plD = other.GetComponent<PlayerDeath>();
            plD.deathHight = deathHight;
            plD.isDead.RemoveAllListeners();
            plD.isDead.AddListener(RestartFromChekpoint);
        }
    }

    private void RestartFromChekpoint()
    {
        playerTransf.position = transform.position;
    }
}
