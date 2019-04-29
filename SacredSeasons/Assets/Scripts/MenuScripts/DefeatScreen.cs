using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatScreen : MonoBehaviour
{
    public void MainMenu()
    {
        UIManager.instance.ShowScreen("Main Menu");
        UIManager.instance.Restart();
    }

    public void Continue()
    {
        UIManager.instance.Continue();
    }

    public void Quit()
    {
        UIManager.instance.Quit();
    }
}
