using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [System.Serializable]
    public struct Screen
    {
        public string name;
        public GameObject screen;
        public Button firstButton;
        public Animator screenAnimator;
    }

    public List<Screen> screens = new List<Screen>();
    public int curScreen;

    public void ShowScreen(string name)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;

        for (int i = 0; i < screens.Count; i++)
        {
            if (screens[i].name.Equals(name))
            {
                StartCoroutine(ShowScreenCoroutine(i));
                screens[curScreen].screen.SetActive(false);
                screens[i].screen.SetActive(true);
                screens[i].firstButton.Select();
                curScreen = i;
            }
        }
    }

    public IEnumerator ShowScreenCoroutine(int index)
    {
        if (screens[curScreen].screenAnimator != null)
        {
            screens[curScreen].screenAnimator.SetTrigger("Close");
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);

        

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }

        if (PlayerController.menuUp == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    public void Play()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        screens[curScreen].screen.SetActive(false);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Removing menu from screen");
        screens[curScreen].screen.SetActive(false);
        Debug.Log("Loading Scene1");
        SceneManager.LoadScene("Scene1");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }

    public void Restart()
    {
        PlayerController.currentHealth = 100;
        //PlayerController.healthBar.value = 100;
        SceneManager.LoadScene("Fire");
        Debug.Log("Loading Main Menu Scene");
    }
}
