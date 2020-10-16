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
    public int maxPopAdd;

    [Header("Settings")]
    public bool active = true;
    public int maxHealth;
    public GameObject hitEffect;

    [Header("Audio Clips")]
    public AudioClip buildSound;
    public AudioClip hitSound;
    public AudioClip destroyedSound;

    public Food costFood;
    public Metal costMetal;
    public Oil costOil;

    //Refs
    PlayerResources res;
    SpriteRenderer spr;
    GameObject healthbar;
    AudioSource audioSource;
    Camera cam;
    GameObject toStation;

    //Inner Vars
    int health;
    int fixCounter = 0;

    [Header("Destroy Methods, do not touch!")]
    public bool undermanned = false;

    private void Awake()
    {
        res = FindObjectOfType<PlayerResources>();
        spr = GetComponent<SpriteRenderer>();
        healthbar = transform.GetChild(0).gameObject;
        healthbar.GetComponent<Healthbar>().maxHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        cam = Camera.main;

        costFood = new Food(foodCost);
        costMetal = new Metal(metalCost);
        costOil = new Oil(oilCost);

        health = maxHealth;
    }

    void Start()
    {
        FindObjectOfType<BuildingsManager>().NewBuilding(gameObject);
        StartCoroutine("GetResource");

        if (maxPopAdd > 0)
            res.Population().Add(0, 0, maxPopAdd);

        //SFX
        audioSource.PlayOneShot(buildSound);
    }

    bool FindStation()
    {
        foreach (GameObject station in FindObjectOfType<BuildingsManager>().stations)
        {
            if (station.GetComponent<Station>().CheckInRange(gameObject))
            {
                toStation = station;
                return true;
            }
        }

        return false;
    }

    void ActiveColor()
    {
        if (!active)
            spr.color = new Color(0.7f, 0.7f, 0.7f);
        else
            spr.color = Color.white;
    }

    private void Update()
    {
        ActiveColor();

        if(toStation == null && !CompareTag("Turret"))
        {
            if (FindStation())
                active = true;
            else
                active = false;
        }

        UpdateHealthbar();
    }

    private void LateUpdate()
    {
        healthbar.transform.rotation = Quaternion.Euler(0, 0, 0);
        healthbar.transform.position = transform.position + Vector3.up * 1.3f;
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
            fixCounter = 0;
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
            //Colony Center - Game Over
            if (gameObject == FindObjectOfType<BuildingsManager>().buildings[0])
                FindObjectOfType<GameOverMenu>().GameOver();

            //Decrease Population
            res.Population().Decrease(populationUsage, populationUsage, maxPopAdd);

            //Overdrafted
            if (res.Population().isOverdraft())
                FindObjectOfType<BuildingsManager>().Overdrafted();
        } 
        else if (undermanned)
        {
            res.Population().Decrease(0, populationUsage, 0);

            FindObjectOfType<Messages>().ShowMessage("Your last " +title +" was destroyed due to being undermanned", Color.red);
        }

        FindObjectOfType<BuildingsManager>().buildings.Remove(gameObject);
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
            if (active)
            {
                res.AddResources(foodUp, metalUp, oilUp);
            }
        }
    }
}
