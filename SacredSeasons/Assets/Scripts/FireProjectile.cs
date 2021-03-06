﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileMovement))]
public class FireProjectile : MonoBehaviour
{
    ProjectileMovement moveComponent;

    [SerializeField]
    float dmg = 10;

    // Start is called before the first frame update
    void Start()
    {
        moveComponent = GetComponent<ProjectileMovement>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Plant"))
        {
            //print("Hit plant");
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
        else if(other.tag.Equals("EarthEnemy"))
        {
            other.gameObject.GetComponent<AI_Controller_Earth>().TakeDamage(dmg);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if (!PlayerController.spellsShot.Contains(gameObject))
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(dmg);
            }
        }
        else if (collision.gameObject.tag.Equals("Monster"))
        {
            collision.gameObject.GetComponent<MonsterBossController>().TakeDamage(dmg);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
