﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    [Header("Inner Vars")]
    public GameObject building;
    public Sites site;

    //Refs
    Camera cam;
    Resources res;
    SpriteRenderer spr;

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
            spr.color = new Color(1, 1, 1);
            if (Input.GetMouseButtonDown(0))
            {
                {
                    Instantiate(building, transform.position, Quaternion.identity);
                    res.DecreaseResources(site.foodCost, site.oilCost, site.metalCost, site.populationUsage);
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            spr.color = new Color(1, 0, 0);
        }

        if(Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }

    bool CheckPlace()
    {
        Collider2D hit = Physics2D.OverlapPoint(transform.position);
        if (hit == null)
            return true;
        else
            return false;
    }

}
