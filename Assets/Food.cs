using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : Resource
{

    private void Awake()
    {
        name = "Food";
        displayGUI = GameObject.Find("Resources GUI/Panel/Food GUI").GetComponent<Text>();
        changeGUI = GameObject.Find("Resources GUI/Panel/Resources Changes/Food Change").GetComponent<Text>();
        buildPanelGUI = GameObject.Find("Bottom Panel/Build Panel/Build Costs/Food Cost").GetComponent<Text>();
    }

    public Food(int newAmount) : base(newAmount)
    {
        name = "Food";
    }

    public void Add(Food food)
    {
        Add(food.amount);
    }

    public void Decrease(Food food)
    {
        Decrease(food.amount);
    }


    //Operators
    public static bool operator ==(Food a, int b)
    {
        if (a.amount == b)
            return true;
        else
            return false;
    }

    public static bool operator !=(Food a, int b)
    {
        if (a.amount != b)
            return true;
        else
            return false;
    }
    public static int operator+ (Food a, Food b)
    {
        return a.amount + b.amount;
    }

    public static bool operator <(Food a, int b)
    {
        if (a.amount <= b)
            return true;
        else
            return false;
    }
    public static bool operator >(Food a, int b)
    {
        if (a.amount <= b)
            return false;
        else
            return true;
    }
    public static bool operator <(Food a, Food b)
    {
        if (a.amount < b.amount)
            return true;
        else
            return false;
    }
    public static bool operator >(Food a, Food b)
    {
        if (a.amount < b.amount)
            return false;
        else
            return true;
    }

    public static int operator -(Food a, int b)
    {
        return a.amount - b;
    }

    public static int operator- (Food a, Food b)
    {
        return a.amount - b.amount;
    }
}
