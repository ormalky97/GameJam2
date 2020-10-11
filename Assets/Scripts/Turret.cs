using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int damage;
    public float range;
    public float fireRate;

    GameObject target;
    Rigidbody2D rb;

    bool canShoot = true;

    // Start is called before the first frame update
    void Awake()
    {
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
                if (canShoot)
                    StartCoroutine("Shoot");

                SetRotation();
            }
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
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
