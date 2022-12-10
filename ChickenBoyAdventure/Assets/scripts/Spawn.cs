using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject original;
    public Transform platformPosition;

    Animator anim;
    TouchingDirections touchingDirections;

    void Awake()
    {
        touchingDirections = GetComponent<TouchingDirections>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !touchingDirections.IsGrounded)
        {
            GameObject clone = (GameObject)Instantiate(original, platformPosition.position, platformPosition.rotation);
            anim.Play("egg_crack", 0, 0.0f);
            if (!touchingDirections.IsGrounded)
            {
                Destroy(clone, 5f);
            }
        }
    }
}