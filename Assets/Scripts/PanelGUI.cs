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
        foodCost = site.foodCost;
        oilCost = site.oilCost;
        metalCost = site.metalCost;
        populationCost = site.populationUsage;

        UpdatePanel();
    }

    public void UpdatePanel()
    {
        titleTxt.text = title;
        descTxt.text = desc;
        foodTxt.text = "  " + foodCost;
        oilTxt.text = "Oil Cost: " + oilCost;
        metalTxt.text = "Metal Cost: " + metalCost;
        populationTxt.text = "Population Needed: " + populationCost;
    }
}
