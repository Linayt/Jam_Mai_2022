using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    [Header("Input")]
    [SerializeField] string attackInput;

    [Header("Range")]
    [SerializeField] Transform attackCollisionOrigin;
    [SerializeField] Transform attackHitDirectionOrigin;
    [SerializeField] float attackMinRange;
    [SerializeField] float attackMaxRange;
    [SerializeField] AnimationCurve attackRangeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] LayerMask attackLayerMask;

    [Header("Power")]
    [SerializeField] float attackMinPower;
    [SerializeField] float attackMaxPower;
    [SerializeField] AnimationCurve attackPowerCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);


    [SerializeField] float attackBumpDirectionYOffset=1;

    [Header("Timing")]
    [SerializeField] float attackMaxTimeCharge;
    [SerializeField] float chargeSpeed;
    [SerializeField] float attackCooldown;

    [Header("Feedback")]
    [SerializeField] Image UIGauge;
    [SerializeField] string animationParameterAttack;

    bool onCharge;
    float chargeValue;
    float attackCurrentCooldown;

    PlayerController pController;
    internal void Initialize()
    {
        pController = GetComponent<PlayerController>();
        onCharge = false;
        chargeValue = 0;
        attackCurrentCooldown = 0;
    }

    
    public void Attack()
    {
        UIGauge.fillAmount = chargeValue;

        if (Input.GetButtonDown(attackInput) && attackCurrentCooldown.PunctualCooldownCheck(attackCooldown))
        {
            onCharge = true;
        }
        if (Input.GetButton(attackInput) && onCharge)
        {
            chargeValue = Math.Clamp(chargeValue + Time.deltaTime, 0, 1);
            
        }
        if (Input.GetButtonUp(attackInput) && onCharge)
        {
            CheckAttackCollision(chargeValue);
            onCharge = false;
            chargeValue = 0;
        }
    }

    private void CheckAttackCollision(float ratio)
    {
        float attackRadius = Helper.CurvedLerp(attackMinRange, attackMaxRange, attackRangeCurve, ratio);

        Collider[] hits = Physics.OverlapSphere(attackCollisionOrigin.position, attackRadius, attackLayerMask);
        if (hits.Length == 0) return;
        foreach (var hit in hits)
        {
            var hitable = hit.GetComponent<IHittable>();
            if (hitable != null)
            {
                Vector3 hitDirection = VectorExtensions.Towards(attackHitDirectionOrigin.position, hit.transform.position + new Vector3(0, attackBumpDirectionYOffset, 0), true);
                hitable.OnHit(ratio, hitDirection);
            }
        }
        
    }
}
