using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class slime : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float walkStopRate = 0.05f;


    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;


    Rigidbody2D rb;
    Animator animator;
    Damageable damageable;
    TouchingDirections touchingDirections;


    public enum WalkableDirection { Right , Left }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    
    public WalkableDirection WalkDirection
    {
        get 
        { 
            return _walkDirection;
        }
        set 
        {
            if(_walkDirection != value)
            {
                //Direction flipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                } else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value; 
        }
    }

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown {
        get 
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }
      void Awake()
    {
        touchingDirections = GetComponent<TouchingDirections>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
             HasTarget = attackZone.detectedColliders.Count > 0;   
             
             if(AttackCooldown > 0)
             {
                AttackCooldown -= Time.deltaTime;
             }
    }

    void FixedUpdate()
    {
      if(touchingDirections.IsGrounded && touchingDirections.IsOnWall )
        {
            FlipDirection();
        }

        if(!damageable.LockVelocity)
        {
            if(CanMove)    
                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            else //mathf so the enemy slows down when attacking
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }   

       private void FlipDirection()
        {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
   
    public void OnCliffDetetcted()
    {
        if(touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
  
}
