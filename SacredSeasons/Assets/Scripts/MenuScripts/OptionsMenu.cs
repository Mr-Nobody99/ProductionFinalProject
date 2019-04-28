using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider BGM;
    public Slider SFX;

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

    public void SetBGMVolume()
    {
        //Debug.Log("Setting BGM Volume to: " + Sound.value);
        AudioManager.instance.mixer.SetFloat("bgmVolume", BGM.value);
        //AudioManager.instance.PlaySingle(UIManager.instance.confirm);
    }

    public void SetSFXVolume()
    {
        AudioManager.instance.mixer.SetFloat("sfxVolume", SFX.value);
    }

    void Start()
    {
        //Sound.value = 1;
    }
}
