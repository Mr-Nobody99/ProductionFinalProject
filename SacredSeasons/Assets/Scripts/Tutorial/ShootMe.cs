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
        if (beenShot)
        {
            // LOOK FOR SHIELD THEN PUT AWAY TUTORIAL SCREEN
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Ice") && !beenShot)
        {
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
