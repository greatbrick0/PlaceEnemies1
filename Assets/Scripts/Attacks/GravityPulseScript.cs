using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPulseScript : Attack
{
    [SerializeField]
    [Tooltip("The reference to the effect appied on hit.")]
    public ForceMovement pullEffect;
    [SerializeField]
    [Tooltip("The reference to the effect appied to the user.")]
    public StatusEffect costEffect;

    [SerializeField]
    [Tooltip("The reference to the second part of this attack.")]
    public GameObject innerPulsePrefab;
    private GameObject innerPulseRef;
    private int attackStage = 0;

    protected override void Start()
    {
        base.Start();

        pullEffect = Instantiate(pullEffect);
    }

    protected override void Update()
    {
        base.Update();

        if (age >= lifetime * 0.75f && attackStage == 1)
        {
            attackStage = 2;
            innerPulseRef = Instantiate(innerPulsePrefab, transform.parent);
            innerPulseRef.GetComponent<Attack>().team = team;
            innerPulseRef.transform.position = transform.position;
        }
        if (age >= lifetime * 0.05f && attackStage == 0)
        {
            attackStage = 1;
            canHit = false;
        }
    }

    protected override bool FilterHitTarget(CombatBody hitTarget)
    {
        return hitTarget.team != this.team;
    }

    protected override void Apply(CombatBody recentHit)
    {
        Vector3 pullDirection = transform.position - recentHit.transform.position;
        pullEffect.forcedVelocity = (pullDirection  - (pullDirection.normalized / 2.0f)) / 2.0f;
        recentHit.Hurt(power);
        recentHit.AddStatusEffect(pullEffect);
    }
}
