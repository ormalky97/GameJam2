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
        StartCoroutine("MetalDecay");
        StartCoroutine("OilDecay");
    }

    IEnumerator FoodDecay()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            int foodDecay = (population - usedPopulation) * 5 + usedPopulation * 10;
            if (food < foodDecay)
            {
                DecreaseResources(0, 0, 0, 0, (food-foodDecay)/10);
                DecreaseResources(food, 0, 0, 0, 0);
            }
            else
                DecreaseResources(foodDecay, 0, 0, 0, 0);
        }
    }

    IEnumerator MetalDecay()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            int turrets = 0;
            foreach (GameObject building in FindObjectOfType<BuildingsManager>().buildings)
            {
                if (building.tag == "Turret")
                    turrets++;
            }
            int metalDecay = 60 * turrets;
            if (metalDecay > metal)
                DecreaseResources(0, 0, metal, 0, 0);
            else
                DecreaseResources(0, 0, metalDecay, 0, 0);
        }
    }

    IEnumerator OilDecay()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            int stations = 0;
            foreach (GameObject building in FindObjectOfType<BuildingsManager>().buildings)
            {
                if (building.tag == "Station")
                    stations++;
            }
            int oilDecay = 60 * stations;
            if (oilDecay > oil)
                DecreaseResources(0, oil, 0 , 0, 0);
            else
                DecreaseResources(0, oilDecay, 0 , 0, 0);
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


                    DecreaseResources(0, 0, 0, 0, Random.Range(Mathf.RoundToInt(1 + newColonists / 3), Mathf.RoundToInt(2 + newColonists * 3 / 4)));
                    Debug.Log("New Colonists Arrived");
                 }
                 //elif (popConsumption > totalRes) //pop-
            }
        }

    }
    public void DecreaseResources(int foodCost, int oilCost, int metalCost, int populationCost, int populationDiff)
    {
        food -= foodCost;
        oil -= oilCost;
        metal -= metalCost;
        usedPopulation += populationCost;
        population += populationDiff;
    }
}
