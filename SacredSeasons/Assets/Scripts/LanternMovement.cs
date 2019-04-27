using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternMovement : MonoBehaviour
{

    Vector3 initialPos;

    bool active = false;
    bool moveUp = true;

    float delay;
    float maxUp;
    float smooth;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        smooth = 500.0f;
        maxUp = Random.Range(5, 50);
        delay = Random.Range(0.1f, 1.0f);
        StartCoroutine(DelayStart(delay));
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (moveUp)
            {
                if (transform.position.y < initialPos.y + maxUp)
                {
                    Vector3 currentPos = transform.position;
                    Vector3 newPos = new Vector3(currentPos.x, currentPos.y + maxUp / smooth, currentPos.z);

                    transform.position = newPos;
                }
                else
                {
                    moveUp = false;
                }
            }
            else if (!moveUp)
            {
                if (transform.position.y > initialPos.y)
                {
                    Vector3 currentPos = transform.position;
                    Vector3 newPos = new Vector3(currentPos.x, currentPos.y - maxUp / smooth, currentPos.z);

                    transform.position = newPos;
                }
                else
                {
                    moveUp = true;
                }
            }
        }
    }

    IEnumerator DelayStart(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        active = true;
        print("Activate");
    }
}
