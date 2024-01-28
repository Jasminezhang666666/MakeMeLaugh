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

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
        }
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
        
        isEnd = true;
        ChangeToEnd();
        EndAgain();

    }

    public void Reset()
    {
        isEnd = false;
        ChangeToStart();
    }
    public void ChangeToEnd()
    {
        SceneManager.LoadScene("Ending");

        //end = GameObject.Find("End");
        //start = GameObject.Find("Start");

        //start.SetActive(false);
        //end.SetActive(true);

        GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }

    private void ChangeToStart()
    {
        start.SetActive(true);
        end.SetActive(false);
    }

    public void EndAgain()
    {
        GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        print(GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text);
    }

}
