using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    GameObject rangeRenderer;
    float range = 10f;

    private void Awake()
    {
        rangeRenderer = transform.GetChild(1).gameObject;
        rangeRenderer.transform.localScale = new Vector2(range, range);
    }

    public bool CheckInRange(GameObject toCheck)
    {
        if (Vector2.Distance(transform.position, toCheck.transform.position) <= range/2)
            return true;
        else
            return false;
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

    private void OnDestroy()
    {
        FindObjectOfType<BuildingsManager>().stations.Remove(gameObject);
    }
}
