using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsMenu : MonoBehaviour
{
    public Dropdown AA;
    public Toggle shadows;

    public void Back()
    {
        UIManager.instance.ShowScreen("OptionsMenu");
    }

    void Start()
    {
        AA.onValueChanged.AddListener(delegate
        {
            AAChanged(AA);
        });
    }
    
    public void AAChanged(Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                QualitySettings.antiAliasing = 2;
                Debug.Log("Anti Aliasing: " + 2 + "x");
                break;
            case 1:
                QualitySettings.antiAliasing = 4;
                Debug.Log("Anti Aliasing: " + 4 + "x");
                break;
            case 2:
                QualitySettings.antiAliasing = 8;
                Debug.Log("Anti Aliasing: " + 8 + "x");
                break;
            default:
                QualitySettings.antiAliasing = 2;
                Debug.Log("Anti Aliasing: " + 2 + "x");
                break;
        }
    }
}
