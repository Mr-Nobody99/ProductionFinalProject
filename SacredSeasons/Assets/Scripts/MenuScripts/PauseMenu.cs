using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   public void Back()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.confirm);
        UIManager.instance.paused = false;

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            UIManager.instance.TutorialUnPause();
        }
        else
        {
            UIManager.instance.Play();
        }
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
