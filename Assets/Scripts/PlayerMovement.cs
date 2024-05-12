using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Animator animator; 
    private Vector2 velocity = Vector2.zero;
    public float moveSpeed = 5f;
    public float jumpVelocity = 5f;
    [Range(0, 1f)] public float airFriction = 0.1f; // Adjust as needed
    private bool isJumping = false;
    private bool isJumpHeld = false;
    public bool isGrounded = false;
    private int moveIterations = 3;
    private float normalBias = 0.01f;
    [Range(0, 5f)] public float fallLongMult = 0.85f;
    [Range(0, 5f)] public float fallShortMult = 1.55f;
    public static PlayerMovement instance;
    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float groundRayDistance = 0.1f;

    public bool isUpsideDown;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    




    void Update()
    {
    // Read input
        animator.SetBool("IsGrounded", IsGrounded());
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
    
    // Check if the player is colliding with a wall
        

    
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
    
    // Calculate movement velocity
        velocity = new Vector2(horizontalInput * moveSpeed, rb2d.velocity.y);
    
    // Check if grounded
        isGrounded = IsGrounded();

        

    // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            isJumpHeld = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVelocity);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            isJumpHeld = false;
        }

    // Apply air friction
        if (!isGrounded)
        {
            
            ApplyAirFriction();
        }
    }

    void FixedUpdate()
    {
    // Apply movement and handle collisions
        Vector2 remainingDelta = velocity * Time.fixedDeltaTime;
        int iter = 0;

        while (remainingDelta.sqrMagnitude > 0 && iter++ < moveIterations)
        {
            CastAndMove(velocity, remainingDelta, out velocity, out remainingDelta);
        }

    // Apply gravity for jumping
        if (isJumping && rb2d.velocity.y > 0)
        {
            if (isJumpHeld)
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallLongMult - 1) * Time.fixedDeltaTime;
            }
            else
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallShortMult - 1) * Time.fixedDeltaTime;
            }
        }
        else if (rb2d.velocity.y <= 0)
        {
            isJumping = false;
        }

    }
    

    void ApplyAirFriction()
    {
        velocity.x *= (1 - airFriction);
    }

    bool IsGrounded()
    {
        Vector2 Vector2Direction = Vector2.down;
        if (isUpsideDown == true)
        {
            Vector2Direction = Vector2.up;
        }
        else
        {
            Vector2Direction = Vector2.down;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0f, 1f, 0f), Vector2Direction, groundRayDistance, groundLayer);
        Debug.DrawRay(transform.position - new Vector3(0f, 1f, 0f), Vector2.down * groundRayDistance, Color.red);
        return hit.collider != null;
    }
    
    bool CastAndMove(Vector2 velocity, Vector2 remainingDelta, out Vector2 newVelocity, out Vector2 newRemaining)
    {
        Vector3 opos = transform.position;

        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hitCount = GetComponent<Collider2D>().Cast(remainingDelta, hits, remainingDelta.magnitude);

        if (hitCount == 0)
        {
            transform.Translate(remainingDelta);
            newVelocity = velocity;
            newRemaining = Vector2.zero;
            return false;
        }
        else
        {
            RaycastHit2D hit = hits[0];
            transform.position = hit.centroid + normalBias * hit.normal;
            newRemaining = remainingDelta + (Vector2)(opos - transform.position);
            newVelocity = velocity - (Vector2.Dot(velocity, hit.normal) * hit.normal);
            newRemaining = newRemaining - Vector2.Dot(newRemaining, hit.normal) * hit.normal;
            return true;
        }
    }
}