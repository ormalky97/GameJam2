using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    public GameObject building;
    public int foodCost;
    public int moneyCost;
    public int materialCost;
    public int populationCost;

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
                    DecreaseResources();
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
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void DecreaseResources()
    {
        res.food -= foodCost;
        res.money -= moneyCost;
        res.materials -= materialCost;
        res.population += populationCost;
    }
}
