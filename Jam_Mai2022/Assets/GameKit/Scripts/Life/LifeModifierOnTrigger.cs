using UnityEngine;

public class LifeModifierOnTrigger : MonoBehaviour
{
	public bool useTag = false;
	public string tagName = "Case Sensitive";
	
	[SerializeField] private int lifeAmountChanged = 1;
	[SerializeField] private bool destroyAfter = true;
	
	private void ApplyLifeModification (Life life)
	{
		life.ModifyLife(lifeAmountChanged);
		if (destroyAfter)
		{
			Destroy(gameObject);
		}
	}
	
	private void OnTriggerEnter (Collider other)
	{
		Life life = other.gameObject.GetComponent<Life>();

		if (life == null) return;

		if (useTag && !life.gameObject.CompareTag((tagName))) return;
	
		ApplyLifeModification(life);
	}
}