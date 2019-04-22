using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetingSystem : MonoBehaviour
{
    public float speed = 3.0f;

    public GameObject m_target = null;
    Vector3 m_lastKnownPosition = Vector3.zero;
    Quaternion m_lookAtRotation;
    Quaternion m_targetLocation;

    bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (m_target)
        {
            if (m_lastKnownPosition != m_target.transform.position)
            {
                m_lastKnownPosition = m_target.transform.position;
                m_targetLocation = Quaternion.LookRotation(m_target.transform.position);
                m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - transform.position);
                Shoot(1);
                //DelayNextShot(1);
            }

            if (transform.rotation != m_lookAtRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookAtRotation, speed * Time.deltaTime);
            }
        }
    }

    bool SetTarget(GameObject target)
    {
        if (!target)
        {
            return false;
        }

        m_target = target;

        return true;
    }

    void Shoot(int index)
    {
        if (canShoot)
        {
            Instantiate(InventoryManager.instance.PlayerSpells[index].projectile, gameObject.transform.position, m_lookAtRotation);
            StartCoroutine(DelayNextShot(1));
        }
    }

    public IEnumerator DelayNextShot(int index)
    {
        //Instantiate(InventoryManager.instance.PlayerSpells[index].projectile, gameObject.transform.position + gameObject.transform.up * 2.0f, m_lookAtRotation);
        canShoot = false;
        yield return new WaitForSeconds(2f);
        canShoot = true;

    }
}
