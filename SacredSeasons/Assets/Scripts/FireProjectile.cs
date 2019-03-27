using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileMovement))]
public class FireProjectile : MonoBehaviour
{
    ProjectileMovement moveComponent;

    // Start is called before the first frame update
    void Start()
    {
        moveComponent = GetComponent<ProjectileMovement>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Plant"))
        {
            print("Hit plant");
            moveComponent.speed = 0f;
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
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
