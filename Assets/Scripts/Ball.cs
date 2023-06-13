using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    
    private void OnTriggerEnter(Collider collision)
    {
        Destroy(ball);
    }
}
