using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHittable
{
    [SerializeField] private Animator animator;
    [SerializeField] private string onHitAnimatorTrigger = "Hit";
    public int playerIndex = 1;

    [Header("Knockback")] 
    [SerializeField] private float moverCancelMinDuration = 1f;
    [SerializeField] private float moverCancelMaxDuration = 3f;

    private IEnumerator DisableMovement(float t)
    {
        float ratio = Helper.CurvedLerp(moverCancelMinDuration, moverCancelMaxDuration, t);

        yield return Helper.GetWait(ratio);
    }
    public void OnHit(float ratio)
    {
        if (!animator) return;

        animator.SetTrigger(onHitAnimatorTrigger);
    }
}
