using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Attack : MonoBehaviour
{
    [SerializeField]
    public string team = "enemy";
    [SerializeField]
    public float speed = 10.0f;
    public Vector3 moveDirection = Vector3.forward;
    [SerializeField]
    public float lifetime = 20.0f;
    protected float age = 0.0f;
    [SerializeField]
    public int power = 10; //used as the amount of damage to deal, healing applied, or any other primary stat

    protected Collider hitbox;
    protected List<CombatBody> hitList = new List<CombatBody>();

    protected virtual void Start()
    {
        hitbox = GetComponent<Collider>();
        hitbox.isTrigger = true;
    }

    protected virtual void Update()
    {
        Age();
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    protected void Age()
    {
        age += 1.0f * Time.deltaTime;
        if (age >= lifetime)
        {
            TooOld();
        }
    }

    protected virtual void TooOld() //for when an attack has existed for too much time.
    {
        Destroy(this.gameObject);
    }

    protected void CompleteAttack() //for when an attack has been used up. ex: a bullet disappears after damaging one enemy.
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<CombatBody>() != null)
        {
            if (FilterHitTarget(other.gameObject.GetComponent<CombatBody>()))
            {
                hitList.Add(other.gameObject.GetComponent<CombatBody>());
                Apply();
            }
        }
    }

    protected abstract bool FilterHitTarget(CombatBody hitTarget);

    protected abstract void Apply(); //used to apply damage, healing, or other effects to the targets

}
