﻿using System.Collections;
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
   
    [Header("Audio Clips")]
    public AudioClip noOil;
    public AudioClip noMetal;
    public AudioClip arrival;

    [Header("Decays")]
    public int foodDecay;
    public int metalDecay;
    public int oilDecay;


    GUI gui;
    Messages messages;
    bool oilZero = false;
    AudioSource audioSource;

    void Awake()
    {
        gui = FindObjectOfType<GUI>();
        messages = FindObjectOfType<Messages>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine("Populate");
        StartCoroutine("FoodDecay");
        StartCoroutine("MetalDecay");
        StartCoroutine("OilDecay");
    }

    void FixMinus()
    {
        if (food < 0)
            food = 0;

        if (oil < 0)
            oil = 0;

        if (metal < 0)
            metal = 0;
    }

    void NoOil()
    {
        oilZero = true;
        audioSource.PlayOneShot(noOil);
        Vector2 centerPos = FindObjectOfType<BuildingsManager>().buildings[0].transform.position;
        foreach (GameObject building in FindObjectOfType<BuildingsManager>().buildings)
        {
            if(building.tag != "Turret" && building.name != "Colony Center")
            {
                if(Vector2.Distance(building.transform.position, centerPos) > 5f)
                    building.GetComponent<Sites>().active = false;
            }
        }
    }

    void RestartOil()
    {
        oilZero = false;
        foreach (GameObject building in FindObjectOfType<BuildingsManager>().buildings)
        {
            if (building.tag != "Turret" && building.name != "Colony Center")
            {
                building.GetComponent<Sites>().active = true;
            }
        }
    }

    private void Update()
    {
        FixMinus();
        UpdateDecyas();

        if (oil == 0)
            NoOil();
        else if (oilZero)
            RestartOil();
        if (metal <= 0)
            audioSource.PlayOneShot(noMetal, 0.5f);
    }

    void UpdateDecyas()
    {
        //food decay
        foodDecay = (population - usedPopulation) * 5 + usedPopulation * 10;

        //metal decay
        int turrets = 0;
        foreach (GameObject building in FindObjectOfType<BuildingsManager>().buildings)
        {
            if (building.tag == "Turret")
                turrets++;
        }
        metalDecay = 60 * turrets;

        //oil decay
        int stations = 0;
        foreach (GameObject building in FindObjectOfType<BuildingsManager>().buildings)
        {
            if (building.tag == "Station")
                stations++;
        }
        oilDecay = 60 * stations;
    }

    IEnumerator FoodDecay()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            if (food < foodDecay)
            {
                DecreaseResources(0, 0, 0, 0, (food-foodDecay)/10, 0);
                DecreaseResources(food, 0, 0, 0, 0, 0);
            }
            else
                DecreaseResources(foodDecay, 0, 0, 0, 0, 0);

            if (usedPopulation > population)
                FindObjectOfType<BuildingsManager>().DestroyUndermanned();
        }
    }

    IEnumerator MetalDecay()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            if (metalDecay > metal)
                DecreaseResources(0, 0, metal, 0, 0, 0);
            else
                DecreaseResources(0, 0, metalDecay, 0, 0, 0);
        }
    }

    IEnumerator OilDecay()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            if (oilDecay > oil)
                DecreaseResources(0, oil, 0 , 0, 0, 0);
            else
                DecreaseResources(0, oilDecay, 0 , 0, 0, 0);
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


                    DecreaseResources(0, 0, 0, 0, Random.Range(Mathf.RoundToInt(1 + newColonists / 3), Mathf.RoundToInt(2 + newColonists * 3 / 4)), 0);
                    audioSource.PlayOneShot(arrival);
                    Debug.Log("New Colonists Arrived");
                 }
                 //elif (popConsumption > totalRes) //pop-
            }
        }

    }
    public void DecreaseResources(int foodCost, int metalCost, int oilCost, int populationCost, int populationDiff, int maxPopDiff)
    {
        food -= foodCost;
        metal -= metalCost;
        oil -= oilCost;        
        usedPopulation += populationCost;
        population += populationDiff;
        maxPopulation += maxPopDiff;

        gui.DisplayChanges(foodCost * -1, metalCost * -1, oilCost * -1, populationDiff);

        if (populationDiff > 0)
            messages.ShowMessage(populationDiff + " new colonists have arrived to the colony", new Color(1, 1, 1));
        else if(populationDiff < 0)
            messages.ShowMessage(populationDiff + " colonists have left the colony", new Color(1, 0, 0));
    }

    public void AddResources(int foodAdd, int metalAdd, int oilAdd, int populationAdd, int populationDiff, int maxPopDiff)
    {
        food += foodAdd;
        metal += metalAdd;
        oil += oilAdd;
        usedPopulation -= populationAdd;
        population -= populationDiff;
        maxPopulation -= maxPopDiff;
    }
}
