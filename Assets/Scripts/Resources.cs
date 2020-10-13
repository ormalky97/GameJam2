using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    [Header("Resources")]
    public int food;
    public int oil;
    public int metal;

    [Header("Population")]
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
            yield return new WaitForSeconds(60f);
            food -= (population - usedPopulation) * 5 + usedPopulation * 10;
        }
    }

    IEnumerator Populate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(60, 90));
            if (population < maxPopulation)
            {
                 int trpc = (food + metal + oil)/15; //Total Resources Per Colonist
                 int maxColonyGrowth = maxPopulation - population;
                 int newColonists;

                 if (population < trpc) //pop+
                 {
                     if ((trpc - population) >= (maxColonyGrowth))
                         newColonists = maxColonyGrowth;
                     else
                         newColonists = trpc - population;


                    population += Random.Range(Mathf.RoundToInt(1 + newColonists / 3), Mathf.RoundToInt(2 + newColonists * 3 / 4));
                    Debug.Log("New Colonists Arrived");
                    Debug.Log(trpc);
                    Debug.Log(newColonists);
                 }
                 //elif (popConsumption > totalRes) //pop-
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
