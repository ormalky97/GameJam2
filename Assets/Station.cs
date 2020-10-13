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
        rangeRenderer.SetActive(true);
    }

    public void HideRange()
    {
        rangeRenderer.SetActive(false);
    }
}
