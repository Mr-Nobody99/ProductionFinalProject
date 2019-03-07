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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(hubZone)
            {
                SceneManager.LoadScene(0);
            }
            if(fireZone)
            {
                SceneManager.LoadScene(1);
            }
            else if(waterZone)
            {
                SceneManager.LoadScene(2);
            }
            else if(earthZone)
            {
                SceneManager.LoadScene(3);
            }
        }
    }
}
