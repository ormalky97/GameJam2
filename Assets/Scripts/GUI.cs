using System.Collections;
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

    [Header("Upkeep Texts")]
    public Text foodUK;
    public Text metalUK;
    public Text oilUK;

    //refs
    GameObject manager;
    Resources res;

    //Vars
    Color red = new Color(1, 0, 0, 1);
    Color green = new Color(0, 1, 0, 1);

    // Start is called before the first frame update
    void Awake()
    {
        manager = GameObject.Find("Game Manager");
        res = manager.GetComponent<Resources>();
    }

    void DisplayUpkeeps()
    {
        foodUK.text = " -" + res.foodDecay;
        metalUK.text = " -" + res.metalDecay;
        oilUK.text = " -" + res.oilDecay;
    }

    public void DisplayChanges(int f, int m, int o, int p)
    {
        SetChangeText(foodCahnge, f);
        SetChangeText(metalChange, m);
        SetChangeText(oilChange, o);
        SetChangeText(populationChange, p);
    }

    void SetChangeText(Text txt, int amount)
    {
        if (amount < 0)
        {
            txt.color = red;
            txt.text = "" + amount;
        }
        else if(amount > 0)
        {
            txt.color = green;
            txt.text = "+" + amount;
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
        if (txt.color.a > 0)
        {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - decayFactor * Time.deltaTime);
        }
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
        UpdateResources();
        DisplayUpkeeps();

        if (!DecayDone())
        {
            Decay(foodCahnge);
            Decay(oilChange);
            Decay(metalChange);
            Decay(populationChange);
        }
    }
}
