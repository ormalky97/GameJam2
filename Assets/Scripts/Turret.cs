using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Settings")]
    public int damage;
    public float range;
    public float fireRate;

    [Header("Refs")]
    public GameObject shotEffect;

    //Refs
    GameObject target;
    GameObject firePoint;
    Rigidbody2D rb;

    //Vars
    bool canShoot = true;

    // Start is called before the first frame update
    void Awake()
    {
        firePoint = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            FindTarget();
        }
        else
        {
            if (Vector2.Distance(transform.position, target.transform.position) > range)
                target = null;
            else
            {
                SetRotation();
                if (canShoot && FindObjectOfType<Resources>().metal != 0)
                    StartCoroutine("Shoot");   
            }
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        Instantiate(shotEffect, firePoint.transform.position, transform.rotation);
        target.GetComponent<Enemy>().RecieveDamage(damage);
        yield return new WaitForSeconds(1 / fireRate);
        canShoot = true;
    }

    void SetRotation()
    {
        Vector2 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        rb.SetRotation(angle);
    }

    void FindTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Enemies"));
        if (hit != null)
        {
            target = hit.gameObject;
        }
    }
}
