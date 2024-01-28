using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isEnd = false;
    private GameObject end;
    private GameObject start;

    private GameObject Score;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        end = GameObject.Find("End");
        start = GameObject.Find("Start");
        ChangeToStart();
        //GameObject.Find("Pizza").GetComponent<pizza>().Launch();
    }

    public void PlayVideoScene()
    {
        SceneManager.LoadScene("Start 2");
    }

    public void PlayGameScene()
    {
        SceneManager.LoadScene("LevelScene");
    }
    public void PlayEndScene()
    {
        SceneManager.LoadScene("StartAndFinish");
        isEnd = true;
        ChangeToEnd();

    }

    public void Reset()
    {
        isEnd = false;
        ChangeToStart();
    }
    private void ChangeToEnd()
    {
        start.SetActive(false);
        end.SetActive(true);
    }

    private void ChangeToStart()
    {
        start.SetActive(true);
        end.SetActive(false);
    }

}
