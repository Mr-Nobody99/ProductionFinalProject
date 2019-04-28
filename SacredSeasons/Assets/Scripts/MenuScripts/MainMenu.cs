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
        AudioManager.instance.PlaySingle(UIManager.instance.back);
        UIManager.instance.Quit();
    }

    public void PlayGame()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.confirm);
        UIManager.instance.PlayGame();
    }

    public void Controls()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.confirm);
        UIManager.instance.ShowScreen("Controls Menu");
    }
}
