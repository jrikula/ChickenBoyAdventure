using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 1;

    Collider2D BiteCollider;

    void Start()
    {
        
    }

    void Awake()
    {
        BiteCollider = GetComponent<Collider2D>(); 
    }

    void Update()
    {
        
    }

    private void  OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        
        if(damageable != null)
        {
            damageable.Hit(attackDamage);
           // Vector2 delieveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            // Hit the target
           // bool gotHit = damageable.Hit(attackDamage, delieveredKnockback);

            //if(gotHit)
              //  Debug.Log(collision.name + " hit for " + attackDamage); 
        }  
    }
}
