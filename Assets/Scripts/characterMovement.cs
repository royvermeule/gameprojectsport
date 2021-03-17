using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    private const float acceleration    = 150.0f;
    private const float maxSpeed        = 15f;
    private const float jumpForce       = 17.0f;
    private const float baseGravity     = 40.0f;
    private const float fastFallGravity = 100.0f;
    private const float jumpBufferTime  = 0.1f;
    private const float coyoteTime      = 0.07f;

    public float airTime = 0.0f;
    private bool jumped   = false;

    private float gravity = baseGravity;

    private bool leftPressed        = false;
    private bool rightPressed       = false;
    private bool spacePressed       = false;
    private float spacePressedTime  = 0.0f;

    private int  horizontalMovement = 0;

    private bool onGround = false;

    private Vector2 motion = new Vector2(0, 0);

    private Rigidbody2D rigidbody;

    private BoxCollider2D groundChecker;
    private BoxCollider2D ceilingChecker;
    private BoxCollider2D wallCheckerLeft;
    private BoxCollider2D wallCheckerRight;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody        = GetComponent<Rigidbody2D>();
        groundChecker    = GameObject.Find("GroundChecker").GetComponent<BoxCollider2D>();
        ceilingChecker   = GameObject.Find("CeilingChecker").GetComponent<BoxCollider2D>();
        wallCheckerLeft  = GameObject.Find("WallCheckerLeft").GetComponent<BoxCollider2D>();
        wallCheckerRight = GameObject.Find("WallCheckerRight").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input
        leftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        rightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        if (leftPressed && rightPressed)
        {
            horizontalMovement = 0;
        }
        else if (leftPressed)
        {
            horizontalMovement = -1;
        }
        else if (rightPressed)
        {
            horizontalMovement = 1;
        }
        else
        {
            horizontalMovement = 0;
        }
        spacePressed = Input.GetKey(KeyCode.Space);
        if (spacePressed)
        {
            spacePressedTime += Time.deltaTime;
        }
        else
        {
            spacePressedTime = 0.0f;
        }

        // Check if player is on ground
        if (groundChecker.IsTouchingLayers())
        {
            onGround = true;
            motion.y = 0;
            airTime = 0.0f;
            jumped = false;
        }
        else
        {
            onGround = false;
            airTime += Time.deltaTime;
        }

        if (ceilingChecker.IsTouchingLayers())
        {
            motion.y = -0.5f;
        }

        // Move player
        if (horizontalMovement == -1)
        {
            motion.x -= acceleration * Time.deltaTime;
            motion.x = Mathf.Max(motion.x, -maxSpeed);
        }
        else if (horizontalMovement == 1)
        {
            motion.x += acceleration * Time.deltaTime;
            motion.x = Mathf.Min(motion.x, maxSpeed);
        }
        else
        {
            if (motion.x < 0)
            {
                motion.x = Mathf.Min(0.0f, motion.x + acceleration * Time.deltaTime);
            }
            else if (motion.x > 0)
            {
                motion.x = Mathf.Max(0.0f, motion.x - acceleration * Time.deltaTime);
            }
        }
        
        // Check whether player is touching a wall
        if (wallCheckerLeft.IsTouchingLayers())
        {
            motion.x = Mathf.Max(motion.x, 0.0f);
        }
        else if (wallCheckerRight.IsTouchingLayers())
        {
            motion.x = Mathf.Min(motion.x, 0.0f);
        }

        if (!onGround)
        {
            motion.y -= gravity * Time.deltaTime;
        }

        if (spacePressed && spacePressedTime < jumpBufferTime && airTime < coyoteTime && !jumped)
        {
            motion.y = jumpForce;
            jumped = true;
        }

        if (!spacePressed && !onGround && motion.y > 0.0f)
        {
            gravity = fastFallGravity;
        }
        else
        {
            gravity = baseGravity;
        }

        rigidbody.velocity = motion;
    }
}
