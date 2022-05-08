using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTP : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] CharacterController characterController;

    [Header("Movement")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rotationSpeed = 180f;
    [SerializeField] float gravityForce = 10f;
    [SerializeField] float jumpDuration = 1f;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] AnimationCurve jumpCurve = AnimationCurve.EaseInOut(0,0,1,1);
    [Header("Collision")]
    [SerializeField] float checkRadius = .3f;
    [SerializeField] LayerMask checkLayerMask = 1;

    [Header("Input")]
    [SerializeField] string xInputName = "Horizontal";
    [SerializeField] string yInputName = "Vertical";
    [SerializeField] string jumpInputName = "Jump";

    bool isJumping = false;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics.CheckSphere(transform.position, checkRadius, checkLayerMask);

        MoveCharacter();
        if(GetMovementDirection() != Vector3.zero)
		{
            RotateCharacter();

		}
        if(isGrounded)
		{
            if(!isJumping)
			{
                velocity.y = 0f;
			}
            Jump();
            //Debug.Log("GROUNDED");
		}
    }

    public Vector3 GetMovementDirection()
	{
        float xInputValue = Input.GetAxis(xInputName);
        float yInputValue = Input.GetAxis(yInputName);

        return new Vector3(xInputValue, 0f, yInputValue).normalized;
	}

    public void MoveCharacter()
	{
        Vector3 moveDir = transform.TransformDirection(GetMovementDirection()) * movementSpeed;
        moveDir.y = 0f;

        velocity.x = moveDir.x;
        velocity.z = moveDir.z;
        velocity.y -= gravityForce * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
	}

    public void Jump()
	{
        if(Input.GetButtonDown(jumpInputName) && !isJumping)
		{
            //velocity.y = jumpForce;
            isJumping = true;
            StartCoroutine(JumpAscend());
		}
	}

    public void RotateCharacter ()
	{
        Vector3 cameraRot = mainCamera.transform.eulerAngles;
        cameraRot.x = 0f;
        cameraRot.z = 0f;

        transform.eulerAngles = cameraRot;
	}

    public IEnumerator JumpAscend()
	{
        float timer = 0f;
        while(timer < jumpDuration)
		{
            timer += Time.deltaTime;

            float ratio = timer / jumpDuration;
            velocity.y = jumpCurve.Evaluate(ratio) * jumpForce;

            yield return null;
		}
        isJumping = false;
        Debug.Log("Jump Over ! ");
	}
}
