using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Options()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.confirm);
        UIManager.instance.ShowScreen("Options Menu");
    }

    public void Quit()
    {
        UIManager.instance.Quit();
    }

    public void PlayGame()
    {
        UIManager.instance.PlayGame();
    }

    public void Controls()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.confirm);
        UIManager.instance.ShowScreen("Controls Menu");
    }
}
