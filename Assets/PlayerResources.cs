using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    //Resources
    Food food;
    Metal metal;
    Oil oil;
    Population population;

    //GUI
    ResourcesGUI resourcesGUI;

    //Starting Resources
    public int startFood;
    public int startMetal;
    public int startOil;
    public int startPopulation;
    
    private void Awake()
    {
        food = gameObject.AddComponent<Food>();
        food.amount = startFood;

        metal = gameObject.AddComponent<Metal>();
        metal.amount = startMetal;

        oil = gameObject.AddComponent<Oil>();
        oil.amount = startOil;

        population = gameObject.AddComponent<Population>();
        population.population = startPopulation;

        resourcesGUI = FindObjectOfType<ResourcesGUI>();
    }

    private void Update()
    {
        UpdateResourcesGUI();
        UpdateChangesGUI();
    }

    public Resource ResourceType(Resource src)
    {
        if (src is Food)
            return food;
        if (src is Metal)
            return metal;
        if (src is Oil)
            return oil;

        return null;
    }

    void UpdateResourcesGUI()
    {
        resourcesGUI.UpdateResourcesUI(food);
        resourcesGUI.UpdateResourcesUI(metal);
        resourcesGUI.UpdateResourcesUI(oil);
        resourcesGUI.UpdateResourcesUI(population);
    }

    void UpdateChangesGUI()
    {
        resourcesGUI.UpdateChangesUI(food);
        resourcesGUI.UpdateChangesUI(metal);
        resourcesGUI.UpdateChangesUI(oil);
        resourcesGUI.UpdateChangesUI(population);
    }

    //Get Resources
    public Food Food()
    {
        return food;
    }
    public Metal Metal()
    {
        return metal;
    }
    public Oil Oil()
    {
        return oil;
    }
    public Population Population()
    {
        return population;
    }
    public int TotalResources()
    {
        return food.amount + oil.amount + metal.amount;
    }

    //Add & Decrease

    public void AddResources(Food addFood, Metal addMetal, Oil addOil)
    {
        food.Add(addFood);
        metal.Add(addMetal);
        oil.Add(addOil);
    }
    public void DecreaseResources(Food decFood, Metal decMetal, Oil decOil)
    {
        food.Decrease(decFood);
        metal.Decrease(decMetal);
        oil.Decrease(decOil);
    }

    public void AddResources(int addFood, int addMetal, int addOil)
    {
        food.Add(addFood);
        metal.Add(addMetal);
        oil.Add(addOil);
    }

    public void DecreaseResources(int decFood, int decMetal, int decOil)
    {
        food.Decrease(decFood);
        resourcesGUI.DisplayChange(new Food(-decFood));
        metal.Decrease(decMetal);
        resourcesGUI.DisplayChange(new Metal(-decMetal));
        oil.Decrease(decOil);
        resourcesGUI.DisplayChange(new Oil(-decOil));
    }

    public void AddResource(Food add)
    {
        food.Add(add);
    }
    public void AddResource(Metal add)
    {
        metal.Add(add);
    }
    public void AddResource(Oil add)
    {
        oil.Add(add);
    }
    public void DecreaseResource(Food dec)
    {
        food.Decrease(dec);
        resourcesGUI.DisplayChange(new Food(-dec.amount));
    }
    public void DecreaseResource(Metal dec)
    {
        metal.Decrease(dec);
        resourcesGUI.DisplayChange(new Metal(-dec.amount));
    }
    public void DecreaseResource(Oil dec)
    {
        oil.Decrease(dec);
        resourcesGUI.DisplayChange(new Oil(-dec.amount));
    }
}
