using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum FacingDirection
    {
        left, right
    }

    public Rigidbody2D rb;
    public float maxSpeed = 8f;
    public float accelerationTime = 0.2f;
    public float decelerationTime = 0.1f;

    public float walkingThreshold = 0.01f;
    private FacingDirection facingDirection = FacingDirection.right;

    void Start()
    {
        
    }

    void Update()
    {
        
        Vector2 playerInput = new Vector2();

        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            playerInput += Vector2.left;
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            playerInput += Vector2.right;
        }

        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 movementInput)
    {
        float velocityX = rb.velocity.x;

        if(movementInput.x != 0f)
        {
            float acceleration = maxSpeed / accelerationTime;
            velocityX += movementInput.x * acceleration * Time.deltaTime;

            if (velocityX > maxSpeed)
            {
                velocityX = maxSpeed;
            }
            else if (velocityX < -maxSpeed)
            {
                velocityX = -maxSpeed;
            }

            if (movementInput.x < 0f)
            {
                facingDirection = FacingDirection.left;
            }
            else
            {
                facingDirection = FacingDirection.right;
            }
        }
        else
        {
            float deceleration = maxSpeed / decelerationTime;
            if (velocityX > 0f)
            {
                velocityX -= deceleration * Time.deltaTime;
                if (velocityX < 0f)
                {
                    velocityX = 0f;
                }
            }
            else if (velocityX < 0f)
            {
                velocityX += deceleration * Time.deltaTime;
                if (velocityX > 0f)
                {
                    velocityX = 0f;
                }
            }
        }

        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }

    public bool IsWalking()
    {
        return rb.velocity.x > walkingThreshold || rb.velocity.x < -walkingThreshold;
    }
    public bool IsGrounded()
    {
        return false;
    }

    public FacingDirection GetFacingDirection()
    {
        return facingDirection;
    }
}
