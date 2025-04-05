using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from the obstacle.");
        }
        //transform.rotation = Quaternion.Euler(-90f, -90f, 0f);
    }

    // FixedUpdate is called at a fixed interval
    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.MovePosition(transform.position + Vector3.right * speed * Time.fixedDeltaTime);
        }
    }
}
