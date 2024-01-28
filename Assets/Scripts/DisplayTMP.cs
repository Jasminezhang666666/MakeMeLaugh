using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTMP : MonoBehaviour
{
    public TextMeshProUGUI SCORE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SCORE.text = GameObject.Find("GameManager").GetComponent<GameManager>().score.ToString();
    }
}
