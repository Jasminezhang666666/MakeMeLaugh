using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueToEnd : MonoBehaviour
{
    public AudioSource audioSource;
    void Update()
    {
        if (!audioSource.isPlaying && audioSource.time > 0)
        {
            audioSource.time = 0;
            GameObject.Find("GameManager").gameObject.GetComponent<GameManager>().PlayEndScene();
        }
    }
}
