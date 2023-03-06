using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPanelSlotScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> panels = new List<GameObject>();
    [SerializeField]
    private int maxUses = 2;
    private int timesUsed = 0;

    public void ReplacePanel(int previousPanelIndex)
    {
        timesUsed++;
        if (timesUsed >= maxUses) return;

        int randomPanel = ChooseRandomInt(previousPanelIndex);
        Instantiate(panels[randomPanel], this.transform).GetComponent<EnemyPanelScript>().panelTypeIndex = randomPanel;
    }

    private void Start()
    {
        maxUses = panels.Count == 1 ? 1 : maxUses;

        if (transform.childCount > 0) return;

        int randomPanel = ChooseRandomInt();
        Instantiate(panels[randomPanel], this.transform).GetComponent<EnemyPanelScript>().panelTypeIndex = randomPanel;
    }

    private int ChooseRandomInt(int excluded = -1)
    {
        int output = 0; 
        
        if(excluded != -1)
        {
            output = Random.Range(0, panels.Count - 1);
            if (output == excluded) output = panels.Count - 1;
        }
        else
        {
            output = Random.Range(0, panels.Count);
        }

        print("output: " + output + " excluded: " + excluded);
        return output;
    }
}
