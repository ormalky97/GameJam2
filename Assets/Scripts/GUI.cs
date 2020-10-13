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
    public Text oilChange;
    public Text metalChange;
    public Text populationChange;

    //refs
    GameObject manager;
    Resources res;
    Color red = new Color(1, 0, 0, 1);
    Color white = new Color(1, 1, 1, 1);

    // Start is called before the first frame update
    void Awake()
    {
        manager = GameObject.Find("Game Manager");
        res = manager.GetComponent<Resources>();
    }

    public void DisplayChanges(int f, int o, int m, int p)
    {

    }

    // Update is called once per frame
    void Update()
    {
        food.text = "  " + res.food;
        oil.text = "  " + res.oil;
        metal.text = "  " + res.metal;
        population.text = "  " + res.usedPopulation + "/" + res.population + " (" + (res.maxPopulation - res.population) +")";
    }
}
