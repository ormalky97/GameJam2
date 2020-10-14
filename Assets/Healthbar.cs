using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public float health;
    public float maxHealth;

    GameObject fill;
    float xScale;

    private void Awake()
    {
        fill = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        xScale = (health / maxHealth) * 1.5f;
        if (xScale < 0)
            xScale = 0;

        fill.transform.localScale = new Vector3(xScale, fill.transform.localScale.y, 1);
    }
}
