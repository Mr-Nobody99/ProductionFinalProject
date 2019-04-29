using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTarget : MonoBehaviour
{

    public bool active;
    [SerializeField]
    string element;

    [SerializeField]
    ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals(element))
        {
            active = false;
            particles.Stop();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
