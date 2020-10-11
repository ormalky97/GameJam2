using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGUI : MonoBehaviour
{
    public string title;
    public string desc;
    public int foodCost;
    public int moneyCost;
    public int materialsCost;
    public int populationCost;

    public Text titleTxt;
    public Text descTxt;
    public Text foodTxt;
    public Text moneyTxt;
    public Text materialsTxt;
    public Text populationTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPanel(string t, string d, int food, int mon, int mat, int pop)
    {
        title = t;
        desc = d;
        foodCost = food;
        moneyCost = mon;
        materialsCost = mat;
        populationCost = pop;

        UpdatePanel();
    }

    public void UpdatePanel()
    {
        titleTxt.text = title;
        descTxt.text = desc;
        foodTxt.text = "Food Cost: " +foodCost;
        moneyTxt.text = "Money Cost: " + moneyCost;
        materialsTxt.text = "Materials Cost: " + materialsCost;
        populationTxt.text = "Population Needed: " + populationCost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
