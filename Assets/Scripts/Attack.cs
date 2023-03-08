using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Attack : MonoBehaviour
{
    [HideInInspector]
    public string team = "enemy";
    [SerializeField]
    [Tooltip("The units per second the projectile will travel at. One hexagon tile is 4 units in diameter.")]
    public float speed = 10.0f;
    [HideInInspector]
    public Vector3 moveDirection = Vector3.forward;
    [SerializeField]
    [Tooltip("The amount of seconds the projectile will live before destroying itself.")]
    public float lifetime = 20.0f;
    protected float age = 0.0f;
    [SerializeField]
    [Min(0)]
    [Tooltip("The amount of damage, healing applied, or any other primary stat.")]
    public int power = 10;

    public bool hasParticles = false;

    protected Collider hitbox;
    protected List<CombatBody> hitList = new List<CombatBody>();
    [HideInInspector]
    public bool canHit = true;

    protected virtual void Start()
    {
        hitbox = GetComponent<Collider>();
        hitbox.isTrigger = true;
    }

    protected virtual void Update()
    {
        Age();
        transform.position += moveDirection.normalized * speed * Time.deltaTime;
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

    protected virtual void CompleteAttack() //for when an attack has been used up. ex: a bullet disappears after damaging one enemy.
    {
        canHit = false;
        if (hasParticles == true)
        {
            ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.transform.parent = null;
                ps.Stop();
                Destroy(ps.gameObject, 2.0f);
            }
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!canHit) return;
        if (other.gameObject.GetComponent<CombatBody>() == null) return;
        if (other.isTrigger) return;

        CombatBody otherCombatBody = other.gameObject.GetComponent<CombatBody>();
        if (hitList.Contains(otherCombatBody)) return;

        if (FilterHitTarget(otherCombatBody))
        {
            hitList.Add(otherCombatBody);
            Apply(otherCombatBody);
        }
    }

    protected abstract bool FilterHitTarget(CombatBody hitTarget);

    protected abstract void Apply(CombatBody recentHit); //used to apply damage, healing, or other effects to the targets

    public void FaceForward()
    {
        transform.LookAt(transform.position + moveDirection);
    }

}
