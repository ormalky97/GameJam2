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
    public bool active = true;
    public int maxHealth;
    public GameObject hitEffect;

    [Header("Audio Clips")]
    public AudioClip hitSound;
    public AudioClip buildSound;

    //Refs
    GameObject manager;
    Resources res;
    SpriteRenderer spr;
    GameObject healthbar;
    AudioSource audioSource;
    Camera cam;

    //Inner Vars
    int health;
    int fixCounter = 0;
    float distanceToCamera;

    [Header("Destroy Methods, do not touch!")]
    public bool undermanned = false;
    public bool replaced = false;
    public int usageDiff;
    public int maxDiff;

    private void Awake()
    {
        manager = GameObject.Find("Game Manager");
        res = manager.GetComponent<Resources>();
        spr = GetComponent<SpriteRenderer>();
        healthbar = transform.GetChild(0).gameObject;
        healthbar.GetComponent<Healthbar>().maxHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        cam = Camera.main;

        health = maxHealth;
    }

    void Start()
    {
        manager.GetComponent<BuildingsManager>().NewBuilding(gameObject);
        StartCoroutine("GetResource");
        res.DecreaseResources(0, 0, 0, 0, 0, populationAdd);
        audioSource.PlayOneShot(buildSound);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            active = false;
        if (Input.GetKeyDown(KeyCode.V))
            active = true;

        if (!active)
            spr.color = new Color(0, 0, 0, 0.5f);
        else
            spr.color = new Color(1, 1, 1, 1);

        UpdateHealthbar();
        distanceToCamera = Vector2.Distance(transform.position, cam.transform.position);
    }

    private void LateUpdate()
    {
        healthbar.transform.rotation = Quaternion.Euler(0, 0, 0);
        healthbar.transform.position = new Vector2(transform.position.x, transform.position.y + 1.3f);
    }

    private void FixedUpdate()
    {
        if (fixCounter < 3000)
        {
            StopCoroutine("Fix");
            fixCounter++;
        }
        else
        {
            StartCoroutine("Fix");
        }
        
    }

    IEnumerator Fix()
    {
        while(health < maxHealth)
        {
            health++;
            yield return new WaitForSeconds(1f);
        }
    }

    void UpdateHealthbar()
    {
        if (health < maxHealth)
        {
            healthbar.SetActive(true);
            healthbar.GetComponent<Healthbar>().health = health;
        }
        else if(healthbar.activeSelf)
            healthbar.SetActive(false);
    }

    public void RecieveDamage(int damage)
    {
        health -= damage;

        //Sound and VFX
        Hit();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.PlayOneShot(hitSound);
        audioSource.pitch = 1f;

        fixCounter = 0;
        if (health <= 0 )
            Destroy(gameObject);
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
            if(active)
                res.AddResources(foodUp, metalUp, oilUp, 0, 0, 0);
        }
    }
}
