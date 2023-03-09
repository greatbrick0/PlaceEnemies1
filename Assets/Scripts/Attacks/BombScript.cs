using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : Attack
{
    [SerializeField]
    [Tooltip("The game object that will be instatiated when this attack is completed. ")]
    private GameObject residueObject;
    private GameObject residueRef;
    [SerializeField]
    [Tooltip("Whether the instanced game object should be randomly rotated or always face forward. ")]
    private bool randomRotation = true;

    protected override bool FilterHitTarget(CombatBody hitTarget)
    {
        return hitTarget.team != this.team;
    }

    protected override void Apply(CombatBody recentHit)
    {
        CompleteAttack();
    }

    protected override void CompleteAttack()
    {
        SpawnResidue();

        base.CompleteAttack();
    }

    protected override void TooOld()
    {
        SpawnResidue();

        base.TooOld();
    }

    private void SpawnResidue()
    {
        residueRef = Instantiate(residueObject, transform.parent);
        residueRef.transform.position = this.transform.position;
        if (randomRotation) residueRef.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        residueRef.GetComponent<Attack>().team = this.team;
    }
}
