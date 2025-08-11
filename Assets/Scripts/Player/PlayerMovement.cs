using System;
using Unity.VisualScripting;
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

    private Stats stats;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats = GetComponent<Stats>();
    }

    float coyoteTime = 0.2f;
    float coyoteCounter;

    float jumpBufferTime = 0.2f;
    float jumpBufferCounter;

    void Update()
    {
        if (stats.CurrentMana < stats.MaxMana)
        {
            stats.RegenerateMana();
        }
        
        if (RuleManager.Instance.IsMasterOfRules())
        {
            GameManager.Instance.Victory();
        }
        
        
        Move = Input.GetAxis("Horizontal");
        
        rb.velocity = new Vector2(Move * moveSpeed * customTimeScale, rb.velocity.y);
        
        if (Move > 0)
            spriteRenderer.flipX = false;
        else if (Move < 0)
            spriteRenderer.flipX = true;
        
        if (isGrounded())
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime * customTimeScale;
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpBufferCounter = jumpBufferTime;
            Music.Instance.PlaySFX("Jump",0.7f);
        }
        else
            jumpBufferCounter -= Time.deltaTime * customTimeScale;
        
        if (jumpBufferCounter > 0 && coyoteCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump); 
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
        rb.velocity = Vector2.zero; 
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

}