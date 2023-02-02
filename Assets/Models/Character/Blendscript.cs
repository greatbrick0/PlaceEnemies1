using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blendscript : MonoBehaviour
{
    private int blendValue;
    private float currentBlend;
    private float targetBlend;
    private float blendVelocity;
    private Animator animator2;
    void Start()
    {
        animator2 = GetComponent<Animator>();
        blendValue = Animator.StringToHash("Blend");
        currentBlend = 0;
        targetBlend = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            targetBlend = 1;
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            targetBlend = -1;
        }
        else
        {
            targetBlend = 0;
        }

        currentBlend = Mathf.SmoothDamp(currentBlend, targetBlend, ref blendVelocity, 0.1f);
        animator2.SetFloat(blendValue, currentBlend);
    }
}
