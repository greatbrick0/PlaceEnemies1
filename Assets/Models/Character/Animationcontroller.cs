using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationcontroller : MonoBehaviour
{

    private Animator animator;
    private int blendValue;
    private int blendTreevalue;
    private bool bTreevalue = false;

    public string prefabName;
    [SerializeField]
    private bool parentIsMoving = false;
    private void Awake()
    {
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
    public void AbilityUsed(string abilityType = "default")
    {

        print(abilityType);

        switch (abilityType)
        {
            //fireball
            case "default":

                //dagger
                break;
            case "dgA":
                //claw 
                break;
            case "cgA":
                //tank
                break;
            case "agA":
                //potion
                break;
            case "pgA": animator.SetTrigger("potionA");
                //healer
                break;
            case "hgA":
                //chain
                break;
            case "jgA":
                //skeleton
                break;
            case "sgA":
                //archer
                break;
            case "rgA":

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
