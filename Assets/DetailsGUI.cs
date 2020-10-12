using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsGUI : MonoBehaviour
{
    public GameObject building = null;

    bool mouseOut;

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(building.transform.position);

        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }
}
