using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider Sound;

    public void Back()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.back);
        UIManager.instance.ShowScreen(UIManager.instance.previousScreenName);
    }

    public void Graphics()
    {
        AudioManager.instance.PlaySingle(UIManager.instance.confirm);
        UIManager.instance.ShowScreen("Graphics Menu");
    }

    public void SetAudioVolume()
    {
        Debug.Log("Setting Audio Volume to: " + Sound.value);
        AudioManager.instance.PlaySingle(UIManager.instance.confirm);
    }

    void Start()
    {
        //Sound.value = 1;
    }
}
