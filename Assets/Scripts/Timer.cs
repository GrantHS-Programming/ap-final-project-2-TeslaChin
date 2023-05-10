using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    static int sol = 0;
    static int hour = 0;
    static double minute = 0;
    [SerializeField]private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro.text = "Some text";
    }


    void FixedUpdate()
    {
        if (minute < 10)
            textMeshPro.text = "Sol " + sol + " " + hour + ":" + "0" + (int)minute;
        else
            textMeshPro.text = "Sol " + sol + " " + hour + ":" + (int)minute;
        if (!MenuUI.menuOpen)
        {

            if (minute < 60)
                minute += Time.deltaTime;
            else
            {
                minute = 0;
                if (hour < 24)
                {
                    hour++;
                }
                else
                {
                    hour = 0;
                    sol++;
                }
                
            }
            
        }

    }
}
