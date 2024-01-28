using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueReset : MonoBehaviour
{
    public Button BtnReset;
    // Start is called before the first frame update
    void Start()
    {
        BtnReset.onClick.AddListener(Transition);
    }

    void Transition()
    {
        if (GameObject.Find("GameManager") != null)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().Reset();
        }

    }
}
