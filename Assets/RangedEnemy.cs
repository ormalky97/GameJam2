using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public int damage;
    public int maxHealth;
    public float moveSpeed;
    public float viewRange;
    public float attackDistance;
    public float attackRate;

    GameObject target;
    Rigidbody2D rb;

    int health;
    bool canAttack = true;

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

        if(Vector2.Distance(transform.position, target.transform.position) <= attackDistance)
        {
            rb.velocity = new Vector2(0, 0);
            if (canAttack)
                StartCoroutine("Shoot");
        }
        else
        {
            GoToTarget();
        }
    }
    IEnumerator Shoot()
    {
        canAttack = false;
        //create shot
        yield return new WaitForSeconds(1 / attackRate);
        canAttack = true;
    }

    public void RecieveDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    void GoToTarget()
    {
        Vector2 dir = target.transform.position - transform.position;
        rb.velocity = dir.normalized * moveSpeed;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
