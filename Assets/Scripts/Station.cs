using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    GameObject rangeRenderer;
    float range = 10f;

    private void Awake()
    {
        rangeRenderer = transform.GetChild(0).gameObject;
        rangeRenderer.transform.localScale = new Vector2(range, range);
    }

    public void ShowRange()
    {
        if(GetComponent<Sites>().active)
            rangeRenderer.SetActive(true);
    }

    public void HideRange()
    {
        if (GetComponent<Sites>().active)
            rangeRenderer.SetActive(false);
    }
}
