using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messages : MonoBehaviour
{
    public float messageTime;   //How long to display before decay starts
    public float decayFactor;   //How fast to decay

    //Refs
    Text ui;

    //vars
    bool startDecay = false;

    // Start is called before the first frame update
    void Awake()
    {
        ui = transform.GetChild(0).GetComponent<Text>();
        ui.gameObject.SetActive(false);
    }

    public void ShowMessage(string message, Color color)
    {
        ui.gameObject.SetActive(true);
        ui.text = message;
        ui.color = color;
        StartCoroutine("Decay", messageTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (startDecay)
        {
            float alpha = ui.color.a;
            if (alpha > 0)
            {
                ui.color = new Color(ui.color.r, ui.color.g, ui.color.b, alpha - decayFactor * Time.deltaTime);
            }
            else
            {
                startDecay = false;
                ui.gameObject.SetActive(false);
            }
        }

    }

    IEnumerator Decay(float showTime)
    {
        yield return new WaitForSeconds(showTime);
        startDecay = true;
    }
}
