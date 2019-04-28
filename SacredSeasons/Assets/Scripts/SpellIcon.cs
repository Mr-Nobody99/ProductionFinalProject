using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpellIcon : MonoBehaviour
{

    RawImage img;

    [SerializeField]
    List<Texture> SpellIcons = new List<Texture>();

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<RawImage>(); 
    }

    // Update is called once per frame
    void Update()
    {
        switch(PlayerController.currentSpellName)
        {
            case "Ice Spell":
                img.texture = SpellIcons[0];
                break;
            case "Earth Spell":
                img.texture = SpellIcons[1];
                break;
            case "Fire Spell":
                img.texture = SpellIcons[2];
                break;
        }
    }
}
