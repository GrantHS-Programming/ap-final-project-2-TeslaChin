using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Clickable : MonoBehaviour
{
    AsyncOperation asyncLoad;
    private void OnMouseDown()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        if (SceneManager.GetActiveScene().name.Equals("Open"))
            asyncLoad = SceneManager.LoadSceneAsync("Main");
        else
            asyncLoad = SceneManager.LoadSceneAsync("Open");



        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
//https://www.reddit.com/r/godot/comments/uvu1l2/comment/i9os0l8/
//https://medium.com/geekculture/unity-shorts-2-clicking-on-a-3d-object-2f37db248f7d