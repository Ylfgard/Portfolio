using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeCombat : MonoBehaviour
{
    [SerializeField] private Transform hitPoint;
    [SerializeField] private float hitRange;
    [SerializeField] private float attackRate;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private FMODUnity.EventReference fmodSoundPath;
    private FMOD.Studio.EventInstance instance;
    private Animator _animator;
    private bool canAttack;

    private void Start() 
    {
        canAttack = true;
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if(canAttack && Input.GetButton("Interact")) 
            StartCoroutine(Attack());
    }
    
    private IEnumerator Attack()
    {   
        StopMovement();
        canAttack = false;
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }

    private void StopMovement()
    {
        gameObject.GetComponent<PlayerMovement>().canMove = false;
        gameObject.GetComponent<PlayerJump>().canMove = false;
    }

    public void CanMoveAgain()
    {
        gameObject.GetComponent<PlayerMovement>().canMove = true;
        gameObject.GetComponent<PlayerJump>().canMove = true;
    }

    public void Interact()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodSoundPath);
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform)); 
        instance.start();
        Collider2D [] hitObjects = Physics2D.OverlapCircleAll(hitPoint.position, hitRange, interactableLayer);

        foreach (Collider2D hitObj in hitObjects)
            hitObj.GetComponent<IInteractable>().Activate();
    }

    private void OnDrawGizmosSelected() 
    {
        if(hitPoint == null) return;
        Gizmos.DrawWireSphere(hitPoint.position, hitRange);
    }
}
