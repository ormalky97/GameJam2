using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;
    public int damage;
    public float expRadius;
    public float expForce;
    public float lifeTime;

    public Vector2 target;

    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Vector2 dir = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        rb.SetRotation(angle);
        rb.velocity = dir.normalized * speed;

        Invoke("Explode", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target) < 0.5f)
            Explode();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
            Explode();
    }

    void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, expRadius, LayerMask.GetMask("Enemies"));
        foreach(Collider2D hit in hits)
        {
            hit.GetComponent<Enemy>().RecieveDamage(damage);

            Vector2 dir = (hit.transform.position - transform.position)/Vector2.Distance(transform.position, hit.transform.position);
            hit.GetComponent<Rigidbody2D>().AddForce(dir * expForce, ForceMode2D.Impulse);
        }
        //sfx
        //vfx

        Destroy(gameObject);
    }
}
