using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioMixer mixer;

    /*float _audioVolume;
    public float audioVolume
    {
        get
        {
            if (_audioVolume < 0)
                return 0;
            if (_audioVolume > 1)
                return 1;

            return _audioVolume;
        }
        set
        {
            _audioVolume = value;
            mixer.SetFloat("MasterVolume", _audioVolume);
        }
    }*/

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
}
