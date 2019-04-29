using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public List<Text> tutorialTexts = new List<Text>();
    public int curText = 0;
    public Button next;
    public GameObject panel;

    public void Next()
    {

        Debug.Log("Pressed Next: " + curText);

        AudioManager.instance.PlaySingle(UIManager.instance.confirm);

        if (curText < 8)
        {
            tutorialTexts[curText].gameObject.SetActive(false);
            curText += 1;
            tutorialTexts[curText].gameObject.SetActive(true);

            if (curText == 5 || curText == 6)
            {
                next.gameObject.SetActive(false);
                Time.timeScale = 1;
                GameManager.instance.canMove = true;
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Time.timeScale = 0;
                GameManager.instance.canMove = false;
                //Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;
            }
        }
        else
        {
            panel.SetActive(false);
            Time.timeScale = 1;
            GameManager.instance.canMove = true;
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        } 
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Jump") && next.gameObject.activeSelf == true)
        {
            Next();
        }

        if (GameManager.instance.moveComplete == true)
        {
            tutorialTexts[curText].gameObject.SetActive(false);
            curText = curText + 1;
            tutorialTexts[curText].gameObject.SetActive(true);
            next.gameObject.SetActive(true);
            //next.Select();

            Time.timeScale = 0;
            GameManager.instance.canMove = false;
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;

            GameManager.instance.moveComplete = false;
        }
        else if (GameManager.instance.shootComplete == true)
        {
            tutorialTexts[curText].gameObject.SetActive(false);
            curText = curText + 1;
            tutorialTexts[curText].gameObject.SetActive(true);
            GameManager.instance.shootComplete = false;
        }
        else if (GameManager.instance.shieldComplete == true)
        {
            tutorialTexts[curText].gameObject.SetActive(false);
            curText = curText + 1;
            tutorialTexts[curText].gameObject.SetActive(true);
            next.gameObject.SetActive(true);
            //next.Select();

            Time.timeScale = 0;
            GameManager.instance.canMove = false;
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;

            GameManager.instance.shieldComplete = false;
            //GameManager.instance.tutorialComplete = true;
        }
    }


}
