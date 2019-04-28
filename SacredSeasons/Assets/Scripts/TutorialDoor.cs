using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TutorialDoor : MonoBehaviour
{
    [SerializeField]
    GameObject doorR;
    [SerializeField]
    GameObject doorL;

    bool locked = true;
    bool activated = false;
    bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      /*if(LevelManager.GetLevelComplete("fire") && LevelManager.GetLevelComplete("water") && LevelManager.GetLevelComplete("earth"))
        {
            locked = false;
        }

      if(!locked && activated && !open)
        {
            if (doorR.transform.localEulerAngles.y != 156 && doorL.transform.localEulerAngles.y != 204)
            {
                doorR.transform.Rotate(0, 1, 0);
                print("Door_R Y Rotation = " + doorR.transform.localEulerAngles.y);
                doorL.transform.Rotate(0, -1, 0);
                print("Door_L Y Rotation = " + doorL.transform.localEulerAngles.y);
            }
            else
            {
                open = true;
                print("finished");
            }
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            print("Should Open");
            activated = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Player") && open)
        {
            SceneManager.LoadScene("Boss Fight");
        }
    }
}
