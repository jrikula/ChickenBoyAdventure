using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject original;
    public Transform platformPosition;

    TouchingDirections touchingDirections;

    void Awake()
    {
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !touchingDirections.IsGrounded)
        {
            GameObject clone = (GameObject)Instantiate(original, platformPosition.position, platformPosition.rotation);
            if (!touchingDirections.IsGrounded)
            {
                Destroy(clone, 5f);
            }
        }
    }
}