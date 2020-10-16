using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Metal : Resource
{

    private void Awake()
    {
        name = "Metal";
        displayGUI = GameObject.Find("Resources GUI/Panel/Metal GUI").GetComponent<Text>();
        changeGUI = GameObject.Find("Resources GUI/Panel/Resources Changes/Metal Change").GetComponent<Text>();
        buildPanelGUI = GameObject.Find("Bottom Panel/Build Panel/Build Costs/Metal Cost").GetComponent<Text>();
    }

    public Metal(int newAmount) : base(newAmount)
    {
        name = "Metal";
    }

    public void Add(Metal metal)
    {
        Add(metal.amount);
    }

    public void Decrease(Metal metal)
    {
        Decrease(metal.amount);
    }

    //Operators
    public static bool operator ==(Metal a, int b)
    {
        if (a.amount == b)
            return true;
        else
            return false;
    }

    public static bool operator !=(Metal a, int b)
    {
        if (a.amount != b)
            return true;
        else
            return false;
    }
    public static bool operator <(Metal a, Metal b)
    {
        if (a.amount < b.amount)
            return true;
        else
            return false;
    }
    public static bool operator >(Metal a, Metal b)
    {
        if (a.amount < b.amount)
            return false;
        else
            return true;
    }
    public static bool operator <=(Metal a, int b)
    {
        if (a.amount <= b)
            return true;
        else
            return false;
    }
    public static bool operator >=(Metal a, int b)
    {
        if (a.amount >= b)
            return true;
        else
            return false;
    }

    public static int operator +(Metal a, Metal b)
    {
        return a.amount + b.amount;
    }

    public static bool operator <(Metal a, int b)
    {
        if (a.amount <= b)
            return true;
        else
            return false;
    }

    public static bool operator >(Metal a, int b)
    {
        if (a.amount <= b)
            return false;
        else
            return true;
    }

    public static int operator -(Metal a, Metal b)
    {
        return a.amount - b.amount;
    }
}
