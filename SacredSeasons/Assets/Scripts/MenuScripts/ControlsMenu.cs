using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    public void Back()
    {
        UIManager.instance.ShowScreen("Main Menu");
    }
}
