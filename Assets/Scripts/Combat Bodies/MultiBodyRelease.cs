using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBodyRelease : Placeable
{
    [SerializeField]
    List<CombatBody> releaseTargets;

    private void ReleaseTargets()
    {
        for(int ii = 0; ii < releaseTargets.Count; ii++)
        {
            releaseTargets[ii].transform.parent = transform.parent;
            releaseTargets[ii].Release();
        }
    }

    public override void Release()
    {
        ReleaseTargets();
        Destroy(this.gameObject);
    }
}
