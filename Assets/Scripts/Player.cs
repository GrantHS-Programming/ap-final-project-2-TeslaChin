using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [SerializeField] private Transform checkGround = null;
    [SerializeField] private LayerMask playerMask;
    bool left;
    bool right;
    bool forward;
    bool backward;
    bool spacePressed;
    float mouseSpeed = 4.0f;
    public Rigidbody player;

    public Transform cam;
    Vector2 input;

    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            spacePressed = true;
        if (Input.GetKey(KeyCode.E))
            forward = true;
        if (Input.GetKey(KeyCode.D))
            backward = true;
        if (Input.GetKey(KeyCode.S))
            left = true;
        if (Input.GetKey(KeyCode.F))
            right = true;
        

        if (!MenuUI.menuOpen && StartUI.started)
        {
            float mouse = mouseSpeed * Input.GetAxis("Mouse X");
            transform.Rotate(0, mouse, 0);
        }

    }
    void FixedUpdate()
    {

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        if (!MenuUI.menuOpen && StartUI.started) {
            if (forward)
            {
                GetComponent<Rigidbody>().AddForce(camF / 2, ForceMode.VelocityChange);
                forward = false;
            }
            if (backward)
            {
                GetComponent<Rigidbody>().AddForce(-camF / 2, ForceMode.VelocityChange);
                backward = false;
            }
            if (right)
            {
                GetComponent<Rigidbody>().AddForce(camR / 2, ForceMode.VelocityChange);
                right = false;
            }
            if (left)
            {
                GetComponent<Rigidbody>().AddForce(-camR / 2, ForceMode.VelocityChange);
                left = false;
            }

            if (Physics.OverlapSphere(checkGround.position, 0.1f, playerMask).Length == 1)
                spacePressed = false;
            if (spacePressed)
                player.velocity = new Vector3(0, 5, 0);
        }

    }

}
