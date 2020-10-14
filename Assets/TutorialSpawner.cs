using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    [Header("Settings")]
    public int waveSize = 5;

    [Header("Refs")]
    public GameObject enemy;

    //Generates New Spawn Position
    Vector2 NewSpawnPosition()
    {
        float spawnDistance = FindObjectOfType<BuildingsManager>().GetMaxDistance() + 25f;
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnDistance;
    }

    //Generates Enemy waves
    public void SendWave()
    {
        GameObject temp;
        Vector2 spawnPoint;

        for (int i = 0; i < waveSize; i++)
        {
            spawnPoint = NewSpawnPosition();
            temp = Instantiate(enemy, spawnPoint, Quaternion.identity);
        }
    }
}
