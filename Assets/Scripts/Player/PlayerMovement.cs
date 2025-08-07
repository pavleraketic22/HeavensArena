using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float customTimeScale = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public float jump;
    public float Move;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    bool grounded;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
/*
    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(Move * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump * 10));
        }
    }
*/
    float coyoteTime = 0.2f;
    float coyoteCounter;

    float jumpBufferTime = 0.2f;
    float jumpBufferCounter;

    void Update()
    {
        if (RuleManager.Instance.IsMasterOfRules())
        {
            // neki ui
        }
        
        
        Move = Input.GetAxis("Horizontal");
        
        rb.velocity = new Vector2(Move * moveSpeed * customTimeScale, rb.velocity.y);
        
        //Flip za promenu smera gledanja
        if (Move > 0)
            spriteRenderer.flipX = false;
        else if (Move < 0)
            spriteRenderer.flipX = true;

        // Coyote time
        if (isGrounded())
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime * customTimeScale;

        // Jump buffer
        if (Input.GetKeyDown(KeyCode.UpArrow))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime * customTimeScale;

        // Jump
        if (jumpBufferCounter > 0 && coyoteCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump); // jump bolje sa velocity umesto AddForce
            jumpBufferCounter = 0f;
        }
    }



    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }
    
    
    public void Knockback(Vector2 direction, float force)
    {
        rb.velocity = Vector2.zero; // resetujemo trenutnu brzinu
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

}