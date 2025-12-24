using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetTalking(bool value)
    {
        if (animator == null) return;
        animator.SetBool("IsTalking", value);
    }
}


