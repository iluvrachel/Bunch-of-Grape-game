using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector3> pointer_click;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Dectect_Mouse_Click();
    }

    private void Dectect_Mouse_Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse_pos = Input.mousePosition;
            pointer_click?.Invoke(mouse_pos); //数据查询时，经常需要做判空处理，这里直接简化了，加了一个？号表示不为空时才执行下面的代码
        }
    }
}
