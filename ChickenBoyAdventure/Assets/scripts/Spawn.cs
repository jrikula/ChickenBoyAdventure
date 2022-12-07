using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject objToSpawn;
    public Transform platformPosition;

    TouchingDirections touchingDirections;

    // Start is called before the first frame update
    private void Awake()
    {
        touchingDirections = GetComponent<TouchingDirections>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && !touchingDirections.IsGrounded)
        {
            Instantiate(objToSpawn, platformPosition.position, platformPosition.rotation);
        }
    }
}
