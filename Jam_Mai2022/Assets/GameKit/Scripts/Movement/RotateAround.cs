using UnityEngine;

public class RotateAround : MonoBehaviour
{
	public Transform targetTransform;
	[SerializeField] private Vector3 rotateAxis;
	[SerializeField] private float rotateSpeed;
	public bool useInput;
	[SerializeField] private string inputName;

	// Update is called once per frame
	private void Update ()
	{
		if (!targetTransform) return;
		
		float mvt = 1f;
		
		if(useInput)
		{
			mvt = Input.GetAxis(inputName);
			
			if (mvt == 0f) return;
		}

		transform.RotateAround(targetTransform.position, rotateAxis, rotateSpeed * Time.deltaTime * mvt);
	}
}
