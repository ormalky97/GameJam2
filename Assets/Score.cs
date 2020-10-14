using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text timer;
    public Text radius;

    public void UpdateRadius(float maxRad)
    {
        radius.text = "" + Mathf.RoundToInt(maxRad);
    }

    void Timer()
    {
        timer.text = "";
        int hr = 0;
        int min = 0;
        int sec = Mathf.CeilToInt(Time.time);

        while(sec >= 60)
        {
            min++;
            sec -= 60;
        }
        while(min >= 60)
        {
            hr++;
            min -= 60;
        }

        if (hr <= 9)
            timer.text += "0" + hr + ":";
        else
            timer.text += hr + ":";

        if (min <= 9)
            timer.text += "0" + min + ":";
        else
            timer.text += min + ":";

        if (sec <= 9)
            timer.text += "0" + sec;
        else
            timer.text += sec;
    }

    void Update()
    {
        Timer();
    }
}
