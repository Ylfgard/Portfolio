using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class DarkestTunnel : MonoBehaviour
{
    [SerializeField] private Light2D[] lights;
    [SerializeField] private float speedIntencity;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(ChangeLightIntencity(true));
        }
            
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(ChangeLightIntencity(false));
        }
    }

    private IEnumerator ChangeLightIntencity(bool minus)
    {
        yield return new WaitForSeconds(0.01f);
        if(minus)
        {
            foreach(Light2D light in lights)
            {
                light.intensity -= speedIntencity;
            }
            if(lights[0].intensity > 0)
                StartCoroutine(ChangeLightIntencity(minus));
            else
                lights[0].intensity = 0;
        }
        else
        {
            foreach(Light2D light in lights)
            {
                light.intensity += speedIntencity;
            }
            if(lights[0].intensity < 1)
                StartCoroutine(ChangeLightIntencity(minus));
            else
                lights[0].intensity = 1;
        }
    }
}
