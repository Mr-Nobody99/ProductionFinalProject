using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellPanelScript : MonoBehaviour
{
    public Text currentSpellText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (PlayerController.currentSpellName)
        {
            case "IceProjectile":
                currentSpellText.text = "Ice";
                break;

            case "FireProjectile":
                currentSpellText.text = "Fire";
                break;
                
            case "EarthProjectile":
                currentSpellText.text = "Earth";
                break;

            default:
                currentSpellText.text = "ERROR";
                break;
        }
        
    }
}
