using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    [Header("Resources Texts")]
    public Text food;
    public Text oil;
    public Text metal;
    public Text population;

    [Header("Changes Texts")]
    public Text foodCahnge;
    public Text metalChange;
    public Text oilChange;
    public Text populationChange;
    public float decayFactor;

    //refs
    GameObject manager;
    Resources res;

    //Vars
    Color red = new Color(1, 0, 0, 1);
    Color green = new Color(0, 1, 0, 1);
    Color white = new Color(1, 1, 1, 1);

    // Start is called before the first frame update
    void Awake()
    {
        manager = GameObject.Find("Game Manager");
        res = manager.GetComponent<Resources>();
    }

    public void DisplayChanges(int f, int m, int o, int p)
    {
        SetChangeText(foodCahnge, f);
        SetChangeText(metalChange, m);
        SetChangeText(oilChange, o);
        SetChangeText(populationChange, p);

        StartCoroutine("ChangeDecay");
    }

    void SetChangeText(Text txt, int amount)
    {
        if (amount < 0)
        {
            txt.color = red;
            txt.text = "" + amount;
        }
        else
        {
            txt.color = green;
            txt.text = "+" + amount;
        } 
    }

    IEnumerator ChangeDecay()
    {
        yield return new WaitForSeconds(1f);

        while(!DecayDone())
        {
            Decay(foodCahnge);
            Decay(oilChange);
            Decay(metalChange);
            Decay(populationChange);
        }
    }

    bool DecayDone()
    {
        if (foodCahnge.color.a <= 0 && oilChange.color.a <= 0 && metalChange.color.a <= 0 && populationChange.color.a <= 0)
            return true;
        else
            return false;
    }

    void Decay(Text txt)
    {
        if(txt.color.a > 0)
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - decayFactor);
    }

    void UpdateResources()
    {
        food.text = "  " + res.food;
        oil.text = "  " + res.oil;
        metal.text = "  " + res.metal;
        population.text = "  " + res.usedPopulation + "/" + res.population + " (" + (res.maxPopulation - res.population) + ")";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
