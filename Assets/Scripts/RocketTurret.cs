﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RocketTurret : MonoBehaviour
{
    [Header("Settings")]
    public int damage;
    public float range;
    public float fireRate;
    public GameObject rocket;

    [Header("Audio Clips")]
    public AudioClip launch;

    [Header("Refs")]
    public GameObject shotEffect;

    //Refs
    GameObject target;
    GameObject firePoint;
    Rigidbody2D rb;
    AudioSource audioSource;
    GameObject rangeRenderer;

    //Vars
    bool canShoot = true;

    // Start is called before the first frame update
    void Awake()
    {
        firePoint = transform.GetChild(1).gameObject;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        rangeRenderer = transform.Find("Range").gameObject;
        rangeRenderer.transform.localScale = new Vector2(range, range);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
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
                {
                    StartCoroutine("Shoot");
                }

            }
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject temp = Instantiate(rocket, firePoint.transform.position, Quaternion.identity);
        temp.GetComponent<Rocket>().target = target.transform.position;
        temp.GetComponent<Rocket>().damage = damage;

        //VFX
        Instantiate(shotEffect, firePoint.transform.position, transform.rotation);

        //SFX
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.PlayOneShot(launch);
        audioSource.pitch = 1f;

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
