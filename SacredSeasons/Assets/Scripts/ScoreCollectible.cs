using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCollectible : MonoBehaviour
{
    [SerializeField]
    int value = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerScore.AddToCurrentScore(value);
            Destroy(gameObject);
        }
    }
}
