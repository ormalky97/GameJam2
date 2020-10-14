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

    [Header("Destroy Methods, do not touch!")]
    public bool undermanned = false;
    public bool replaced = false;
    public int usageDiff;
    public int maxDiff;

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
        if (health <= 0 )
        {
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        if (health <= 0)
        {
            if (gameObject == FindObjectOfType<BuildingsManager>().buildings[0])
                FindObjectOfType<GameOverMenu>().GameOver();
            res.DecreaseResources(0, 0, 0, -populationUsage, -populationUsage, -populationAdd);
            if (res.population > res.maxPopulation)
            {
                res.DecreaseResources(0, 0, 0, 0, res.maxPopulation - res.population, 0);
                if (res.usedPopulation > res.population)
                {
                    FindObjectOfType<BuildingsManager>().DestroyUndermanned();
                }
            }
        } 
        else if (undermanned)
        {
            res.DecreaseResources(0, 0, 0, -populationUsage, 0, 0);
            FindObjectOfType<BuildingsManager>().buildings.Remove(gameObject);
            FindObjectOfType<Messages>().ShowMessage("Your last " +title +" was destroyed due to being undermanned", new Color(1, 0, 0));
        }
        else if (replaced)
        {
            res.DecreaseResources(0, 0, 0, usageDiff, 0, maxDiff);
            FindObjectOfType<BuildingsManager>().buildings.Remove(gameObject);
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
            yield return new WaitForSeconds(1f); //resources add up every sec
            res.AddResources(foodUp, metalUp, oilUp, 0, 0, 0);
        }
    }
}
