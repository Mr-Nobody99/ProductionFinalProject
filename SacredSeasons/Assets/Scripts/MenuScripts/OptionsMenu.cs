using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider Sound;

    public void Back()
    {
        UIManager.instance.ShowScreen("Main Menu");
    }

    public void Graphics()
    {
        UIManager.instance.ShowScreen("Graphics Menu");
    }

    public void SetAudioVolume(float x)
    {
        Debug.Log("Setting Audio Volume to: " + x);
        GameManager.instance.audioVolume = x;
    }

    void Start()
    {
        Sound.value = GameManager.instance.audioVolume;
    }
}
