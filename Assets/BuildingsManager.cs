using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    List<GameObject> buildings;
    float maxDistance = 0;
    int maxDistanceIndex;

    private void Awake()
    {
        buildings = new List<GameObject>();
    }

    public float GetMaxDistance()
    {
        return maxDistance;
    }
    
    void NewBuilding(GameObject building)
    {
        buildings.Add(building);

        //Keep refrence for furthest building
        float distance = Vector2.Distance(transform.position, building.transform.position);
        if (distance > maxDistance)
        {
            maxDistance = distance;
            maxDistanceIndex = buildings.Count - 1;
        }
    }
}
