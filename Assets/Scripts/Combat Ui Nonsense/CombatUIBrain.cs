using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUIBrain : MonoBehaviour
{

    [SerializeField]
    protected List<GameObject> _abilities;

    [SerializeField]
    protected GameObject _healthUI;

    [SerializeField]
    protected GameObject _bountyCollectedValue;

    [SerializeField]
    protected GameObject _scoreValue;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerAbility(int abilityFromList)
    {
        _abilities[abilityFromList].GetComponent<CombatUiIconCD>().UseSpell();
    }
}
