using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public string name;
    public int amount;

    //GUI Refs
    public Text displayGUI;
    public Text changeGUI;
    public Text buildPanelGUI;

    //constructor
    public Resource(int amn)
    {
        amount = amn;
    }

    //Add and Decrease

    public void Add(int amn)
    {
        amount += amn;
    }

    public void Decrease(int amn)
    {
        amount -= amn;
    }

    //Operators
    public static bool operator <(Resource a, Resource b)
    {
        if (a.amount < b.amount)
            return true;
        else
            return false;
    }
    public static bool operator >(Resource a, Resource b)
    {
        if (a.amount < b.amount)
            return false;
        else
            return true;
    }
    public static bool operator <=(Resource a, Resource b)
    {
        if (a.amount <= b.amount)
            return true;
        else
            return false;
    }
    public static bool operator >=(Resource a, Resource b)
    {
        if (a.amount >= b.amount)
            return true;
        else
            return false;
    }
}
