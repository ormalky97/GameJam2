﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    public int damage;
    public int maxHealth;
    public float moveSpeed;
    public float viewRange;
    public float attackDistance;
    public float attackRate;

    [Header("Pref Target")]
    public GameObject prefTarget;
    public bool onlyPref = false;

    [Header("Hit Effect")]
    public GameObject hitEffect;

    [Header("Hit Effect")]
    public AudioClip attackSound;
    public AudioClip scarySpawn;


    //Refs
    GameObject target;
    Rigidbody2D rb;

    //Vars
    int health;
    bool canAttack = true;
    bool isTouching = false;

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<AudioSource>().PlayOneShot(scarySpawn, 1f);
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(target);

        if (onlyPref)
            FindPrefTarget();
        else
            FindTarget();

        if (target == null)
        {
            //Move Toawrds Colony Center
            Vector2 dir = Vector3.zero - transform.position;
            rb.AddForce(dir.normalized * moveSpeed * Time.deltaTime);
        }
        else
        {
            if (Vector2.Distance(target.transform.position, transform.position) <= attackDistance || isTouching)
            {
                //rb.velocity = new Vector2(0, 0);
                if (canAttack)
                    StartCoroutine("Attack");
            }
            else
                GoToTarget();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (target == collision.gameObject)
            isTouching = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (target == collision.gameObject)
            isTouching = false;
    }

    IEnumerator Attack()
    {
        GetComponent<AudioSource>().PlayOneShot(attackSound, 1f);
        canAttack = false;
        target.GetComponent<Sites>().RecieveDamage(damage);
        yield return new WaitForSeconds(1 / attackRate);
        canAttack = true;
    }

    public void RecieveDamage(int amount)
    {
        health -= amount;
        Bleed();
        if (health <= 0)
            Die();
    }

    void Bleed()
    {
        float x = transform.position.x + Random.Range(-0.2f, 0.2f);
        float y = transform.position.y + Random.Range(-0.2f, 0.2f);
        Vector2 pos = new Vector2(x, y);

        GameObject temp = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(temp, 2f);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    //search for pref target only
    void FindPrefTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, viewRange, LayerMask.GetMask("Building"));
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(target.tag))
                target = hit.gameObject;
        }
    }

    //Search for targets noramlly
    void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, viewRange, LayerMask.GetMask("Building"));
        foreach(Collider2D hit in hits)
        {
            if (target == null)
                target = hit.gameObject;
            else
            {
                if (prefTarget != null && hit.CompareTag(target.tag))
                {
                    target = hit.gameObject;
                    return;
                }
            }
        }
    }

    void GoToTarget()
    {
        Vector2 dir = target.transform.position - transform.position;
        rb.AddForce(dir.normalized * moveSpeed * Time.deltaTime);
    }
}
