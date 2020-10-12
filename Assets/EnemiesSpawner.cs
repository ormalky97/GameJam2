using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public List<GameObject> enemies;

    int bestEnemy;
    int waveSize;
    float waveTimer = 60f;
    float enemyTimer = 60f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("IncreaseWaveSize");
        StartCoroutine("NewEnemy");
        StartCoroutine("SendWave");
    }

    IEnumerator IncreaseWaveSize()
    {
        int i = 0;
        while(true)
        {
            i++;
            yield return new WaitForSeconds(waveTimer);
            waveSize += 2 * i;
        }
    }

    IEnumerator NewEnemy()
    {
        int i = 0;
        while (bestEnemy < enemies.Count)
        {
            i++;
            yield return new WaitForSeconds(enemyTimer);
            bestEnemy++;
            enemyTimer *= 2;
        }
    }

    IEnumerator SendWave()
    {
        GameObject nextEnemy;
        Vector2 spawnPoint;

        while (true)
        {
            yield return new WaitForSeconds(60f);
            for (int i = 0; i < waveSize; i++)
            {
                nextEnemy = enemies[Random.Range(0, bestEnemy)];
                Instantiate(nextEnemy, spawnPoint, Quaternion.identity);
                yield return new WaitForSeconds(0.25f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
