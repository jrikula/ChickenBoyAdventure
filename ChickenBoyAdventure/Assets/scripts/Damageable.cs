using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    Animator animator;



    [SerializeField]
    private int _maxHealth = 5;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 5;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            // If health drops below 0, character is no longer alive
            if(_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;

   

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;


    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("isAlive set " + value);
        }
    }    
    // the velocity should not change while this is true but needs to be repected by other pysics components like
    // the player controller
    public bool LockVelocity {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set 
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                // Remove invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
        //Hit(10);
    }

    // returns wheter the damageable took damage or not
    public bool Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            //Notify other subscribe components that the damageable was hit to handle the knockback and such
            
           //? check if it is null or not
           
           animator.SetTrigger(AnimationStrings.hit);
           LockVelocity = true;
           damageableHit?.Invoke(damage, knockback);
           

            return true;
        }
        // Unable to hit
        return false;
    }
}
