using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCategories : MonoBehaviour
{
    GameObject[] panels;

    void Start()
    {
        panels = GameObject.FindGameObjectsWithTag("Categories");
        for(int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }
}
