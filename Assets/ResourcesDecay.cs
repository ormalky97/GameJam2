using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesDecay : MonoBehaviour
{
    //Player Resources
    PlayerResources playerResources;

    [Header("Audio Clips")]
    public AudioClip noOil;
    public AudioClip noMetal;
    public AudioClip arrival;

    bool oilZero = false;
    AudioSource audioSource;

    void Awake()
    {
        playerResources = FindObjectOfType<PlayerResources>();

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine("Populate");
        StartCoroutine("FoodDecay");
        StartCoroutine("MetalDecay");
        StartCoroutine("OilDecay");
    }

    private void Update()
    {
        if (playerResources.Oil() == 0)
            NoOil();

        else if (oilZero)
            RestartOil();

        if (playerResources.Metal() == 0)
            audioSource.PlayOneShot(noMetal, 0.5f);
    }

    void NoOil()
    {
        oilZero = true;
        audioSource.PlayOneShot(noOil);
        foreach (GameObject station in FindObjectOfType<BuildingsManager>().stations)
        {
            if (station.name != "Colony Center")
            {
                station.GetComponent<Sites>().active = false;
            }
        }
    }

    void RestartOil()
    {
        oilZero = false;
        foreach (GameObject station in FindObjectOfType<BuildingsManager>().stations)
        {
            if (station.name != "Colony Center")
            {
                station.GetComponent<Sites>().active = true;
            }
        }
    }

    IEnumerator Populate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(60, 90));
            if (playerResources.Population().Unused() > 0)
            {
                int trpc = playerResources.TotalResources() / 15; //Total Resources Per Colonist
                int newColonists;

                if (playerResources.Population().population < trpc) //pop+
                {
                    if ((trpc - playerResources.Population().population) >= playerResources.Population().Unused())
                        newColonists = playerResources.Population().Unused();
                    else
                        newColonists = trpc - playerResources.Population().population;

                    int popAdd = Random.Range(Mathf.RoundToInt(1 + newColonists / 3), Mathf.RoundToInt(2 + newColonists * 3 / 4));
                    playerResources.Population().Add(popAdd, 0, 0);
                    audioSource.PlayOneShot(arrival);
                    Debug.Log("New Colonists Arrived");
                }
            }
        }

    }

    IEnumerator FoodDecay()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            Food foodDecay = new Food(playerResources.Population().FreePopulation() * 5 + playerResources.Population().usedPopulation * 10);
            if (playerResources.Food() < foodDecay)
            {
                playerResources.Population().Add(0, (playerResources.Food() - foodDecay) / 10, 0);
                playerResources.DecreaseResource(playerResources.Food());
            }
            else
                playerResources.DecreaseResource(foodDecay);
        }
    }

    IEnumerator MetalDecay()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            int turrets = 0;
            foreach (GameObject building in FindObjectOfType<BuildingsManager>().buildings)
            {
                if (building.CompareTag("Turret"))
                    turrets++;
            }
            Metal metalDecay = new Metal(30 * turrets);
            if (playerResources.Metal() < metalDecay)
                playerResources.DecreaseResource(playerResources.Metal());
            else
                playerResources.DecreaseResource(metalDecay);
        }
    }

    IEnumerator OilDecay()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            Oil oilDecay = new Oil(60 * FindObjectOfType<BuildingsManager>().stations.Count);
            if (playerResources.Oil() < oilDecay)
                playerResources.DecreaseResource(playerResources.Oil());
            else
                playerResources.DecreaseResource(oilDecay);
        }
    }
}
