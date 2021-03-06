using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElusiveRimba
{

    public class Knife : MonoBehaviour
    {
        [SerializeField] private float speed = 35;
        [SerializeField] private float rotationSpeed = 100f;

        private Collider2D coll;
        private Rigidbody2D rb;
        private KnifePickUp knifePickUpScript;
        private Vector3 startPos, currentPos, endPos;
        private float range;
        private bool isResting;

        private void Awake()
        {
            range = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>().knifeRange;
            coll = GetComponent<Collider2D>();
            rb = GetComponent<Rigidbody2D>();
            knifePickUpScript = GetComponent<KnifePickUp>();
        }

        private void Start()
        {
            startPos = transform.position;

            //Debug.LogError("Throw knife sound here");
            
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if(Mathf.Abs((transform.position - startPos).magnitude) < range)
            {
                transform.Translate(Vector3.up * speed * Time.fixedDeltaTime, Space.Self);
            }
            else
            {
                // Projectile ranged stop actions
                ProjectileStopped();
                FMODUnity.RuntimeManager.PlayOneShot("event:/knife fall");
            }
        }

        private void ProjectileStopped()
        {
            rb.Sleep();
            coll.isTrigger = true;
            knifePickUpScript.canPickUp = true;

            enabled = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            switch(other.gameObject.tag)
            {
                case "Enemy":
                    // Enemy hit sound and blood effects
                    ProjectileStopped();
                    other.gameObject.TryGetComponent(out Enemy enemy);
                    enemy.Died();
                    FMODUnity.RuntimeManager.PlayOneShot("event:/enemy damage knife");
                    Debug.LogWarning("Knife hits enemy sound here");
                    break;

                case "Obstacle":
                    // Obstacle hit sound and maybe some particle effects
                    ProjectileStopped();
                    Debug.LogWarning("Knife hits obstacle sound here");
                    FMODUnity.RuntimeManager.PlayOneShot("event:/knife fall");
                    break;

                default:
                    // Other collisions
                    break;
            }
        }
    }
}
