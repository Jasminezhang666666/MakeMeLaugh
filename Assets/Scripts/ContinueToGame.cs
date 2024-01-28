using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueToGame : MonoBehaviour
{
    public Button BtnContinue;
    // Start is called before the first frame update
    void Start()
    {
         BtnContinue.onClick.AddListener(Transition);
    }

    void Transition()
    {
        if (GameObject.Find("GameManager")!= null)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().PlayGameScene();
        }

    }

}
