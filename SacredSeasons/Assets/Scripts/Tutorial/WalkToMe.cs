using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToMe : MonoBehaviour
{

    public GameObject shootMe;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.instance.moveText.SetActive(false);
            UIManager.instance.shootText.SetActive(true);
            GameManager.instance.moveTutorialDone = true;
            shootMe.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
