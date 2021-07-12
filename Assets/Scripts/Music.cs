using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is classfor sound and script will not destroy after load and also destroys the second instance
public class Music : MonoBehaviour
{
    void Start()
    {
        GameObject[] musicGameObjects = GameObject.FindGameObjectsWithTag("music");
        if (musicGameObjects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

}
