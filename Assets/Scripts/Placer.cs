using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placer : MonoBehaviour
{
    [Header("Settings")]
    public float maxDistance;

    [Header("Inner Vars")]
    public GameObject building;
    public Sites site;

    //Refs
    Camera cam;
    Resources res;
    SpriteRenderer spr;
    Messages tooltip;

    //vars
    string reason;

    private void Awake()
    {
        tooltip = GameObject.FindObjectOfType<Messages>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.sprite = building.GetComponent<SpriteRenderer>().sprite;

        cam = Camera.main;
        res = GameObject.Find("Game Manager").GetComponent<Resources>();
        site = building.GetComponent<Sites>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Mathf.Round(cam.ScreenToWorldPoint(Input.mousePosition).x), Mathf.Round(cam.ScreenToWorldPoint(Input.mousePosition).y));

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

        if(Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
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

}
