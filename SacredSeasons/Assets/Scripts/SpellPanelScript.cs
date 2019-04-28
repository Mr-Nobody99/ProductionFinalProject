using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellPanelScript : MonoBehaviour
{
    //public Text currentSpellText;
    public RawImage currentSpellIcon;

    [System.Serializable]
    public struct SpellIcons
    {
        //public RawImage icon;
        public string element;
        public Texture spellTexture;
    }

    public List<SpellIcons> spells = new List<SpellIcons>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (PlayerController.currentSpellName)
        {
            case "Ice Spell":
                //currentSpellText.text = "Ice";
                currentSpellIcon.texture = spells[0].spellTexture;
                break;

            case "Fire Spell":
                //currentSpellText.text = "Fire";
                currentSpellIcon.texture = spells[1].spellTexture;
                break;
                
            case "Earth Spell":
                //currentSpellText.text = "Earth";
                currentSpellIcon.texture = spells[2].spellTexture;
                break;

            default:
                //currentSpellText.text = "ERROR";
                currentSpellIcon.texture = spells[3].spellTexture;
                break;
        }
        
    }
}
