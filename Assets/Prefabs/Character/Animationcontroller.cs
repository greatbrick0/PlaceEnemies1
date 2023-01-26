using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationcontroller : MonoBehaviour
{
    private Animator animator;
    private int blendValue;

    void Start()
    {
        animator = GetComponent<Animator>();
        blendValue = Animator.StringToHash("Blend");
  
    }

    void Update()
    {
        animator.SetFloat(blendValue, Input.GetKey(KeyCode.W) ? 1 : 0);
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Trigger2");
        }
    }
}
