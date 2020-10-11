using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string resourceType;
    public int amount;
    public float rate;

    GameObject manager;

    // Start is called before the first frame update
    void Awake()
    {
        manager = GameObject.Find("Game Manager");
        if(resourceType != "Population")
            StartCoroutine("GetResource");
        else
        {
            manager.GetComponent<Resources>().maxPopulation += amount;
        }
    }

    IEnumerator GetResource()
    {
        while(true)
        {
            yield return new WaitForSeconds(60 / rate);
            AddResource();
        }
    }

    void AddResource()
    {
        Resources res = manager.GetComponent<Resources>();

        switch (resourceType)
        {
            case "Food":
                res.food += amount;
                break;

            case "Money":
                res.money += amount;
                break;

            case "Materials":
                res.materials += amount;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
