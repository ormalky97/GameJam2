using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCategory : MonoBehaviour
{
    public GameObject foodCategory;
    public GameObject metalCategory;
    public GameObject OilCategory;
    public GameObject StationsCategory;
    public GameObject PopulationCategory;
    public GameObject TurretsCategory;

    GameObject currentCategory;

    void CloseAll()
    {
        foodCategory.SetActive(false);
        metalCategory.SetActive(false);
        OilCategory.SetActive(false);
        StationsCategory.SetActive(false);
        PopulationCategory.SetActive(false);
        TurretsCategory.SetActive(false);
    }

    public void ToggleCategory(GameObject category)
    {
        if (category.activeSelf)
        {
            category.SetActive(false);
            currentCategory = null;
        }
        else
        {
            CloseAll();
            category.SetActive(true);
            currentCategory = category;
        }
    }

    public GameObject GetActiveCategory()
    {
        return currentCategory;
    }
}
