using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class Sites : MonoBehaviour
{
    [Header("Title")]
    public string title;

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

    [Header("Settings")]
    public int maxHealth;
    public GameObject hitEffect;

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
        res.DecreaseResources(0, 0, 0, 0, 0, populationAdd);
    }

    public void RecieveDamage(int damage)
    {
        Debug.Log(health);
        health -= damage;
        Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
            res.DecreaseResources(0, 0, 0, -populationUsage, -populationUsage, -populationAdd);
            if (res.population > res.maxPopulation)
            {
                FindObjectOfType<BuildingsManager>().DestroyUndermanned();
                res.DecreaseResources(0, 0, 0, 0, res.maxPopulation - res.population, 0);
            }
        }
    }

    void Hit()
    {
        float x = transform.position.x + Random.Range(-0.2f, 0.2f);
        float y = transform.position.y + Random.Range(-0.2f, 0.2f);
        Vector2 pos = new Vector2(x, y);

        GameObject temp = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(temp, 2f);
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
        res.metal += metalUp;
        res.oil += oilUp;
    }
}
