using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationcontroller : MonoBehaviour
{
    private Animator animator;
    private int blendValue;

    [SerializeField]
    private bool parentIsMoving = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        blendValue = Animator.StringToHash("Blend");
  
    }

    void Update()
    {
        parentIsMoving = transform.parent.GetComponent<Rigidbody>().velocity.magnitude >= 0.1f;

        animator.SetFloat(blendValue, Input.GetKey(KeyCode.W) ? 1 : 0);
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Trigger2");
        }
    }

    //called from the parent CombatBody when an ability is successfully used
    //abilityType can be used later for more types of animations (heavy attack, light attack, dash)
    //this function is not called if the ability is used while on cooldown, or fails for some other reason
    public void AbilityUsed(int abilityType=0) 
    {

    }
}
