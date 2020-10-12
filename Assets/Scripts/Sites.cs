using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Sites : MonoBehaviour
{
    [Header("Costs")]
    public int foodCost;
    public int oilCost;
    public int metalCost;
    public int populationUsage;

    [Header("Production")]
    public int foodUp; //per sec
    public int metalUp; //per sec
    public int oilUp; //per sec
    public int populationAdd;

    [Header("Upkeep")]
    public int metalDown; //per sec
    public int oilDown; //per sec

    [Header("Settings")]
    public int maxHealth;

    //Refs
    GameObject manager;
    Resources res;
    
    //Inner Vars
    int health;

    private void Awake()
    {
        manager = GameObject.Find("Game Manager");
        res = manager.GetComponent<Resources>();
        health = maxHealth;
    }

    void Start()
    {
        manager.GetComponent<BuildingsManager>().NewBuilding(gameObject);
        StartCoroutine("GetResource");
        res.maxPopulation += populationAdd;
    }

    public void RecieveDamage(int damage)
    {
        Debug.Log(health);
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }

    IEnumerator GetResource()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); //resources add up every sec
            AddResource();
        }
    }

    void AddResource()
    {
        res.food += foodUp;
        res.metal += metalUp -= metalDown;
        res.oil += oilUp -= oilDown;
    }
}
