using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorMove : MonoBehaviour
{
    public GameObject innerDoor1;
    public GameObject innerDoor2;
    public GameObject outerDoor1;
    public GameObject outerDoor2;
    public GameObject player;
    static bool inside = true;
    float distance;
    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
    }

    private void OnMouseDown()
    {
        if (inside && distance<2)
        {
            int x = 0;
            while (x < 100) { 
                innerDoor1.transform.Translate(0, 0, .01f);
                innerDoor2.transform.Translate(0, 0, .01f);
                outerDoor1.transform.Translate(0, 0, -.01f);
                outerDoor2.transform.Translate(0, 0, -.01f);
                x++;
            }
            inside = false;
        }
        else if (!inside && distance<2)
        {
            int x = 0;
            while (x < 100)
            {
                innerDoor1.transform.Translate(0, 0, -.01f);
                innerDoor2.transform.Translate(0, 0, -.01f);
                outerDoor1.transform.Translate(0, 0, .01f);
                outerDoor2.transform.Translate(0, 0, .01f);
                x++;
            }
            inside = true;
        }
    }
}
//https://www.reddit.com/r/godot/comments/uvu1l2/comment/i9os0l8/
//https://medium.com/geekculture/unity-shorts-2-clicking-on-a-3d-object-2f37db248f7d