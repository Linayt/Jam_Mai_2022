using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 10f;
    [SerializeField] float dragValue = 0.3f;

    [Header("Jump")]
    [SerializeField] float jumpDuration = 1f;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] AnimationCurve jumpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    //[SerializeField] private float jumpHeight;
    [SerializeField] private int nbMidAirJump;
    private int midAirJumpCount;


    [Header("GroundCheck")]
    [SerializeField] private Transform groundCheckOrigin;
    [SerializeField] private float groundCheckRadius = 0.5f;
    [SerializeField] private LayerMask groundCheckMask;

    [Header("Gravity")]
    [SerializeField] private float gravityValue = -9.81f;

    [Header("Input")]
    [SerializeField] private string xInput;
    [SerializeField] private string jumpInput;


    Vector3 velocity;
    bool isGrounded;
    bool canMove;
    bool isJumping;

    float dragvelocity;

    PlayerController pController;
    internal void Initialize()
    {
        pController = GetComponent<PlayerController>();
        velocity = Vector3.zero;
        velocity.y = 0f;
        isJumping = false;
        midAirJumpCount = nbMidAirJump;
        canMove = true;
    }

    public void Move()
    {
        Vector3 moveDirec = GetInputDirection() * speed;
        moveDirec.y = 0;

        isGrounded = Physics.CheckSphere(groundCheckOrigin.position, groundCheckRadius, groundCheckMask);

        if (isGrounded)
        {
            velocity.y = -2f;
            midAirJumpCount = nbMidAirJump;
        }
        else
        {
            velocity.y += gravityValue * Time.deltaTime;
        }

        if (canMove)
        {
            pController.charControl.Move(moveDirec * Time.deltaTime);
        }
        pController.charControl.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (Input.GetButtonDown(jumpInput) && !isJumping)
        {
            if (isGrounded)
            {
                StartCoroutine(JumpMotion());
            }
            else if( midAirJumpCount > 0)
            {
                midAirJumpCount -= 1;
                velocity.y = 0;
                StartCoroutine(JumpMotion());

            }
            
        }
    }

    public IEnumerator JumpMotion()
    {
        isJumping = true;
        float timer = 0;
        while (timer < jumpDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer.Ratio(jumpDuration);
            velocity.y = jumpCurve.Evaluate(ratio) * jumpForce;
            yield return null;
        }
        isJumping = false;
    }


    public void AddExternalForce(Vector3 force, bool resetForce)
    {
        if (resetForce)
        {
            velocity = force;
        }
        else
        {
            velocity += force;
        }
    }

    public void VelocityDrag()
    {
        velocity.x = Mathf.SmoothDamp(velocity.x, 0, ref dragvelocity, dragValue);

    }

    public Vector3 GetInputDirection()
    {
        return new Vector3(Input.GetAxisRaw(xInput), 0, 0);
    }
}
