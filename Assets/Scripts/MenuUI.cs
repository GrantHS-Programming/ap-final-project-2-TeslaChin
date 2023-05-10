using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    private Canvas CanvasObject;
    public static bool menuOpen = false;

    void Start()
    {
        CanvasObject = GetComponent<Canvas>();
        CanvasObject.enabled = !CanvasObject.enabled;
        Cursor.lockState = CursorLockMode.Locked;
        //SceneManager.LoadScene("Open");

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CanvasObject.enabled = !CanvasObject.enabled;
            menuOpen = !menuOpen;
        }
    }
    private void FixedUpdate()
    {

        if (menuOpen)
            Cursor.lockState = CursorLockMode.None;
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

}
//https://forum.unity.com/threads/save-camera-position-between-scene.914831/ : Camera position for scene change