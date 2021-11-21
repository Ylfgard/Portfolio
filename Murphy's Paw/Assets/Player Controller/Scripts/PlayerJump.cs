using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool grounded;
    [HideInInspector] public float xDirection;
    [SerializeField] private Transform groundCheker;
    [SerializeField] private float groundChekRadius;
    [SerializeField] private float gravityScale;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AnimationCurve jumpTrajectory;
    [SerializeField] private float duration;
    [SerializeField] private float hight;
    [SerializeField] private float maxLenght;
    [SerializeField] private FMODUnity.EventReference fmodSoundPathJump;
    private FMOD.Studio.EventInstance instanceJump;
    [SerializeField] private FMODUnity.EventReference fmodSoundPathLanding;
    private FMOD.Studio.EventInstance instanceLanding;
    public bool inReverseZone;
    private PlayerMovement playerMovement;
    private Transform _transform;
    private Rigidbody2D _r2d;
    private Animator _animator;
    private float progress;
    private bool inJump;
    private bool needPlayLandSound;

    private void Start()
    {
        canMove = true;
        needPlayLandSound = false;
        _r2d = gameObject.GetComponent<Rigidbody2D>();
        _transform = gameObject.GetComponent<Transform>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if(inJump == false && _r2d.gravityScale < gravityScale) _r2d.gravityScale = gravityScale;
        if(grounded && Input.GetButtonDown("Jump")) JumpBegin();
        if(Input.GetButtonUp("Jump")) JumpEnding();
        if(inJump)
        {
            if(Input.GetAxis("Horizontal") == 0)
            {
                xDirection = 0;
            } 
            else 
            {
                if(inReverseZone) xDirection = -Mathf.Sign(Input.GetAxis("Horizontal"));
                else xDirection = Mathf.Sign(Input.GetAxis("Horizontal"));
                
                if(xDirection < 0 && _transform.rotation.y != 0) _transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                else if(xDirection > 0 && _transform.rotation.y == 0) _transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }
        
        if(needPlayLandSound && inJump == false && grounded)
        {
            instanceLanding = FMODUnity.RuntimeManager.CreateInstance(fmodSoundPathLanding);
            instanceLanding.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform)); 
            instanceLanding.start();
            needPlayLandSound = false;
        }
    }

    private void FixedUpdate() 
    {
        grounded = Physics2D.OverlapCircle(groundCheker.position, groundChekRadius, groundLayer);
        
        if(inJump)
        {
            float yPastPosition = jumpTrajectory.Evaluate(progress);
            progress += Time.fixedDeltaTime / duration;
            Vector3 offset = Vector2.zero;
            offset.x = xDirection * maxLenght / duration * Time.fixedDeltaTime;
            offset.y = (jumpTrajectory.Evaluate(progress) - yPastPosition) * hight;

            _transform.position += offset;
            if((progress >= 0.2f && grounded) || progress >= 1)
                JumpEnding();
        }
    }

    private void JumpBegin()
    {
        if(canMove == true)
        {
            instanceJump = FMODUnity.RuntimeManager.CreateInstance(fmodSoundPathJump);
            instanceJump.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform)); 
            instanceJump.start();
            _animator.SetTrigger("Jump");
            progress = 0;
            inJump = true;
            needPlayLandSound = true;
            _r2d.gravityScale = 0;
            playerMovement.inJump = true;
        }
    }

    private void JumpEnding()
    {
        inJump = false;
        xDirection = 0;
        _r2d.gravityScale = gravityScale;
        _r2d.velocity += progress * duration * Physics2D.gravity * gravityScale;
        playerMovement.inJump = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheker.position, groundChekRadius);
    }
}