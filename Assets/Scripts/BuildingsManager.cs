using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        int overdraft = GetComponent<Resources>().usedPopulation - GetComponent<Resources>().population;
        for (int i = buildings.Count - 1; overdraft > 0; i--)
        {
            if (buildings[i].tag == "Food" || buildings[i].tag == "oil" || buildings[i].tag == "Metal")
            {
                buildings[i].GetComponent<Sites>().undermanned = true;
                Destroy(buildings[i]);
            }
        }
    }
}
