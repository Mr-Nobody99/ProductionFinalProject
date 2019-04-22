using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;

    [System.Serializable]
    public struct Spell
    {
        public string name;
        public GameObject projectile;
        public bool available;
    }

    [System.Serializable]
    public struct Shield
    {
        public string name;
        public GameObject shield;
        public bool available;
    }

    public int currentSpellIndex = 0;

    public List<Spell> PlayerSpells = new List<Spell>();
    public List<Shield> PlayerShields = new List<Shield>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
}
