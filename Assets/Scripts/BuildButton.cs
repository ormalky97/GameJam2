using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildButton : MonoBehaviour
{
    [Header("Settings")]
    public GameObject building;
    public string title;
    public string desc;
    
    [Header("Refs")]
    public GameObject placer;
    public GameObject panel;

    //Refs
    GameObject manager;
    Resources res;
    Sites site;

    private void Awake()
    {
        manager = GameObject.Find("Game Manager");
        res = manager.GetComponent<Resources>();
        site = building.GetComponent<Sites>();
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
        panel.GetComponent<PanelGUI>().SetPanel(title, desc, site.foodCost, site.oilCost, site.metalCost, site.populationUsage);
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
        building.GetComponent<Sites>().foodCost = site.foodCost;
        building.GetComponent<Sites>().oilCost = site.oilCost;
        building.GetComponent<Sites>().metalCost = site.metalCost;
        building.GetComponent<Sites>().populationUsage = site.populationUsage;
    }

    bool CheckResources()
    {
        if(res.food >= site.foodCost &&
           res.oil >= site.oilCost &&
           res.metal >= site.metalCost &&
           res.population - res.usedPopulation >= site.populationUsage)
        {
            return true;
        }
        else
            return false;
    }
}
