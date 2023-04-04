using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationcontroller : MonoBehaviour
{

    private Animator animator;
    private int blendValue;
    private int blendTreevalue;
    private bool bTreevalue = false;
    private Rigidbody dd;
    public string prefabName;
    [SerializeField]
    private bool parentIsMoving = false;
    private void Awake()
    {
        dd = FindObjectOfType<Rigidbody>();
        animator = GetComponent<Animator>();
        blendValue = Animator.StringToHash("Blend");
        blendTreevalue = Animator.StringToHash("blendTreevalue");
        bTreevalue = animator.GetBool("bTreevalue");
        switch (prefabName)
        {

            case "Fireballdemon":
                animator.SetInteger(blendTreevalue, 2);

                break;
            case "Daggerdemon":
                animator.SetInteger(blendTreevalue, 1);
                break;
            case "Clawdemon":
                animator.SetInteger(blendTreevalue, 5);
                break;
            case "Chunkydemon":
                animator.SetInteger(blendTreevalue, 9);
                break;
            case "Potiondemon":
                animator.SetInteger(blendTreevalue, 6);
                break;
            case "Healerdemon":
                animator.SetInteger(blendTreevalue, 4);
                break;
            case "Chaindemon":
                animator.SetInteger(blendTreevalue, 8);
                break;
            case "Skeledemon":
                animator.SetInteger(blendTreevalue, 3);
                break;
            case "Archerdemon":
                animator.SetInteger(blendTreevalue, 7);
                break;
            default:

                break;
        }
        //switch statement to set the default blend tree per prefab
    }
    void Start()
    {

    }

    void Update()
    {
        parentIsMoving = transform.parent.GetComponent<Rigidbody>().velocity.magnitude >= 0.1f;
        Vector3 localVelocity = Vector3.zero;
        if (transform.parent != null)
        {
           localVelocity = transform.parent.TransformDirection(dd.velocity);
        }
        float xSpeed1 = localVelocity.x;
        float ySpeed1 = localVelocity.z;

        animator.SetFloat("XSpeed", xSpeed1);
        animator.SetFloat("YSpeed", ySpeed1);
        //Converts global velocity vector to a local vector
        float forawrdVelocity = localVelocity.z;
        //Players total local forward velocity = forwardVelocity, can be used for dash animations or being slowed
        
        //if forward velocity is greater than or equal to 0.1f, blendValue is set to 1, else its set to 0

    }

    //called from the parent CombatBody when an ability is successfully used
    //abilityType can be used later for more types of animations (heavy attack, light attack, dash)
    //this function is not called if the ability is used while on cooldown, or fails for some other reason
    public void AbilityUsed(string abilityType = "default")
    {

        print(abilityType);

        switch (abilityType)
        {
            //fireball
            case "default":

                //dagger
                break;
            case "dgA": animator.SetTrigger("daggerA");
                //claw 
                break;
            case "cgA":
                animator.SetTrigger("clawA");
                //tank
                break;
            case "agA":
                animator.SetTrigger("chunkyA");
                //potion
                break;
            case "pgA": animator.SetTrigger("potionA");
                //healer
                break;
            case "hgA":
                animator.SetTrigger("healerA");
                //chain
                break;
            case "jgA":
                animator.SetTrigger("chainA");
                //skeleton
                break;
            case "sgA":
                animator.SetTrigger("bombA");
                //archer
                break;
            case "rgA":
                animator.SetTrigger("archerA");
                break;
            case "fgA":
                Debug.Log("Itworks:)");
                animator.SetTrigger("fireballA");
                break;
            default:
                break;
        }

    }


}
