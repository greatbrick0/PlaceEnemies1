using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   

 

  

    private Vector3 moveDirection;
    Rigidbody cc;
    private Animator animator;
  
    void Start()
    {

        animator = GetComponent<Animator>();
        
        cc = GetComponent<Rigidbody>();
   

    }

    void Update()
    {
        // Retrieve the rigidbody's global velocity
        Vector3 velocity = cc.velocity;

        // Set xSpeed and ySpeed based on the global velocity components
        float xSpeed = velocity.x;
        float ySpeed = velocity.z;

        // Set the x and y speed parameters in the animator
        animator.SetFloat("XSpeed", xSpeed);
        animator.SetFloat("YSpeed", ySpeed);
    }
   






}
