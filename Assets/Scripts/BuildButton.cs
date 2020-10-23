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
    [TextArea]
    public string desc;

    [Header("Refs")]
    public GameObject buildPanel;
    public GameObject placer;


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

    public void ShowBuildPanel()
    {
        Vector2 PanelPos = new Vector2(transform.position.x, transform.position.y + 15);
        buildPanel.SetActive(true);
        buildPanel.GetComponent<PanelGUI>().SetPanel(building, desc);
        buildPanel.transform.position = PanelPos;
    }

    public void HideBuildPanel()
    {
        buildPanel.SetActive(false);
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
        }
        else
        {
            GameObject.FindObjectOfType<Messages>().ShowMessage("Can't build while game is paused", new Color(1, 1, 1, 1));
        }
    }

    bool CheckResources()
    {
        string message = "";

        if (res.food < site.foodCost)
            message += "Not enough Food \n";

        if (res.metal < site.metalCost)
            message += "Not enough Metal \n";

        if (res.oil < site.oilCost)
            message += "Not enough Oil \n";

        if (res.population - res.usedPopulation < site.populationUsage)
            message += "Not enough free Population \n";

        if (message == "")
            return true;
        else
        {
            FindObjectOfType<Messages>().ShowMessage(message, Color.white);
            return false;
        }  
    }
}
