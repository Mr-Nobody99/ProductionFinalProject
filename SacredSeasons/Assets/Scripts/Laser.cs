using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    Transform targetBone;
    Vector3 dir;

    public int ElementIndex = 0;
    [SerializeField]
    List<ParticleSystem> Orbs;
    [SerializeField]
    List<LineRenderer> Beams;

    public bool Activated;

    // Start is called before the first frame update
    void Start()
    {
        Activated = false;
        targetBone = GameObject.Find("PlayerTarget").transform;

        foreach(LineRenderer line in Beams)
        {
            line.useWorldSpace = true;
            line.enabled = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay( transform.position, dir);
    }

    // Update is called once per frame
    void Update()
    {
        if (Activated)
        {
            Beams[ElementIndex].enabled = true;
            RotateToPlayer();
            UpdateBeam();
        }
        else
        {
            Beams[ElementIndex].enabled = false;
        }
    }

    public void PlayParticles(bool play)
    {
        if (play)
        {
            Orbs[ElementIndex].Play();
        }
        else
        {
            Orbs[ElementIndex].Stop();
            Orbs[ElementIndex].Clear();
        }
    }

    void RotateToPlayer()
    {
        dir = targetBone.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    void UpdateBeam()
    {
        Beams[ElementIndex].SetPosition(0, transform.position);
        RaycastHit hit;
        Physics.SphereCast(transform.position, 1.0f, dir, out hit);
        
        switch (ElementIndex)
        {
            case 0:
                if(hit.collider.tag.Equals("Earth"))
                {
                    Beams[ElementIndex].SetPosition(1, hit.point);
                }
                else
                {
                    Beams[ElementIndex].SetPosition(1, targetBone.position);
                }
                break;

            case 1:
                if (hit.collider.tag.Equals("Ice"))
                {
                    Beams[ElementIndex].SetPosition(1, hit.point);
                }
                else
                {
                    Beams[ElementIndex].SetPosition(1, targetBone.position);
                }
                break;

            case 2:
                if (hit.collider.tag.Equals("Fire"))
                {
                    Beams[ElementIndex].SetPosition(1, hit.point);
                }
                else
                {
                    Beams[ElementIndex].SetPosition(1, targetBone.position);
                }
                break;
        }
        //Beams[ElementIndex].SetPosition(1, hit.point);
    }
}
