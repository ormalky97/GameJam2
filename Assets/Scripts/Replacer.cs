using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replacer : MonoBehaviour
{
    [Header("Settings")]
    public float maxDistance;

    [Header("Inner Vars")]
    public string targetTitle;
    public GameObject building = null;
    public Sites site = null;

    //Refs
    List<GameObject> stations;
    GameObject oldBuilding;
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
    bool GuiProtection()
    {
        int add = 1;
        GameObject temp = FindObjectOfType<BuildCategory>().GetActiveCategory();
        if (temp != null)
            add = 2;
        Debug.Log(transform.position.y);
        if (transform.position.y < cam.transform.position.y - cam.orthographicSize + add)
            return true;
        else
            return false;
    }

    // Update is called once per frame
    void Update()
    {
        bool protect = GuiProtection();
        transform.position = new Vector2(Mathf.Round(cam.ScreenToWorldPoint(Input.mousePosition).x), Mathf.Round(cam.ScreenToWorldPoint(Input.mousePosition).y));
        spr.enabled = !protect;

        if (Input.GetMouseButtonDown(0) && protect)
            Destroy(gameObject);

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }

        if (CheckDistance())
        {
            if (CheckPlace())
            {
                spr.color = new Color(1, 1, 1, 0.75f);
                if (Input.GetMouseButtonDown(0))
                {
                    Replace();
                }
            }
            else
            {
                spr.color = new Color(1, 0, 0, 0.75f);
                if (Input.GetMouseButtonDown(0) && !protect)
                {
                    tooltip.ShowMessage(reason, new Color(1, 1, 1, 1));
                }
            }       
        }
        else
        {
            spr.color = new Color(1, 0, 0, 0.75f);
            if (Input.GetMouseButtonDown(0) && !protect)
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

    void Replace()
    {
        //Soft destroy old building and get population usafe diff
        Sites targetSite = oldBuilding.GetComponent<Sites>();
        targetSite.replaced = true;
        targetSite.usageDiff = building.GetComponent<Sites>().populationUsage - targetSite.populationUsage;
        targetSite.maxDiff = building.GetComponent<Sites>().populationAdd - targetSite.populationAdd;
        Destroy(oldBuilding);

        //Create new building
        Instantiate(building, transform.position, Quaternion.identity);
        res.DecreaseResources(site.foodCost, site.oilCost, site.metalCost, site.populationUsage, 0, 0);
        Destroy(gameObject);
    }

    bool CheckPlace()
    {
        Collider2D hit = Physics2D.OverlapPoint(transform.position);
        if (hit != null)
        {
            if(hit.GetComponent<Sites>().title == targetTitle)
            {
                oldBuilding = hit.gameObject;
                return true;
            }
            else
            {
                reason = "Must be placed on " + targetTitle;
                return false;
            }
        }
        else
        {
            reason = "Must be placed on " + targetTitle;
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
