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

    public CombatUISpellIDLister _spellListForIcon;

    public void UpdateResource(int increaseAmount)
    {
        
        //_bountyCollectedValue.AddResource(increaseAmount);
    }
    public void UpdateScore(int increaseAmount)
    {
        //_scoreValue.AddScore(increaseAmount);
    }
    public void TakeDamage()
    {
        //_healthUI.RecieveDamage();
    }

    public void HealthOverride(int NewHealthTotal)
    {
        //_healthUI.SetHealth = NewHealthTotal;
    }
    public void ConnectSpellList(List<Ability> PlayerSpellList)
    {
        for (int i = 0; i < PlayerSpellList.Count; i++) 
        {  
            _abilities[i].GetComponent<CombatUiIconCD>().ChangeSpellIcon(_spellListForIcon.GetSpellIcon(PlayerSpellList[i].ID));
            _abilities[i].GetComponent<CombatUiIconCD>().CooldownTimeManipulate(PlayerSpellList[i].cooldownTime);
        }
        
    }

    public void TriggerAbility(int abilityFromList)
    {
        _abilities[abilityFromList].GetComponent<CombatUiIconCD>().UseSpell();
    }
}
