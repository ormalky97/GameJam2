using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRange : MonoBehaviour
{
    GameObject rangeRenderer;
    void Awake()
    {
        rangeRenderer = transform.Find("Range").gameObject;
    }
    void OnMouseEnter()
    {
        Show();
    }

    void OnMouseExit()
    {
        Hide();
    }

    public void Show()
    {
        if (GetComponent<Sites>().active)
            rangeRenderer.SetActive(true);
    }

    public void Hide()
    {
        if (GetComponent<Sites>().active)
            rangeRenderer.SetActive(false);
    }
}
