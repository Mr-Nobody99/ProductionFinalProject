using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowablePlatform : MonoBehaviour
{
    [SerializeField]
    GameObject platform;
    [SerializeField]
    ParticleSystem ps;
    //[SerializeField]
    //GameObject platformText;

    [SerializeField]
    float growSpeed = 1f;
    [SerializeField]
    float growHeight = 1f;

    [HideInInspector]
    public bool doGrow = false;
    [HideInInspector]
    public bool doShrink = false;

    Vector3 initialScale;
    Vector3 targetScale;

    bool grown = false;

    // Start is called before the first frame update
    void Start()
    {
        ps.Play();
        initialScale = platform.transform.localScale;
        //targetScale = new Vector3(initialScale.x, initialScale.y * growHeight, initialScale.z);
        targetScale = new Vector3(initialScale.x * growHeight/2, initialScale.y * growHeight, initialScale.z * growHeight/2);

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
                grown = true;
                ps.Play();
            }
        }
        else if (doShrink)
        {
            ps.Stop();
            platform.transform.localScale = Vector3.Lerp(platform.transform.localScale, initialScale, Time.deltaTime * growSpeed);
            if (platform.transform.localScale.y <= initialScale.y + 0.5f)
            {
                doShrink = false;
                grown = false;
                ps.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Earth") && !grown)
        {
            doGrow = true;
        }
        else if (other.name.Contains("Earth") && grown)
        {
            doShrink = true;
        }
    }

    /*
    // Take out, has to work on spellcast
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            platformText.SetActive(true);
        }
    }

    // Take out, has to grow/shrink on toggle based on spell cast
    private void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Player") && Input.GetButtonDown("Interact"))
        {
            doGrow = true;
            platform.SetActive(true);
        }
    }

    // Take out, see above reasons ^
    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            platformText.SetActive(false);
        }
    }*/
}
