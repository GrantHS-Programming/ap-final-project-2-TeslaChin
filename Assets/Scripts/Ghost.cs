using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameObject ghost;
    static int random = 15;
    public GameObject target;
    int xDistance;
    int zDistance;
    float num;
    Vector3 distance;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void FixedUpdate()
    {
        
        if ((int)Timer.counter >= random)
        {
            //house x: -4 to -9
            //house z: 1 to 5

            xDistance = Random.Range(3, 10)+ Random.Range(3, 10);
            zDistance = Random.Range(3, 10) + Random.Range(3, 10);
            num = Random.Range(-1f, 1f);
            num /= Mathf.Abs(num);
            xDistance *= (int)num;
            num = Random.Range(-1f, 1f);
            num /= Mathf.Abs(num);
            zDistance *= (int)num;
            distance = new Vector3(xDistance, Random.Range(0f, 3f), zDistance);
            while (((target.transform.position+distance).x<-4 && (target.transform.position + distance).x>-9 && (target.transform.position + distance).y<5 && (target.transform.position + distance).y > 1))
            {
                xDistance = Random.Range(3, 10) + Random.Range(3, 10);
                zDistance = Random.Range(3, 10) + Random.Range(3, 10);
                num = Random.Range(-1f, 1f);
                num /= Mathf.Abs(num);
                xDistance *= (int)num;
                num = Random.Range(-1f, 1f);
                num /= Mathf.Abs(num);
                zDistance *= (int)num;
                distance = new Vector3(xDistance, Random.Range(0f, 3f), zDistance);
            }
            random += Random.Range(15, 20);
            Instantiate(ghost, target.transform.position + distance, Quaternion.identity);
        }
        var step = Time.deltaTime *2;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        transform.LookAt(target.transform);
    }

}
