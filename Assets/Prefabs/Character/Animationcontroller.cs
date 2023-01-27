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
        

        Vector3 localVelocity = transform.InverseTransformDirection(transform.parent.GetComponent<Rigidbody>().velocity);
        //Converts global velocity vector to a local vector
        float forawrdVelocity = localVelocity.z;
        //Players total local forward velocity = forwardVelocity, can be used for dash animations or being slowed
        animator.SetFloat(blendValue, forawrdVelocity >= 0.1f ? 1 : 0);
        //if forward velocity is greater than or equal to 0.1f, blendValue is set to 1, else its set to 0

    }

    //called from the parent CombatBody when an ability is successfully used
    //abilityType can be used later for more types of animations (heavy attack, light attack, dash)
    //this function is not called if the ability is used while on cooldown, or fails for some other reason
    public void AbilityUsed(int abilityType=0) 
    {
        animator.SetTrigger("Trigger2");
    }
}
