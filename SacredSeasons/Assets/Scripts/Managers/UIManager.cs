﻿using System.Collections;
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

    public string currentSeason;

    public void ShowScreen(string name)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        PlayerController.menuUp = true;

        // Checks which season is currently active and changes the pause and options menu shown
        if (name.Equals("Pause Menu"))
        {
            if (currentSeason.Contains("Autumn") || currentSeason.Contains("Fall"))
            {
                name = "Fall Pause Menu";
            } else if (currentSeason.Contains("Spring"))
            {
                name = "Spring Pause Menu";
            } else if (currentSeason.Contains("Summer"))
            {
                name = "Summer Pause Menu";
            } else if (currentSeason.Contains("Winter"))
            {
                name = "Winter Pause Menu";
            }
            else
            {
                // Default case
                name = "Fall Pause Menu";
            }
        } else if (name.Equals("Options Menu"))
        {
            if (currentSeason.Contains("Autumn") || currentSeason.Contains("Fall"))
            {
                name = "Fall Options Menu";
            }
            else if (currentSeason.Contains("Spring"))
            {
                name = "Spring Options Menu";
            }
            else if (currentSeason.Contains("Summer"))
            {
                name = "Summer Options Menu";
            }
            else if (currentSeason.Contains("Winter"))
            {
                name = "Winter Options Menu";
            }
            else
            {
                // Default case
                name = "Fall Options Menu";
            }
        }

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

    private void Start()
    {
        Scene curScene = SceneManager.GetActiveScene();
        currentSeason = curScene.name;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }

        if (PlayerController.menuUp == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
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
        screens[curScreen].screen.SetActive(false);
        PlayerController.menuUp = false;
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
        PlayerController.menuUp = true;
        SceneManager.LoadScene("Fire");
    }
}
