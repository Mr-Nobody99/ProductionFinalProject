using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileMovement))]
public class EarthProjectile : MonoBehaviour
{
    ProjectileMovement moveComponent;

    [SerializeField]
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        moveComponent = this.GetComponent<ProjectileMovement>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("WaterEnemy"))
        {
            collision.gameObject.GetComponent<AI_Controller_Water>().TakeDamage(damage*2);
        }
        else if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
        else if (collision.gameObject.tag.Equals("Monster"))
        {
            collision.gameObject.GetComponent<MonsterBossController>().TakeDamage(damage/3);
        }
    }
}
