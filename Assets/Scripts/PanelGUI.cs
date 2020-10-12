﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGUI : MonoBehaviour
{
    string title;
    string desc;
    int foodCost;
    int oilCost;
    int metalCost;
    int populationCost;

    public Text titleTxt;
    public Text descTxt;
    public Text foodTxt;
    public Text oilTxt;
    public Text metalTxt;
    public Text populationTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPanel(string t, string d, int food, int oil, int met, int pop)
    {
        title = t;
        desc = d;
        foodCost = food;
        oilCost = oil;
        metalCost = met;
        populationCost = pop;

        UpdatePanel();
    }

    public void UpdatePanel()
    {
        titleTxt.text = title;
        descTxt.text = desc;
        foodTxt.text = "Food Cost: " + foodCost;
        oilTxt.text = "Oil Cost: " + oilCost;
        metalTxt.text = "Metal Cost: " + metalCost;
        populationTxt.text = "Population Needed: " + populationCost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
