using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    private TextMeshProUGUI scale;
    private TextMeshProUGUI score;
    void Start()
    {
        score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        scale = GameObject.Find("Scale").GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {

        score.text = "Point: " + Point.GetPlayerPoint().ToString();
        scale.text = Point.GetScale().ToString();

    }
}
