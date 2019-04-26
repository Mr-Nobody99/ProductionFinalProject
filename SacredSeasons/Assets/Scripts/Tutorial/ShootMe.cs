using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMe : MonoBehaviour
{
    static bool beenShot = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (beenShot && Input.GetButton("Block"))
        {
            UIManager.instance.tutorialScreen.SetActive(false);
            GameManager.instance.shootTutorialDone = true;
            GameManager.instance.tutorialOver = true;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            Debug.Log("Projectile");
        }

        if (other.tag == "Projectile" && !beenShot)
        {
            Debug.Log("Went through");
            beenShot = true;
            UIManager.instance.shootText.SetActive(false);
            UIManager.instance.finalText.SetActive(true);
            GameManager.instance.shootTutorialDone = true;

            ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
            ps.Stop();
            //gameObject.SetActive(false);
        }
    }

}
