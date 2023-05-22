using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameObject ghost;
    static int random = 15;
    public GameObject target;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //random = Random.Range(15, 20);
    }
    void FixedUpdate()
    {
        if ((int)Timer.counter >= random)
        {
            random += Random.Range(15, 20);
            Instantiate(ghost, new Vector3(Random.Range(0,-8), 1.2f, Random.Range(0,-6)), Quaternion.identity);
        }
        var step = Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        transform.LookAt(target.transform);
    }

}
