using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CanvasManager : MonoBehaviour
{
    public static string turn_info = "P1";
    private TextMeshProUGUI turn_info_ui;

    // Start is called before the first frame update
    void Start()
    { 
        turn_info_ui = GameObject.Find("Turn_info").GetComponent<TextMeshProUGUI>();
        turn_info_ui.text = turn_info;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!GameManager.is_over)
        {
            if (GameManager.cur_turn % 2 == 0)
            {
                turn_info = "P1";
            }
            else if (GameManager.cur_turn % 2 == 1)
            {
                turn_info = "P2";
            }
        }
        turn_info_ui.text = turn_info;
    }
}
