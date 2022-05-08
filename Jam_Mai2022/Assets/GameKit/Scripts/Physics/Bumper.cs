using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
	//[Header("Forces")]
	[Tooltip("Force and direction of the propulsion")]
	public Vector3 bumpForce = new Vector3(0f, 10, 0f);
	[Tooltip("Do we add an additional force towards the colliding object ?")]
	public bool bumpTowardsOther = false;
	[Tooltip("Additional force added towards the colliding object ?")]
	[SerializeField] float additionalForceTowardsOther = 500f;
	[SerializeField] bool preventInputHolding = false;

	//[Header("Tag")]
	[Tooltip("Do we bump only objects with a specific tag ?")]
	public bool useTag = false;
	[Tooltip("Name of the tag used on collision")]
	public string tagName = "Player";

	public bool displayDebugInfo = true;
	
	private void OnCollisionEnter (Collision collision)
	{
		Vector3 toOther = collision.transform.position - transform.position;
		Rigidbody col = collision.rigidbody;

		if(col == null) return;

		if (useTag && !collision.gameObject.CompareTag(tagName)) return;

		if (preventInputHolding)
		{
			Jumper j = collision.gameObject.GetComponent<Jumper>();

			if (j)
			{
				j.isbeingBumped = true;
			}
		}

		ApplyBump(col, toOther);
	}

	void ApplyBump (Rigidbody col, Vector3 dir)
	{
		col.velocity = Vector3.zero;
		Vector3 direction = dir.WorldToLocalSpace(transform);

		if (bumpTowardsOther)
		{
			col.AddForce(direction.normalized * additionalForceTowardsOther);
		}

		direction = bumpForce.WorldToLocalSpace(transform);
		col.AddForce(direction, ForceMode.Impulse);
	}

	void TriggerHandler(Collider other, Vector3 dir)
	{
		Rigidbody otherRigid = other.GetComponent<Rigidbody>();
		if (otherRigid == null)
		{
			otherRigid = other.GetComponentInParent<Rigidbody>();
		}

		if (otherRigid == null) return;
		
		if (preventInputHolding)
		{
			Jumper j = otherRigid.gameObject.GetComponent<Jumper>();

			if (j)
			{
				j.isbeingBumped = true;
			}
		}

		otherRigid.velocity = Vector3.zero;

		Vector3 force = bumpForce.WorldToLocalSpace(transform);
		if (bumpTowardsOther)
		{
			force = dir.normalized.WorldToLocalSpace(transform);
			otherRigid.AddForce(force * additionalForceTowardsOther + bumpForce, ForceMode.Impulse);
		}

		otherRigid.AddForce(force, ForceMode.Impulse);
	}


	private void OnTriggerEnter (Collider other)
	{
		Vector3 toOther = other.transform.position - transform.position;
		if (useTag && !other.gameObject.CompareTag(tagName)) return;

		TriggerHandler(other, toOther);
	}
}