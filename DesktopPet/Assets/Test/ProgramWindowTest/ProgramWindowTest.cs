using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgramWindowTest : MonoBehaviour
{

    public Button Btn;
    public GameObject obj;


    bool canMove = false;
    bool setActive = true;

    void Start()
    {
        Btn.onClick.AddListener(Test_BtnClick);

#if !UNITY_EDITOR
           ProgramWindowManager.getInstance().Initialized();
           ProgramWindowManager.getInstance().MoveWindow();
#endif
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            obj.SetActive(setActive);
            setActive = !setActive;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            canMove = !canMove;
        }

#if !UNITY_EDITOR
        if (canMove)
        {
          ProgramWindowManager.getInstance().MoveWindow();
        }
#endif

    }

    public void Test_BtnClick()
    {
        obj.SetActive(setActive);
        setActive = !setActive;
    }
}
