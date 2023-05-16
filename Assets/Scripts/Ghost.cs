using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameObject ghost;
    static int lim = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Timer.counter >= lim)
        {
            Instantiate(ghost, new Vector3(Random.Range(0,-8), 1.2f, Random.Range(0,-6)), new Quaternion(0, Random.Range(0f,1f), 0, 0));
            lim += Random.Range(5, 10);
        }
    }
}
