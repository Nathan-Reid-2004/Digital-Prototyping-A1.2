using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public Vector2 acceleration = Vector2.zero;
    public float jumpHeight = 200;
    public float movementInfluenceFactor = 0.12f;

    public float lowJumpMultiplier = 1.5f; // scalar for tap and hold
    public float fallMultiplier = 2.5f; // speed up on descent

    // Coyote time/ledge forgiveness
    public float coyoteTime;
    public float coyoteTimeMax = 0.2f;

    // Length of the ground check.
    public float groundCheckLength = 1.25f;

    public LayerMask groundLayer; // Specifying which layer to check collisions on

    private bool isGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coyoteTime = coyoteTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = JumpRegister();

        if (isGrounded)
        {
            // print("yeah true");
            coyoteTime = coyoteTimeMax;
        }
        else
        {
            coyoteTime -= Time.deltaTime;
        }

        HandleMovement();
        HandleJump();
    }
    public void HandleMovement()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x + (Input.GetAxis("Horizontal") * movementInfluenceFactor),
        rb2d.velocity.y);

        if (rb2d.velocity.y > 0 && Input.GetKey(KeyCode.S)) // holding s?
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y / 8);
        }
        else if (rb2d.velocity.y < 0 && Input.GetKey(KeyCode.S)) // holding s and starting to fall?
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y - 0.025f);
        }

    }

    public void HandleJump()
    {
        if (coyoteTime > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            // Jump using the Rigidbody velocity
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
        }

        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += (Vector2.up * Physics2D.gravity.y *
                (fallMultiplier * Time.deltaTime));
        }
        // ascending and space is not being held
        else if (rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            // if not pressing key, apply lowjump mult
            rb2d.velocity += (Vector2.up * Physics2D.gravity.y *
                (lowJumpMultiplier * Time.deltaTime));
        }
    }

    bool JumpRegister()
    {
        bool check = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, groundLayer); 
        // using a raycast to determine the players distance away from the ground using starting point, direction, size, and layer respectively
        return check;
    }
}
