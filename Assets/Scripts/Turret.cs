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

    [Header("Audio Clips")]
    public AudioClip shoot;

    [Header("Refs")]
    public GameObject shotEffect;

    //Refs
    GameObject target;
    GameObject firePoint;
    Rigidbody2D rb;
    AudioSource audioSource;

    //Vars
    bool canShoot = true;
    bool shotSide = true;

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
                {
                    StartCoroutine("Shoot");
                }
                      
            }
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        //Deal damage to target
        target.GetComponent<Enemy>().RecieveDamage(damage);

        //VFX
        Instantiate(shotEffect, firePoint.transform.position, transform.rotation);
        GetComponent<Animator>().SetTrigger("Shoot");
        GetComponent<Animator>().SetBool("ShootSide", shotSide);
        shotSide = !shotSide;

        //SFX
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.PlayOneShot(shoot);
        audioSource.pitch = 1f;

        //Reset canShoot
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
