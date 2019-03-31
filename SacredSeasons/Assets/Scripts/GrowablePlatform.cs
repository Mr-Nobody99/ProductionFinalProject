using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowablePlatform : MonoBehaviour
{
    [SerializeField]
    GameObject platform;
    [SerializeField]
    ParticleSystem ps;
    [SerializeField]
    GameObject platformText;

    [SerializeField]
    float growSpeed = 1f;
    [SerializeField]
    float growHeight = 1f;

    [HideInInspector]
    public bool doGrow = false;

    Vector3 initialScale;
    Vector3 targetScale;

    // Start is called before the first frame update
    void Start()
    {
        platformText.SetActive(false);
        initialScale = new Vector3(1f, 0f, 1f);
        targetScale = new Vector3(1f, growHeight, 1f);
        platform.SetActive(false);
        platform.transform.localScale = new Vector3(1f, 0f, 1f);
    }

    private void Update()
    {
        if (doGrow)
        {
            ps.Stop();
            platform.transform.localScale = Vector3.Lerp(platform.transform.localScale, targetScale, Time.deltaTime * growSpeed);
            if (platform.transform.localScale.y >= targetScale.y - 0.15f)
            {
                doGrow = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            platformText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Player") && Input.GetButtonDown("Interact"))
        {
            doGrow = true;
            platform.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            platformText.SetActive(false);
        }
    }
}
