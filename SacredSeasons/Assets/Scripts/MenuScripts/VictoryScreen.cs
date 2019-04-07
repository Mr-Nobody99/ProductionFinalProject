using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public void MainMenu()
    {
        UIManager.instance.ShowScreen("Main Menu");
    }

    public void Quit()
    {
        UIManager.instance.Quit();
    }
}
