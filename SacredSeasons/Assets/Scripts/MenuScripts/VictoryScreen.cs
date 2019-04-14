using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public void MainMenu()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.confirm);
        UIManager.instance.Restart();
    }

    public void Quit()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.back);
        UIManager.instance.Quit();
    }
    
    public void Restart()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.confirm);
        UIManager.instance.Restart();
    }
}
