using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class McGuffin : MonoBehaviour
{
    Vector3 initialPos;

    new string name;

    float maxUp;
    float smooth;

    bool moveUp;

    [Header("Level")]
    [SerializeField]
    bool fire;
    [SerializeField]
    bool water;
    [SerializeField]
    bool earth;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        maxUp = 1;
        smooth = 100;

        if(fire)
        {
            name = "fire";
        }
        else if(water)
        {
            name = "water";
        }
        else
        {
            name = "earth";
        }
    }

    private void Update()
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            LevelManager.SetLevelComplete(name, true);
            SceneManager.LoadScene("HUB");
            Destroy(gameObject);
        }
    }
}
