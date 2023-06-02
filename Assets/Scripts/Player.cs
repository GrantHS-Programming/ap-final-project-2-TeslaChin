using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [SerializeField] private Transform checkGround = null;
    [SerializeField] private LayerMask playerMask;
    bool moveLeft;
    bool moveRight;
    bool moveForward;
    bool moveBackward;
    bool spacePressed;
    public Rigidbody player;

    float mouseSpeed = 4.0f;
    public Transform cam;
    Vector2 input;

    int health = 2200;
    int coll = 0;
    public GameObject healthBar;
    public static bool dead = false;
    public Canvas endScreen;
    public Canvas menu;

    int stamina = 2200;
    int run = 1;
    public GameObject stamBar;


    void Start()
    {
        player = GetComponent<Rigidbody>();
        endScreen.enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            spacePressed = true;
        if (Input.GetKey(KeyCode.E))
            moveForward = true;
        if (Input.GetKey(KeyCode.D))
            moveBackward = true;
        if (Input.GetKey(KeyCode.S))
            moveLeft = true;
        if (Input.GetKey(KeyCode.F))
            moveRight = true;
        if (Input.GetKey(KeyCode.W))
            run = 2;

        
        if ((health <= 0 || player.position.y <= -50) && !dead)
        {
            StartCoroutine(death());
        }
    }

    IEnumerator death()
    {
        dead = true;
        player.constraints = RigidbodyConstraints.None;
        menu.enabled = false;
        for (float i= 1; i>=0; i-=.01f)
        {
            Time.timeScale = i;
            Time.fixedDeltaTime = .02f * Time.timeScale;
            yield return new WaitForSecondsRealtime(.05f);
        }

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        endScreen.enabled = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            coll++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            coll--;
        }
    }

    void FixedUpdate()
    {
        if (!MenuUI.menuOpen && !dead /*&& StartUI.started*/)
        {
            float mouse = mouseSpeed * Input.GetAxis("Mouse X");
            transform.Rotate(0, mouse, 0);
        }

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        if (stamina <= 0)
            run = 1;
        if (run == 2 && (moveBackward || moveForward || moveLeft || moveRight))
            stamina -= 10;
        else
            stamina ++;

        if (moveForward)
        {
            GetComponent<Rigidbody>().AddForce(camF / 3 * run, ForceMode.VelocityChange);
            moveForward = false;
            run = 1;
        }
        if (moveBackward)
        {
            GetComponent<Rigidbody>().AddForce(-camF / 3 * run, ForceMode.VelocityChange);
            moveBackward = false;
            run = 1;
        }
        if (moveRight)
        {
            GetComponent<Rigidbody>().AddForce(camR / 3 * run, ForceMode.VelocityChange);
            moveRight = false;
            run = 1;
        }
        if (moveLeft)
        {
            GetComponent<Rigidbody>().AddForce(-camR / 3 * run, ForceMode.VelocityChange);
            moveLeft = false;
            run = 1;
        }

        health-=coll*20;
        var hBarRectTransform = healthBar.transform as RectTransform;
        hBarRectTransform.sizeDelta = new Vector2(health, hBarRectTransform.sizeDelta.y);
        var sBarRectTransform = stamBar.transform as RectTransform;
        sBarRectTransform.sizeDelta = new Vector2(stamina, sBarRectTransform.sizeDelta.y);
            

        if (Physics.OverlapSphere(checkGround.position, 0.1f, playerMask).Length == 1)
            spacePressed = false;
        if (spacePressed)
            player.velocity = new Vector3(0, 5, 0);
    }
    
}
