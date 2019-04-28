using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private bool hubZone;
    [SerializeField]
    private bool fireZone;
    [SerializeField]
    private bool waterZone;
    [SerializeField]
    private bool earthZone;

    string sceneToLoad;

    private void Start()
    {
        if(hubZone)
        {
            sceneToLoad = "HUB";
        }
        else if(fireZone)
        {
            sceneToLoad = "Fire";
        }
        else if (waterZone)
        {
            sceneToLoad = "Water";
        }
        else if (earthZone)
        {
            sceneToLoad = "Earth";
        }
    }

    private void Update()
    {
        if(waterZone && LevelManager.GetLevelComplete("water"))
        {
            Destroy(gameObject);
        }
        else if( fireZone && LevelManager.GetLevelComplete("fire"))
        {
            Destroy(gameObject);
        }
        else if(earthZone && LevelManager.GetLevelComplete("earth"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
