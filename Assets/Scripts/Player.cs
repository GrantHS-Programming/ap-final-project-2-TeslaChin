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
    int health = 2200;
    int coll = 0;
    public GameObject bar;
    public Transform cam;
    Vector2 input;
    public static bool dead = false;

    void Start()
    {
        player = GetComponent<Rigidbody>();
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
        

        if (!MenuUI.menuOpen && !dead /*&& StartUI.started*/)
        {
            float mouse = mouseSpeed * Input.GetAxis("Mouse X");
            transform.Rotate(0, mouse, 0);
        }
        
        if (health <= 0)
        {
            StartCoroutine(waiter());
        }
    }
    IEnumerator waiter()
    {
        dead = true;
        player.constraints = RigidbodyConstraints.None;
        //Time.timeScale = .5f;
        yield return new WaitForSeconds(4);
        //player.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;

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

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        //if (!MenuUI.menuOpen /*&& StartUI.started*/)
        {
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

            //if (coll)
            {
                health-=coll*20;
                var barRectTransform = bar.transform as RectTransform;
                barRectTransform.sizeDelta = new Vector2(health, barRectTransform.sizeDelta.y);
            }

            if (Physics.OverlapSphere(checkGround.position, 0.1f, playerMask).Length == 1)
                spacePressed = false;
            if (spacePressed)
                player.velocity = new Vector3(0, 5, 0);
        }

    }

}
