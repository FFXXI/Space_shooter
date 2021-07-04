using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{

    public float tumble;
    private Rigidbody rb;


    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(1, 1, 1) * tumble;

        rb.angularVelocity = Random.insideUnitSphere * tumble;

    }

}
