using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public int food;
    public int money;
    public int materials;
    public int population;
    public int maxPopulation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DecreaseResources(int foodCost, int moneyCost, int materialCost, int populationCost)
    {
        food -= foodCost;
        money -= moneyCost;
        materials -= materialCost;
        population += populationCost;
    }
}
