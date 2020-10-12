using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryButton : MonoBehaviour
{
    public GameObject panel;
    
    public void TogglePanel()
    {
        GameObject[] panels = GameObject.FindGameObjectsWithTag("Categories");
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        panel.SetActive(true);
    }
}
