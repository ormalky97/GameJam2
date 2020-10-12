using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public int food;
    public int oil;
    public int metal;
    public int population;
    public int usedPopulation;
    public int maxPopulation;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Populate");
        StartCoroutine("FoodDecay");
    }

    IEnumerator FoodDecay()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);
            food -= (population - usedPopulation) * 5 + usedPopulation * 10;
        }
    }

    IEnumerator Populate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(60, 180));
            if(population < maxPopulation)
            {
                population += Random.Range(0, maxPopulation - population);
                Debug.Log("New Colonists Arrived");
            }
        }
    }


    public void DecreaseResources(int foodCost, int oilCost, int metalCost, int populationCost)
    {
        food -= foodCost;
        oil -= oilCost;
        metal -= metalCost;
        usedPopulation += populationCost;
    }
}
