using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Game_Over();
    }

    void Game_Over()
    {
        if (is_over)
        {
            if (cur_turn % 2 == 0)
            {
                CanvasManager.turn_info = "water win";
            }
            else
            {
                CanvasManager.turn_info = "forest win";
            }
            
        }
    }
    
    public void Restart()
    {
        is_over = false;
        cur_turn = 0;
        SceneManager.LoadScene(0);
    }
    
}
