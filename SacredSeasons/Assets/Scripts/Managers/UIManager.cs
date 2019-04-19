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

    public bool paused = false;

    public AudioClip confirm;
    public AudioClip back;

    public void ShowScreen(string name)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;

        if (name.Equals("Main Menu") && (paused))
        {
            name = "Pause Menu";
        }

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

            paused = true;

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
        } else
        {
            paused = false;
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
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

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
        PlayerController.paused = false;
        PlayerController.menuUp = false;
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        screens[curScreen].screen.SetActive(false);
        AudioManager.instance.PlaySingle(confirm);
        SceneManager.LoadScene("Scene1");
    }

    public void Quit()
    {
        AudioManager.instance.PlaySingle(confirm);
        Application.Quit();
        Debug.Log("Quitting Game");
    }

    public void Restart()
    {
        PlayerController.currentHealth = 100;
        AudioManager.instance.PlaySingle(confirm);
        //PlayerController.healthBar.value = 100;
        UIManager.instance.ShowScreen("Main Menu");
        SceneManager.LoadScene("Boss Fight");
        Debug.Log("Loading Main Menu Scene");
    }
}
