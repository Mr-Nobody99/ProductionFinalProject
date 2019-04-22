using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    public void Back()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.back);
        UIManager.instance.ShowScreen(UIManager.instance.previousScreenName);
    }
}
