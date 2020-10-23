using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
        tooltip = FindObjectOfType<Messages>();
        stations = FindObjectOfType<BuildingsManager>().stations;
        spr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        res = GameObject.Find("Game Manager").GetComponent<Resources>();
    }
    
    void Start()
    {
        site = building.GetComponent<Sites>();
        spr.sprite = building.GetComponent<SpriteRenderer>().sprite;

        FindObjectOfType<CameraController>().canDrag = false;

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

        if (transform.position.y < cam.transform.position.y - cam.orthographicSize + add)
            return true;
        else
            return false;
    }

    void SetPosition()
    {
        float x = Mathf.Round(cam.ScreenToWorldPoint(Input.mousePosition).x);
        float y = Mathf.Round(cam.ScreenToWorldPoint(Input.mousePosition).y);
        transform.position = new Vector3(x, y, 0f);
        //Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
        bool protect = GuiProtection();
        spr.enabled = !protect;

        if (Input.GetMouseButtonDown(0) && protect)
            Destroy(gameObject);

        if (Input.GetMouseButtonDown(1))
            Destroy(gameObject);

        if (CheckDistance())
        {
            if (CheckPlace())
            {
                spr.color = new Color(1, 1, 1, 0.75f);
                if (Input.GetMouseButtonDown(0) && !protect)
                {
                    Build();
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
            if(station.GetComponent<Sites>().active)
            {
                distance = Vector2.Distance(transform.position, station.transform.position);
                if (distance < minDistance)
                {
                    closetStation = station;
                    minDistance = distance;
                }
            }
        }

        return closetStation;
    }

    void Build()
    {
        Instantiate(building, transform.position, Quaternion.identity);
        res.DecreaseResources(site.foodCost, site.metalCost, site.oilCost, site.populationUsage, 0, 0);
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
