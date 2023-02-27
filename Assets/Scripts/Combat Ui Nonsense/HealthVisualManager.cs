using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisualManager : MonoBehaviour
{
    [SerializeField]
    private List<Image> shards;
    private Image shield;

    private int health;
    private bool shielded;

    private void ResetHealthVisual()
    {
        foreach (Image s in shards)
        {
            s.transform.position = Vector3.zero;
            s.GetComponent<Animator>().SetBool("IsPrio", false);
        }
    }


    public void SetHealth(int x)
    {
        health = x;

        UpdateHealthVisual();
    }
    public void TakeDamage()
    {
        health--;
        UpdateHealthVisual();
    }

    public void TakeDamage(int x) //overload for if player needs to take more then 1, or a scuffed way to heal.
    {
        health -= x;
        UpdateHealthVisual();
    }

    private void UpdateHealthVisual()
    {
        if (health == 0)
        {
            //Run death animation on the last crystal.
            return;
        }
        for (int ii = 0; ii < shards.Count;ii++)
        {
            shards[ii].gameObject.SetActive(ii < health);
            if (ii == health-1)
            {
                shards[ii].gameObject.GetComponent<Animator>().SetBool("IsPrio",true);
            }
  
        }
    }
    private void RecieveShield()
    {
        if (!shielded) //can remove if we are allowing shields to stack.
        {
            shielded = true;
            shield.gameObject.SetActive(true); //no impact yet, dont have shield sprite.
            
        }
    }

    
}
