using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{
	[SerializeField] private Camera cam;

	[SerializeField] private bool lockOnX = false;
	[SerializeField] private bool lockOnY = true;
	[SerializeField] private bool lockOnZ = false;

	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private bool lookTowardsDestination = true;

	[SerializeField] private Vector3 offset = new Vector3(0f, 0.2f, 0f);
	[SerializeField] private LayerMask clickMask = ~0;
	[SerializeField] private GameObject clickMarker = null;

	[System.Serializable]
	public class AnimOptions
	{
		public Animator animator;
		public string isMovingParameterName = "Moving";
		public bool trackFacingDirection = false;
		public string hParameterName = "Horizontal";
		public string vParameterName = "Vertical";

	}

	public AnimOptions animOptions;

	private GameObject currentClickMarker;
	private ParticleSystem particles;

	private Vector3 targetPos;
	private bool isMoving = false;
	private bool isStopped = true;

	public bool displayDebugInfo;
	
	private void Start ()
	{
		if(clickMarker != null)
		{
			currentClickMarker = Instantiate(clickMarker, transform.position, clickMarker.transform.rotation);
			particles = currentClickMarker.GetComponent<ParticleSystem>();

			if(particles == null)
			{
				currentClickMarker.SetActive(false);
			}
			else
			{
				particles.Stop();
			}
		}
		Debug.Log(clickMask);

		if(cam == null)
		{
			cam = Camera.main;
		}

		if(animOptions.animator == null)
		{
			animOptions.animator = GetComponentInChildren<Animator>();
			Debug.LogWarning("No animator referenced !", gameObject);
		}
	}

	void UpdateAnimValues()
	{
		if(animOptions.animator != null)
		{
			animOptions.animator.SetBool(animOptions.isMovingParameterName, isMoving);

			if(animOptions.trackFacingDirection)
			{
				animOptions.animator.SetFloat(animOptions.hParameterName, transform.forward.x);
				animOptions.animator.SetFloat(animOptions.vParameterName, transform.forward.y);
			}
		}
	}

	void SetTargetPos()
	{
		if(cam!=null)
		{
			if(Input.GetMouseButton(0))
			{
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, 100f, clickMask))
				{
					//Debug.Log(hit.point);
					Vector3 cursorPos = hit.point;
					if (lockOnY)
					{
						cursorPos.y = transform.position.y;
					}
					if (lockOnX)
					{
						cursorPos.x = transform.position.x;
					}
					if (lockOnZ)
					{
						cursorPos.z = transform.position.z;
					}

					if(Vector3.Distance(cursorPos, transform.position) > 1f)
					{
						if(currentClickMarker != null)
						{
							currentClickMarker.transform.position = cursorPos + offset;
							if (particles == null)
							{
								currentClickMarker.SetActive(false);
								currentClickMarker.SetActive(true);
							}
							else
							{
								if(isStopped == true)
								{
									particles.Play();
									isStopped = false;
								}
							}
						}

						targetPos = cursorPos;
						isMoving = true;
					}
				}
			}
		}	
	}

	void MovetowardsTargetPos()
	{
		if(Vector3.Distance(transform.position, targetPos) > 0.1f)
		{
			Vector3 movement = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
			transform.position = movement;

			if (lookTowardsDestination)
			{
				Vector3 cursorPos = targetPos;
				if (lockOnY)
				{
					cursorPos.y = transform.position.y;
				}
				else if (lockOnX)
				{
					cursorPos.x = transform.position.x;
				}
				else if (lockOnZ)
				{
					cursorPos.z = transform.position.z;
				}

				transform.LookAt(cursorPos);
			}
		}
		else
		{
			isMoving = false;
			if (currentClickMarker != null)
			{
				if (particles == null)
				{
					currentClickMarker.SetActive(false);
				}
				else
				{
					particles.Stop();
					isStopped = true;
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		//(ceDebug.Log(transform.forward);
		SetTargetPos();
		UpdateAnimValues();
	}

	private void FixedUpdate ()
	{
		if (isMoving)
		{
			MovetowardsTargetPos();
		}
	}
}
