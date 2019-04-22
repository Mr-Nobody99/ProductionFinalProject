using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformingRespawn : MonoBehaviour
{

    public GameObject respawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Original Location: " + other.transform.localPosition.ToString());
            Vector3 respawnT = respawn.transform.position;
            other.transform.localPosition = respawnT;
            Debug.Log("New Location: " + other.transform.localPosition.ToString());
            //other.transform.position = respawn.transform.position;

        }
    }

}
