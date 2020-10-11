﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildButton : MonoBehaviour
{
    public string title;
    public string desc;

    public GameObject building;
    public GameObject placer;
    public GameObject panel;

    public int foodCost;
    public int moneyCost;
    public int materialCost;
    public int populationCost;

    GameObject manager;
    Resources res;

    private void Awake()
    {
        manager = GameObject.Find("Game Manager");
        res = manager.GetComponent<Resources>();
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
        panel.GetComponent<PanelGUI>().SetPanel(title, desc, foodCost, moneyCost, materialCost, populationCost);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }

    public void Build()
    {
        if (CheckResources())
        {
            SetupPlacer();
        }
    }

    void SetupPlacer()
    {
        GameObject newPlacer;
        newPlacer = Instantiate(placer, transform.position, Quaternion.identity);

        newPlacer.GetComponent<Placer>().building = building;
        newPlacer.GetComponent<Placer>().foodCost = foodCost;
        newPlacer.GetComponent<Placer>().moneyCost = moneyCost;
        newPlacer.GetComponent<Placer>().materialCost = materialCost;
        newPlacer.GetComponent<Placer>().populationCost = populationCost;
    }

    bool CheckResources()
    {
        if(res.food >= foodCost &&
           res.money >= moneyCost &&
           res.materials >= materialCost &&
           res.population + populationCost <= res.maxPopulation)
        {
            return true;
        }
        else
            return false;
    }
}
