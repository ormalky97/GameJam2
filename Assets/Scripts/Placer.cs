using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placer : MonoBehaviour
{
    [Header("Settings")]
    public float maxDistance;

    [Header("Inner Vars")]
    public GameObject building = null;
    public Sites site = null;

    //Refs
    List<GameObject> stations;
    Camera cam;
    Resources res;
    SpriteRenderer spr;
    Messages tooltip;

    //vars
    string reason;

    private void Awake()
    {
        tooltip = GameObject.FindObjectOfType<Messages>();
        stations = GameObject.FindObjectOfType<BuildingsManager>().stations;
        spr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        res = GameObject.Find("Game Manager").GetComponent<Resources>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        site = building.GetComponent<Sites>();
        spr.sprite = building.GetComponent<SpriteRenderer>().sprite;

        foreach (GameObject station in stations)
        {
            station.GetComponent<Station>().ShowRange();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Mathf.Round(cam.ScreenToWorldPoint(Input.mousePosition).x), Mathf.Round(cam.ScreenToWorldPoint(Input.mousePosition).y));
        if(CheckDistance())
        {
            if (CheckPlace())
            {
                spr.color = new Color(1, 1, 1, 0.75f);
                if (Input.GetMouseButtonDown(0))
                {
                    Build();
                }
            }
            else
            {
                spr.color = new Color(1, 0, 0, 0.75f);
                if (Input.GetMouseButtonDown(0))
                {
                    tooltip.ShowMessage(reason, new Color(1, 1, 1, 1));
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            spr.color = new Color(1, 0, 0, 0.75f);
            if(Input.GetMouseButtonDown(0))
                tooltip.ShowMessage("Can't build so far from a station", new Color(1, 1, 1, 1));
        }
        
    }

    bool CheckDistance()
    {
        GameObject closetStation = FindClosetStation();
        if (Vector2.Distance(transform.position, closetStation.transform.position) <= maxDistance)
            return true;
        else
            return false;
    }

    GameObject FindClosetStation()
    {
        GameObject closetStation = null;
        float minDistance = 100f;
        float distance;

        foreach (GameObject station in stations)
        {
            distance = Vector2.Distance(transform.position, station.transform.position);
            if (distance < minDistance)
            {
                closetStation = station;
                minDistance = distance;
            }
        }

        return closetStation;
    }

    void Build()
    {
        Instantiate(building, transform.position, Quaternion.identity);
        res.DecreaseResources(site.foodCost, site.oilCost, site.metalCost, site.populationUsage);
        Destroy(gameObject);
    }

    bool CheckPlace()
    {
        Collider2D hit = Physics2D.OverlapPoint(transform.position);
        if (hit == null)
            return true;
        else
        {
            reason = "Blocked by another building";
            return false;
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject station in stations)
        {
            station.GetComponent<Station>().HideRange();
        }
    }
}
