using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    public AudioMixer mixer;

    public AudioSource fxSource;
    public AudioSource musicSource;

    public AudioClip mainMenuMusic;
    public AudioClip hubMusic;
    public AudioClip winterMusic;
    public AudioClip springMusic;
    public AudioClip summerMusic;
    public AudioClip bossMusic;

    public AudioClip gameOverMusic;
    public AudioClip victoryMusic;

    string sceneName = "";

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
    }



    public void PlaySingle(AudioClip clip)
    {
        fxSource.clip = clip;

        fxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        sceneName = SceneManager.GetActiveScene().name;

        switch (scene.name)
        {
            case "Main Menu Scene":
                PlayMusic(mainMenuMusic);
                break;
            case "HUB":
                PlayMusic(hubMusic);
                break;
            case "Water":
                PlayMusic(winterMusic);
                break;
            case "Earth":
                PlayMusic(springMusic);
                break;
            case "Fire":
                PlayMusic(summerMusic);
                break;
            case "Boss Fight":
                PlayMusic(bossMusic);
                break;
            case "Tutorial":
                PlayMusic(hubMusic);
                break;
            default:
                PlayMusic(hubMusic);
                break;
        }
    }
}
