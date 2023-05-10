using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    float mouseSpeed = -4.0f;
    float facing;
    public GameObject cam;

   
    void Update()
    {

        if (!MenuUI.menuOpen && StartUI.started)
        {
            facing = cam.transform.localRotation.eulerAngles.x;
            float v = mouseSpeed * Input.GetAxis("Mouse Y");

            if (!(((facing <= 280) && (facing >= 270)) && (v < 0)) && !(((facing <= 90) && (facing >= 80)) && (v > 0)))
            {
                transform.Rotate(v, 0, 0);
            }
        }
    }
}
