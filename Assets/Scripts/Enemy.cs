using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public int type;
    public int damage;
    public int maxHealth;
    public float moveSpeed;
    public float viewRange;

    GameObject target;
    Rigidbody2D rb;

    int health;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Colony Center");
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            target = GameObject.Find("Colony Center");

        FindTarget();
        GoToTarget();

        if(Vector2.Distance(target.transform.position, transform.position) <= 1f)
        {
            target.GetComponent<Building>().RecieveDamage(damage);
        }
    }

    public void RecieveDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void FindTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, viewRange, LayerMask.GetMask("Building"));
        if (hit != null)
            target = hit.gameObject;
    }

    void GoToTarget()
    {
        Vector2 dir = target.transform.position - transform.position;
        rb.velocity = dir.normalized * moveSpeed;
    }
}
