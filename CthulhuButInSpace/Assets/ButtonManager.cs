
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button MainButton;

    void Start()
    {
        MainButton.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        print("Button Pressed");
    }
}