using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileMovement))]
public class IceProjectile : MonoBehaviour
{
    ProjectileMovement moveComponent;

    [Header("Frozen Platform Prefab")]
    [SerializeField]
    GameObject platformPrefab;
    [Header("Damage Value")]
    [SerializeField]
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        moveComponent = GetComponent<ProjectileMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "FireEnemy")
        {
            collision.gameObject.GetComponent<AI_Controller_Fire>().Freeze();
            collision.gameObject.GetComponent<AI_Controller_Fire>().TakeDamage(damage*2);
        }
        else if(collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
        else if(collision.gameObject.tag.Equals("Monster"))
        {
            collision.gameObject.GetComponent<MonsterBossController>().TakeDamage(damage/3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            moveComponent.speed = 0;

            Quaternion rotation = Quaternion.Euler(90, 0, 0);

            print("Hit Water");
            var platform = Instantiate(platformPrefab, transform.position, Quaternion.identity);
            platform.GetComponent<FloatingMovement>().water = other.gameObject;
            //platform.transform.parent = other.transform;
            Destroy(gameObject);

            var hitFX = Instantiate(moveComponent.Hit_FX, transform.position, Quaternion.identity);
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
        }
    }
}
