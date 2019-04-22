using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCollectible : MonoBehaviour
{

    public int ListIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            InventoryManager.instance.PlayerSpells[ListIndex].available = true;
            InventoryManager.instance.PlayerShields[ListIndex].available = true;
            SceneManager.LoadScene("HUb Scene");
            Destroy(gameObject);
        }
    }

}
