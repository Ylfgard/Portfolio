using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public bool inJump;
    [HideInInspector] public bool canMove;
    [HideInInspector] public float xDirection;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private float moveSpeed;
    [SerializeField] private FMODUnity.EventReference fmodSoundPath;
    private FMOD.Studio.EventInstance instance;
    private Animator _animator;
    private Transform _transform;
    private bool inReverseZone;
    
    private void Start()
    {
        canMove = true;
        _transform = gameObject.GetComponent<Transform>();
        _animator = gameObject.GetComponent<Animator>();
    }
    
    private void Update()
    {
        if(canMove == true || playerJump.grounded == false)
        {
            if(inReverseZone) xDirection = -Input.GetAxis("Horizontal");
            else xDirection = Input.GetAxis("Horizontal");
            if(xDirection != 0) _animator.SetBool("Walk", true);
            else _animator.SetBool("Walk", false);

            if(xDirection < 0 && _transform.rotation.y != 0) _transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            else if(xDirection > 0 && _transform.rotation.y == 0) _transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));  
        }
    }

    private void FixedUpdate() 
    {
        if((canMove == true || playerJump.grounded == false)  && inJump == false)
        {
            Vector3 offset = Vector3.zero;
            offset.x = xDirection;
            offset = offset.normalized * moveSpeed;
            _transform.position += offset * Time.deltaTime;
        }
    }

    public IEnumerator ChangeReverseState(bool state)
    {
        StopAllCoroutines();
        yield return new WaitForSeconds(0.5f);
        inReverseZone = state;
        gameObject.GetComponent<PlayerJump>().inReverseZone = state;
    }

    public void PlaySoundOfStep()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodSoundPath);
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform)); 
        instance.start();
    }
}
