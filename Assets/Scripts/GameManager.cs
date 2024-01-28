using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool isEnd = false;
    private GameObject end;
    private GameObject start;

    private float score;

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
        score = GameObject.FindWithTag("EnemyBase").gameObject.GetComponent<Ebase>().score;
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
        GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }

    private void ChangeToStart()
    {
        start.SetActive(true);
        end.SetActive(false);
    }

}
