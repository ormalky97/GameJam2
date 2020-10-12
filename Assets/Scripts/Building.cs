using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string resourceType;
    public int amount;
    public float rate;
    public int maxHealth;
    

    GameObject manager;
    int health;

    // Start is called before the first frame update
    private void Awake()
    {
        manager = GameObject.Find("Game Manager");
        health = maxHealth;
    }

    void Start()
    {    
        switch (resourceType)
        {
            case "Population":
                manager.GetComponent<Resources>().maxPopulation += amount;
                break;

            case "Turret":
                break;

            default:
                StartCoroutine("GetResource");
                break;
        }     
    }

    public void RecieveDamage(int amount)
    {
        Debug.Log(health);
        health -= amount;
        if (health <= 0)
            Destroy(gameObject);
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
