using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    public List<GameObject> buildings;
    public List<GameObject> stations;

    float maxDistance = 0;
    int maxDistanceIndex;

    private void Awake()
    {
        buildings = new List<GameObject>();
        stations = new List<GameObject>();
    }

    public float GetMaxDistance()
    {
        return maxDistance;
    }
    
    public void NewBuilding(GameObject building)
    {
        buildings.Add(building);
        if (building.tag == "Station")
            stations.Add(building);

        //Keep refrence for furthest building
        float distance = Vector2.Distance(transform.position, building.transform.position);
        if (distance > maxDistance)
        {
            maxDistance = distance;
            maxDistanceIndex = buildings.Count - 1;
        }
    }

    public void DestroyUndermanned()
    {
        int overdraft = res.population - res.maxPopulation;
        foreach (GameObject building in FindObjectOfType<BuildingsManager>().buildings)
    }
}
