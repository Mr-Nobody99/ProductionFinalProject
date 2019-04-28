using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource fxSource;
    public AudioSource musicSource;

    public AudioClip mainMenuMusic;
    public AudioClip hubMusic;
    public AudioClip winterMusic;
    public AudioClip springMusic;
    public AudioClip summerMusic;
    public AudioClip bossMusic;

    string sceneName = "";

    //public AudioSource uiSource;
    public static AudioManager instance = null;

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

        fxSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "Main Menu":
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
