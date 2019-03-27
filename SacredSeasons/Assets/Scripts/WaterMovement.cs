using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{

    Vector3 initalPos;

    [SerializeField]
    GameObject respawn;
    [SerializeField]
    float maxUp;
    [SerializeField]
    float smooth;

    bool MoveUp = true;

    // Start is called before the first frame update
    void Start()
    {
        initalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveUp)
        {
            if (transform.position.y < initalPos.y + maxUp)
            {
                Vector3 currentPos = transform.position;
                Vector3 newPos = new Vector3(currentPos.x, currentPos.y + maxUp / smooth, currentPos.z);

                transform.position = newPos;
            }
            else
            {
                MoveUp = false;
            }
        }
        else if(!MoveUp)
        {
            if (transform.position.y > initalPos.y)
            {
                Vector3 currentPos = transform.position;
                Vector3 newPos = new Vector3(currentPos.x, currentPos.y - maxUp / smooth, currentPos.z);

                transform.position = newPos;
            }
            else
            {
                MoveUp = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            other.transform.position = respawn.transform.position;
        }
    }

}
