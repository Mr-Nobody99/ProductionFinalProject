using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
   public void Back()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.confirm);
        UIManager.instance.paused = false;
        UIManager.instance.Play();
    }

    public void Options()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.confirm);
        UIManager.instance.ShowScreen("Options Menu");
    }

    public void Controls()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.confirm);
        UIManager.instance.ShowScreen("Controls Menu");
    }

    public void Quit()
    {
        UIManager.instance.Quit();
    }
}
