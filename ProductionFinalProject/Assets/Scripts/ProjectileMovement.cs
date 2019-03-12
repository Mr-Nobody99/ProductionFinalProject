using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{

    [SerializeField]
    float speed;
    [SerializeField]
    float maxLifetime;

    [SerializeField]
    GameObject Hit_FX;
    [SerializeField]
    GameObject platformPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        if(speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            print("no speed value set!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;
        var hitFX = Instantiate(Hit_FX, transform.position, Quaternion.identity);
        var hit_PS = hitFX.GetComponent<ParticleSystem>();
        if(hit_PS != null)
        {
            Destroy(hitFX, hit_PS.main.duration);
        }
        else
        {
            var hitFXChild = hitFX.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(hitFX, hitFXChild.main.duration);
        }

        if(collision.gameObject.tag == "FireEnemy")
        {
            collision.gameObject.GetComponent<AI_Controller_Fire>().Freeze();
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Water")
        {
            speed = 0;
            var hitFX = Instantiate(Hit_FX, transform.position, Quaternion.identity);
            var hit_PS = hitFX.GetComponent<ParticleSystem>();
            if (hit_PS != null)
            {
                Destroy(hitFX, hit_PS.main.duration);
            }
            else
            {
                var hitFXChild = hitFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitFX, hitFXChild.main.duration);
            }

            print("Hit Water");
            var platform = Instantiate(platformPrefab, transform.position, Quaternion.identity);
            platform.transform.parent = other.transform;
            Destroy(gameObject);
        }
    }
}
