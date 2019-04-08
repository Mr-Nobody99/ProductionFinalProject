using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
   public void Back()
    {
        UIManager.instance.Play();
    }

    public void Options()
    {
        UIManager.instance.ShowScreen("Options Menu");
    }

    public void Quit()
    {
        UIManager.instance.Quit();
    }
}
