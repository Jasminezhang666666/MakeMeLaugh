using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public AudioSource AS;
    public string target;

    private void Update()
    {
        if (AS.isPlaying == false) 
        {
            SceneManager.LoadScene(target);
        }
    }
}
