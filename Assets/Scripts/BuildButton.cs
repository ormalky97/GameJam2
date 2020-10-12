using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildButton : MonoBehaviour
{
    public string title;
    public string desc;
    
    public GameObject site;
    public GameObject placer;
    public GameObject panel;

    public int foodCost;
    public int oilCost;
    public int metalCost;
    public int populationUsage;

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
        panel.GetComponent<PanelGUI>().SetPanel(title, desc, foodCost, oilCost, metalCost, populationUsage);
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

        newPlacer.GetComponent<Placer>().site = site;
        newPlacer.GetComponent<Placer>().foodCost = foodCost;
        newPlacer.GetComponent<Placer>().oilCost = oilCost;
        newPlacer.GetComponent<Placer>().metalCost = metalCost;
        newPlacer.GetComponent<Placer>().populationUsage = populationUsage;
    }

    bool CheckResources()
    {
        if(res.food >= foodCost &&
           res.oil >= oilCost &&
           res.metal >= metalCost &&
           res.population - res.usedPopulation >= populationUsage)
        {
            return true;
        }
        else
            return false;
    }
}
