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
    public GameObject buildPanel;
    public GameObject placer;


    //Refs
    PlayerResources res;
    Sites site;

    string reason;

    private void Awake()
    {
        res = FindObjectOfType<PlayerResources>();
        site = building.GetComponent<Sites>();
    }

    public void ShowBuildPanel()
    {
        Vector2 PanelPos = transform.position + Vector3.up * 15;
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

        if (!FindObjectOfType<PauseMenu>().isPaused)
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
            FindObjectOfType<Messages>().ShowMessage("Can't build while game is paused", new Color(1, 1, 1, 1));
        }
    }

    string CheckResource(Resource resource, Resource target)
    {
        if (resource < target)
            return "Not enough " + resource.name + "\n";
        else
            return "";
    }
    string CheckResource(int pop)
    {
        if (res.Population().population < pop)
            return "Not enough Population \n";
        else
            return "";
    }

    bool CheckResources()
    {
        reason = "";
        reason += CheckResource(res.Food(), new Food(site.foodCost));
        reason += CheckResource(res.Metal(), new Metal(site.metalCost));
        reason += CheckResource(res.Oil(), new Oil (site.oilCost));
        reason += CheckResource(site.populationUsage);

        if (reason == "")
            return true;

        FindObjectOfType<Messages>().ShowMessage(reason);
        return false;
    }
}
