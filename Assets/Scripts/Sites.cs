using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Sites : MonoBehaviour
{
    public int foodUp; //per sec
    public int metalUp; //per sec
    public int oilUp; //per sec
    public int popUp; //per 2 mins

    public int foodDown; //per sec
    public int metalDown; //per sec
    public int oilDown; //per sec
    public int popDown; //per 3 mins

    bool isPopUp; //whether site adds pop over time
    bool isPopDown; //whether site substracts pop over time

    GameObject manager;
    public int maxHealth;
    int health;

    private void Awake()
    {
        manager = GameObject.Find("Game Manager");
        health = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GetResource");
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
            
            if (isPopUp)
            {
                yield return new WaitForSeconds(120); //pop adds up every 2 mins
                PopUp();
            }

            if (isPopDown)
            {
                yield return new WaitForSeconds(180); //pop substracts every 3 mins/
                PopDown();
            }
        }
    }

    void AddResource()
    {
        Resources res = manager.GetComponent<Resources>();

        res.food += foodUp -= foodDown;
        res.metal += metalUp -= metalDown;
        res.oil += oilUp -= oilDown;
    }

    void PopUp()
    {
        Resources res = manager.GetComponent<Resources>();

        res.population += popUp;
    }

    void PopDown()
    {
        Resources res = manager.GetComponent<Resources>();

        res.population -= popDown;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
