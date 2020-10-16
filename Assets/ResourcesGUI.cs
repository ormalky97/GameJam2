using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesGUI : MonoBehaviour
{
    [Header("Resources Texts")]
    public Text population;

    [Header("Changes Texts")]
    public Text populationChange;
    public float decayFactor;



    public void DisplayChange(Resource res)
    {
        if (res.amount < 0)
        {
            res.changeGUI.color = Color.red;
            res.changeGUI.text = "" + res.amount;
        }
        else if (res.amount > 0)
        {
            res.changeGUI.color = Color.green;
            res.changeGUI.text = "+" + res.amount;
        }
    }

    public void UpdateResourcesUI(Population pop)
    {
        pop.displayGUI.text = "  " + pop.usedPopulation + "/" + pop.population + " (" + pop.Unused() + ")";
    }

    public void UpdateResourcesUI(Resource res)
    {
        res.displayGUI.text = "  " + res.amount;
    }
    public void UpdateChangesUI(Population pop)
    {
        if (pop.changeGUI.color.a > 0)
            Decay(pop.changeGUI);
    }

    public void UpdateChangesUI(Resource res)
    {
        if (res.changeGUI.color.a > 0)
            Decay(res.changeGUI);
    }

    void Decay(Text txt)
    {
        float alpha = txt.color.a - decayFactor * Time.deltaTime;
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, alpha);
    }
}
