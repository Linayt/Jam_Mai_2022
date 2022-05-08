using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerEtats))]
[RequireComponent(typeof(PlayerMover))]

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IHittable
{
    [HideInInspector] public PlayerAttack pAttack;
    [HideInInspector] public PlayerEtats pEtats;
    [HideInInspector] public PlayerMover pMover;

    [HideInInspector] public CharacterController charControl;

    [SerializeField] private Animator animator;
    [SerializeField] private string onHitAnimatorTrigger = "Hit";
    public int playerIndex = 1;

    [Header("Knockback")] 
    [SerializeField] private float moverCancelMinDuration = 1f;
    [SerializeField] private float moverCancelMaxDuration = 3f;


    private void OnEnable()
    {
        pAttack = GetComponent<PlayerAttack>();
        pAttack.Initialize();

        pEtats = GetComponent<PlayerEtats>();
        pEtats.Initialize();

        pMover = GetComponent<PlayerMover>();
        pMover.Initialize();


        charControl = GetComponent<CharacterController>();
    }

    private IEnumerator DisableMovement(float t)
    {
        float ratio = Helper.CurvedLerp(moverCancelMinDuration, moverCancelMaxDuration, t);

        yield return Helper.GetWait(ratio);
    }
    public void OnHit(float ratio,Vector3 directionHit)
    {
        Debug.Log(gameObject.name + " is Hit !", this);
        if (!animator) return;

        animator.SetTrigger(onHitAnimatorTrigger);
    }

    private void Update()
    {
        pAttack.Attack();
        pMover.Move();
        pMover.Jump();
        pMover.VelocityDrag();
    }
}
