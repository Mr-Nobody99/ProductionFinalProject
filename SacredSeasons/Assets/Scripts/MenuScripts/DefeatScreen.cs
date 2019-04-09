using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatScreen : MonoBehaviour
{
    public void MainMenu()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.confirm);
        UIManager.instance.ShowScreen("Main Menu");
        UIManager.instance.Restart();
    }

    public void Quit()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.back);
        UIManager.instance.Quit();
    }
}
