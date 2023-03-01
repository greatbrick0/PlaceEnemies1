using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Animator animator;
    private Rigidbody cc;
    private float currentXSpeed;
    private float currentYSpeed;
    public float turnSpeed = 0.1f;
    public float speedSmoothTime = 0.1f;
    public float speedSmoothFactor = 2f;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        cc = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get the local velocity
        Vector3 localVelocity = transform.InverseTransformDirection(cc.velocity);

        // Set xSpeed and ySpeed based on the local velocity components
        float xSpeed = localVelocity.x;
        float ySpeed = localVelocity.z;

        // Smoothly adjust the speed based on the rotation of the model
        float angle = Vector3.Angle(Vector3.forward, transform.forward);
        if (Vector3.Dot(transform.right, Vector3.forward) < 0)
        {
            angle = 360 - angle;
        }
        float adjustedSpeed = Mathf.Lerp(currentYSpeed, currentXSpeed, angle / 90f);
        currentYSpeed = Mathf.Lerp(currentYSpeed, ySpeed, Time.deltaTime * speedSmoothFactor / speedSmoothTime);
        currentXSpeed = Mathf.Lerp(currentXSpeed, xSpeed, Time.deltaTime * speedSmoothFactor / speedSmoothTime);

        // Set the x and y speed parameters in the animator
        animator.SetFloat("XSpeed", xSpeed);
        animator.SetFloat("YSpeed", ySpeed);
    }
}
