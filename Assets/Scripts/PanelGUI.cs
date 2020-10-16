using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.UI;

public class PanelGUI : MonoBehaviour
{
    [Header("Text Objects Refs")]
    public Text titleTxt;
    public Text descTxt;

    //Refs
    PlayerResources playerResources;

    //Vars
    string title;
    string desc;
    Food foodCost;
    Oil oilCost;
    Metal metalCost;
    int populationCost;

    private void Awake()
    {
        playerResources = FindObjectOfType<PlayerResources>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (transform.position.x < 150f)
            transform.position = new Vector2(150, transform.position.y);
    }

    public void SetPanel(GameObject obj, string d)
    {
        Sites site = obj.GetComponent<Sites>();

        title = site.title;
        desc = d;

        foodCost = new Food(site.foodCost);
        oilCost = new Oil(site.oilCost);
        metalCost = new Metal(site.metalCost);
        populationCost = site.populationUsage;

        UpdatePanel();
    }

    public void UpdatePanel()
    {
        titleTxt.text = title;
        descTxt.text = desc;

        SetResource(foodCost);
        SetResource(metalCost);
        SetResource(oilCost);
        SetResource(populationCost);
    }

    void SetResource(Resource res)
    {
        if (res <= playerResources.ResourceType(res))
            playerResources.ResourceType(res).buildPanelGUI.color = Color.white;
        else
            playerResources.ResourceType(res).buildPanelGUI.color = Color.red;

        playerResources.ResourceType(res).buildPanelGUI.text = "  " + res.amount;
    }

    void SetResource(int pop)
    {
        if (playerResources.Population().FreePopulation() < pop)
            playerResources.Population().buildPanelGUI.color = Color.red;
        else
            playerResources.Population().buildPanelGUI.color = Color.white;

        playerResources.Population().buildPanelGUI.text = "  " + pop;
    }
}
