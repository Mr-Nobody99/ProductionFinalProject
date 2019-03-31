using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    GameObject playerRef;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = playerRef.transform.position - transform.position;
        dir.y = 1f;
        transform.localRotation = Quaternion.LookRotation(dir, Vector3.up);
    }
}
