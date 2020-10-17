using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oil : Resource
{


    private void Awake()
    {
        name = "Oil";
        displayGUI = GameObject.Find("Resources GUI/Panel/Oil GUI").GetComponent<Text>();
        changeGUI = GameObject.Find("Resources GUI/Panel/Resources Changes/Oil Change").GetComponent<Text>();
        buildPanelGUI = GameObject.Find("Bottom Panel/Build Panel/Build Costs/Oil Cost").GetComponent<Text>();
    }
    public Oil(int newAmount) : base(newAmount)
    {
        name = "Oil";
        displayGUI = GameObject.Find("Resources GUI/Panel/Oil GUI").GetComponent<Text>();
        changeGUI = GameObject.Find("Resources GUI/Panel/Resources Changes/Oil Change").GetComponent<Text>();
    }

    public void Add(Oil oil)
    {
        Add(oil.amount);
    }

    public void Decrease(Oil oil)
    {
        Decrease(oil.amount);
    }

    //Operators
    public static bool operator ==(Oil a, int b)
    {
        if (a.amount == b)
            return true;
        else
            return false;
    }

    public static bool operator !=(Oil a, int b)
    {
        if (a.amount != b)
            return true;
        else
            return false;
    }

    public static bool operator <(Oil a, int b)
    {
        if (a.amount <= b)
            return true;
        else
            return false;
    }

    public static bool operator >(Oil a, int b)
    {
        if (a.amount <= b)
            return false;
        else
            return true;
    }
    public static bool operator <(Oil a, Oil b)
    {
        if (a.amount < b.amount)
            return true;
        else
            return false;
    }
    public static bool operator >(Oil a, Oil b)
    {
        if (a.amount < b.amount)
            return false;
        else
            return true;
    }

    public static int operator +(Oil a, Oil b)
    {
        return a.amount + b.amount;
    }

    public static int operator -(Oil a, Oil b)
    {
        return a.amount - b.amount;
    }
}
