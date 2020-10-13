using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    public int waveSize;
    public float waveTime;
    public float waveIncreaseTimer = 60f;
    public float newEnemyTimer = 60f;

    [Header("Enemies")]
    public List<GameObject> enemies;

    //Inner Vars
    int bestEnemy;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("IncreaseWaveSize");
        StartCoroutine("NewEnemy");
        StartCoroutine("SendWave");
    }

    //Generates New Spawn Position
    Vector2 NewSpawnPosition()
    {
        float spawnDistance = GetComponent<BuildingsManager>().GetMaxDistance() + 25f;
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnDistance;
    }

    //Increase Wave Size
    IEnumerator IncreaseWaveSize()
    {
        int i = 0;
        while(true)
        {
            i++;
            yield return new WaitForSeconds(waveIncreaseTimer);
            waveSize += 2 * i;
        }
    }

    //Inroduces New Enemies
    IEnumerator NewEnemy()
    {
        int i = 0;
        while (bestEnemy < enemies.Count)
        {
            i++;
            yield return new WaitForSeconds(newEnemyTimer);
            bestEnemy++;
            newEnemyTimer *= 2;
        }
    }

    //Generates Enemy waves
    IEnumerator SendWave()
    {
        GameObject nextEnemy;
        Vector2 spawnPoint;

        while (true)
        {
            Debug.Log("New Wave in " + waveTime +" Seconds");
            yield return new WaitForSeconds(waveTime);
            for (int i = 0; i < waveSize; i++)
            {
                Debug.Log("New Wave Inbound");
                spawnPoint = NewSpawnPosition();
                nextEnemy = enemies[Random.Range(0, bestEnemy)];
                Instantiate(nextEnemy, spawnPoint, Quaternion.identity);
                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}
