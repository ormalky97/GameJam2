using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SniperTurret : MonoBehaviour
{
    [Header("Settings")]
    public int damage;
    //public float range;
    public float fireRate;

    [Header("Audio Clips")]
    public AudioClip shoot;

    [Header("Refs")]
    public GameObject shotEffect;
    public LineRenderer lineRenderer;

    //Refs
    GameObject target;
    GameObject firePoint;
    Rigidbody2D rb;
    AudioSource audioSource;
    

    //Vars
    bool canShoot = true;

    // Start is called before the first frame update
    void Awake()
    {
        firePoint = transform.GetChild(1).gameObject;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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
            SetRotation();
            if (canShoot && FindObjectOfType<Resources>().metal != 0)
            {
                StartCoroutine("Shoot");
            }
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        Vector2 dir = target.transform.position - transform.position;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, 100f, LayerMask.GetMask("Enemies"));
        foreach(RaycastHit2D hit in hits)
        {
            hit.collider.GetComponent<Enemy>().RecieveDamage(damage);
        }

        //VFX
        Instantiate(shotEffect, firePoint.transform.position, transform.rotation);
        lineRenderer.SetPosition(0, Vector2.zero);
        lineRenderer.SetPosition(1, transform.up);

        lineRenderer.enabled = true;
        yield return 0;
        //lineRenderer.enabled = false;


        //SFX
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.PlayOneShot(shoot);
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
        GameObject temp = GameObject.FindGameObjectWithTag("Enemy");
        if (temp != null)
            target = temp;
    }
}
