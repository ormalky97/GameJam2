using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Background Movemnt")]
    public RawImage background;
    public float xFactor;
    public float yFactor;

    [Header("Background Buildings")]
    public List<Sprite> buildings;
    public GameObject bgBuilding;
    public int xSpawn;
    public float xDestroy;
    public float yMax;
    public float speed;

    //Vars
    List<GameObject> activeBuildings;

    private void Awake()
    {
        activeBuildings = new List<GameObject>();
    }

    private void Start()
    {
        StartCoroutine("NewBuildings");
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        //Move background
        float newX = background.uvRect.x + xFactor * Time.deltaTime;
        float newY = background.uvRect.y + yFactor * Time.deltaTime;
        background.uvRect = new Rect(newX, newY, background.uvRect.width, background.uvRect.height);

        //Move Buildings
        foreach(GameObject building in activeBuildings)
        {
            if (building.transform.position.x > xDestroy)
                building.transform.position = new Vector2(building.transform.position.x - speed * Time.deltaTime, building.transform.position.y);
            else
            {
                activeBuildings.Remove(building);
                Destroy(building);
            }  
        }
    }

    IEnumerator NewBuildings()
    {
        yield return new WaitForSeconds(Random.Range(0f, 10f));
        Collider2D hit;
        Vector2 spawnPos;
        do {
            spawnPos = new Vector2Int(xSpawn, Mathf.CeilToInt(Random.Range(yMax * -1, yMax)));
            hit = Physics2D.OverlapPoint(spawnPos, LayerMask.GetMask("BG Building"));
        } while (hit != null);
        GameObject temp = Instantiate(bgBuilding, spawnPos, Quaternion.identity);
        temp.GetComponent<SpriteRenderer>().sprite = buildings[Random.Range(0, buildings.Count)];
        activeBuildings.Add(temp);
        StartCoroutine("NewBuildings");
    }
}
