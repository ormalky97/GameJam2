using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public int type;
    public float moveSpeed;
    public float viewRange;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        GoToTarget();
    }

    void FindTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, viewRange, LayerMask.GetMask("Buildings"));
        if (hit != null)
            target = hit.gameObject;
    }

    void GoToTarget()
    {
        Vector2 dir = target.transform.position - transform.position;
        rb.velocity = dir.normalized * moveSpeed;
    }
}
