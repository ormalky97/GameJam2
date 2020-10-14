using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildButton : MonoBehaviour
{
    [Header("Replacer")]
    public bool replacer = false;
    public string oldBuilding;

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

    void ClearPlacers()
    {
        GameObject temp = GameObject.Find("Placer");
        if (temp != null)
            Destroy(temp);
        temp = GameObject.Find("Replacer");
        if (temp != null)
            Destroy(temp);
    }

    public void Build()
    {
        ClearPlacers();

        if (!GameObject.FindObjectOfType<PauseMenu>().isPaused)
        {
            if (CheckResources())
            {
                GameObject newPlacer;
                newPlacer = Instantiate(placer, transform.position, Quaternion.identity);

                if (replacer)
                {
                    newPlacer.GetComponent<Replacer>().targetTitle = oldBuilding;
                    newPlacer.GetComponent<Replacer>().building = building;
                }
                else
                    newPlacer.GetComponent<Placer>().building = building;

                building.GetComponent<Sites>().foodCost = site.foodCost;
                building.GetComponent<Sites>().oilCost = site.oilCost;
                building.GetComponent<Sites>().metalCost = site.metalCost;
                building.GetComponent<Sites>().populationUsage = site.populationUsage;
            }
            else
            {
                GameObject.FindObjectOfType<Messages>().ShowMessage("Not enough resources or free population", new Color(1, 1, 1, 1));
            }
        }
        else
        {
            GameObject.FindObjectOfType<Messages>().ShowMessage("Can't build while game is paused", new Color(1, 1, 1, 1));
        }
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
