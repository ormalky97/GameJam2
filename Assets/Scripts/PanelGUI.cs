using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGUI : MonoBehaviour
{
    [Header("Text Objects Refs")]
    public Text titleTxt;
    public Text descTxt;
    public Text foodTxt;
    public Text oilTxt;
    public Text metalTxt;
    public Text populationTxt;

    //Vars
    string title;
    string desc;
    int foodCost;
    int oilCost;
    int metalCost;
    int populationCost;
    Resources res;

    private void Awake()
    {
        res = FindObjectOfType<Resources>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdatePanel();

        if (transform.position.x < 80)
            transform.position = new Vector2(80, transform.position.y);
    }

    public void SetPanel(GameObject obj, string d)
    {
        gameObject.SetActive(true);
        Sites site = obj.GetComponent<Sites>();

        title = site.title;
        desc = d;
        foodCost = site.foodCost;
        oilCost = site.oilCost;
        metalCost = site.metalCost;
        populationCost = site.populationUsage;

        UpdatePanel();
    }

    void UpdatePanel()
    {
        titleTxt.text = title;
        descTxt.text = desc;
        foodTxt.text = "  " + foodCost;
        oilTxt.text = "  " + oilCost;
        metalTxt.text = "  " + metalCost;
        populationTxt.text = "  " + populationCost;

        SetColors();
    }

    void SetColors()
    {
        if (res.food < foodCost)
            foodTxt.color = Color.red;
        else
            foodTxt.color = Color.white;

        if (res.metal < metalCost)
            metalTxt.color = Color.red;
        else
            metalTxt.color = Color.white;

        if (res.oil < oilCost)
            oilTxt.color = Color.red;
        else
            oilTxt.color = Color.white;

        if (res.population - res.usedPopulation < populationCost)
            populationTxt.color = Color.red;
        else
            populationTxt.color = Color.white;
    }
}
