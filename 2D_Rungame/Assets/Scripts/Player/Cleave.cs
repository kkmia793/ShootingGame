using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleave : MonoBehaviour
{
    public GameObject CleaveEffect;
    private Rigidbody rb;
    private Transform myTransform;

    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myTransform = transform;
        boxCollider = GetComponent<BoxCollider>();

        CleaveEffect = Instantiate(CleaveEffect, myTransform.position, myTransform.rotation) as GameObject;
        CleaveEffect.transform.parent = myTransform;

    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsDirection();
        offScreen();

    }

    private void RotateTowardsDirection()
    {
        if (rb.velocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized, Vector3.up);
            float angle = Vector3.Angle(myTransform.forward, rb.velocity.normalized);
            float lerpFactor = angle * Time.deltaTime; // Use the angle as the interpolation factor
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, lerpFactor);
        }
    }

    private void offScreen()
    {
        if (this.transform.position.x > 20.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
