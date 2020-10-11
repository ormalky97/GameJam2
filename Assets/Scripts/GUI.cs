using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public Text food;
    public Text money;
    public Text materials;
    public Text population;

    GameObject manager;
    Resources res;

    // Start is called before the first frame update
    void Awake()
    {
        manager = GameObject.Find("Game Manager");
        res = manager.GetComponent<Resources>();
    }

    // Update is called once per frame
    void Update()
    {
        food.text = "Food: " + res.food;
        money.text = "Money: " + res.money;
        materials.text = "Materials: " + res.materials;
        population.text = "Population: " + res.usedPopulation + "/" + res.population + " (" + (res.maxPopulation - res.population) +")";
    }
}
