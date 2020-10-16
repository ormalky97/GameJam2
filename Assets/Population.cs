using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Population : MonoBehaviour
{
    public int population;
    public int usedPopulation;
    public int maxPopulation;

    public Text displayGUI;
    public Text changeGUI;
    public Text buildPanelGUI;

    private void Awake()
    {
        population = 0;
        usedPopulation = 0;
        maxPopulation = 0;

        displayGUI = GameObject.Find("Resources GUI/Panel/Population GUI").GetComponent<Text>();
        changeGUI = GameObject.Find("Resources GUI/Panel/Resources Changes/Population Change").GetComponent<Text>();
        buildPanelGUI = GameObject.Find("Bottom Panel/Build Panel/Build Costs/Population Cost").GetComponent<Text>();
    }
    
    public bool isOverdraft()
    {
        if (population > maxPopulation)
            return true;
        else
            return false;
    }

    public int Unused()
    {
        return maxPopulation - population;
    }

    public int FreePopulation()
    {
        return population - usedPopulation;
    }

    //Constructors
    public Population()
    {
        population = 0;
        usedPopulation = 0;
        maxPopulation = 0;
    }

    public Population(int pop, int used, int max)
    {
        population = pop;
        usedPopulation = used;
        maxPopulation = max;
    }
    //Add and Decrease
    public void Decrease(Population newPopulation)
    {
        Decrease(newPopulation.population, newPopulation.usedPopulation, newPopulation.maxPopulation);
    }

    public void Decrease(int addPop, int addUsedPop, int addMaxPop)
    {
        population -= addPop;
        usedPopulation -= addUsedPop;
        maxPopulation -= addMaxPop;
    }

    public void Add(Population newPopulation)
    {
        Add(newPopulation.population, newPopulation.usedPopulation, newPopulation.maxPopulation);
    }

    public void Add(int addPop, int addUsedPop, int addMaxPop)
    {
        population += addPop;
        usedPopulation += addUsedPop;
        maxPopulation += addMaxPop;
    }
}
