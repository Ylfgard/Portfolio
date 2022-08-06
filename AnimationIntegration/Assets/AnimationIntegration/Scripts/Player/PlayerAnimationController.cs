using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public void PlayMoveAnim(Vector3 direction)
    {
        int anim = GetAnimHash(direction);
        _animator.Play(anim);
    }

    public void PlayFinishingAnim()
    {
        _animator.Play(AnimationsKeeper.Finishing);
    }

    private int GetAnimHash(Vector3 direction)
    {
        if (direction.z > 0 || direction.x != 0) return AnimationsKeeper.Forward;
        if(direction.z < 0) return AnimationsKeeper.Back;
        return AnimationsKeeper.Idle;
    }
}
