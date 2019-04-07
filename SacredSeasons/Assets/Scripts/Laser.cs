using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    Transform targetBone;
    Vector3 dir;

    [SerializeField]
    ParticleSystem Orb;
    [SerializeField]
    LineRenderer Beam;

    public bool Activated;

    // Start is called before the first frame update
    void Start()
    {
        targetBone = GameObject.Find("PlayerTarget").transform;
        Activated = false;
        Beam.useWorldSpace = true;
        Beam.enabled = false;
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
            Beam.enabled = true;
            RotateToPlayer();
            UpdateBeam();
        }
        else
        {
            Beam.enabled = false;
        }
    }

    public void PlayParticles(bool play)
    {
        if (play)
        {
            Orb.Play();
        }
        else
        {
            Orb.Stop();
            Orb.Clear();
        }
    }

    void RotateToPlayer()
    {
        dir = targetBone.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    void UpdateBeam()
    {
        Beam.SetPosition(0, transform.position);
        RaycastHit hit;
        Physics.SphereCast(transform.position, 1.0f, dir, out hit);
        Beam.SetPosition(1, hit.point);
    }
}
