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
    public float attackDistance;
    public float attackRate;
    public GameObject prefTarget;
    public bool onlyPref = false;

    GameObject target;
    Rigidbody2D rb;

    int health;
    bool canAttack = true;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (onlyPref)
            FindPrefTarget();
        else
            FindTarget();

        if (target == null)
        {
            //Move Toawrds Colony Center
            Vector2 dir = Vector3.zero - transform.position;
            rb.velocity = dir.normalized * moveSpeed;
        }
        else
        {
            if (Vector2.Distance(target.transform.position, transform.position) <= attackDistance)
            {
                rb.velocity = new Vector2(0, 0);
                if (canAttack)
                    StartCoroutine("Attack");
            }
            else
                GoToTarget();
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        target.GetComponent<Sites>().RecieveDamage(damage);
        yield return new WaitForSeconds(1 / attackRate);
        canAttack = true;
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

    //search for pref target only
    void FindPrefTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, viewRange, LayerMask.GetMask("Building"));
        foreach (Collider2D hit in hits)
        {
            if (hit.tag == prefTarget.tag)
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
                if (prefTarget != null && hit.tag == prefTarget.tag)
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
        rb.velocity = dir.normalized * moveSpeed;
    }
}
