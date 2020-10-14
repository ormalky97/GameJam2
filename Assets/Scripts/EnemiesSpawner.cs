using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    public int waveSize;
    public int initialWaveTime;
    public float waveTime;
    public float waveIncreaseTimer = 60f;
    public float newEnemyTimer = 60f;

    [Header("Enemies")]
    public List<GameObject> enemies;

    //Inner Vars
    int bestEnemy = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("IncreaseWaveSize");
        StartCoroutine("NewEnemy");
        StartCoroutine("WavesManager");
    }

    IEnumerator WavesManager()
    {
        yield return new WaitForSeconds(initialWaveTime);

        while(true)
        {
            Debug.Log("New Wave Inbound");
            StartCoroutine("SendWave");
            yield return new WaitForSeconds(waveTime);
        }
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
            waveTime += 1f;
        }
    }

    //Inroduces New Enemies
    IEnumerator NewEnemy()
    {
        while (bestEnemy < enemies.Count)
        {
            yield return new WaitForSeconds(newEnemyTimer);
            bestEnemy++;
            Debug.Log("New enemy type");
            newEnemyTimer *= 2;
        }
    }

    //Generates Enemy waves
    IEnumerator SendWave()
    {
        GameObject nextEnemy;
        Vector2 spawnPoint;

        Debug.Log("New Wave Inbound");
        for (int i = 0; i < waveSize; i++)
        {
            spawnPoint = NewSpawnPosition();
            int rand = Random.Range(0, bestEnemy + 1);
            nextEnemy = enemies[rand];
            Instantiate(nextEnemy, spawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
