using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int cur_turn = 0;
    public static bool is_over = false;



    // Start is called before the first frame update
    void Start()
    {
        is_over = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Game_Over();
    }

    void Game_Over()
    {
        if (is_over)
        {
            Debug.Log("Game Over");
        }
    }
}
