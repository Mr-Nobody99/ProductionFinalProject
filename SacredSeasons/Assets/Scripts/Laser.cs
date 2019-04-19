using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    GameObject PlayerRef;
    MonsterBossController MonsterController;

    [SerializeField]
    Transform parentBone;
    Transform targetBone;
    Vector3 dir;

    public int ElementIndex = 0;
    [SerializeField]
    List<ParticleSystem> Orbs;
    [SerializeField]
    List<LineRenderer> Beams;
    [SerializeField]
    List<ParticleSystem> HitFX;

    bool reflect;
    public float reflectSpeed = 1.0f;
    public float reflectScaleSpeed = 1.0f;
    public float reflectDistanceLimit = 5.0f;
    public bool Activated;

    // Start is called before the first frame update
    void Start()
    {
        Activated = false;
        reflect = false;

        MonsterController = GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterBossController>();

        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        targetBone = GameObject.Find("PlayerTarget").transform;

        foreach(LineRenderer line in Beams)
        {
            line.useWorldSpace = true;
            line.enabled = false;
        }

        HitFX[0] = Instantiate(HitFX[0]);
        HitFX[1] = Instantiate(HitFX[1]);
        HitFX[2] = Instantiate(HitFX[2]);

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
            reflect = false;
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
            HitFX[ElementIndex].Stop();
        }
    }

    void RotateToPlayer()
    {
        dir = targetBone.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    void UpdateBeam()
    {
        Beams[ElementIndex].SetPosition(0, parentBone.position);
        RaycastHit hit;
        Physics.SphereCast(transform.position, 1.0f, dir, out hit);
        
        switch (ElementIndex)
        {
            case 0:
                if(hit.collider.tag.Equals("Earth"))
                {
                    Beams[ElementIndex].SetPosition(1, hit.point);
                    if (!reflect)
                    {
                        reflect = true;
                        HitFX[ElementIndex].transform.localPosition = hit.point;
                        HitFX[ElementIndex].Play();
                    }
                    else if(reflect)
                    {
                        HitFX[ElementIndex].transform.position = Vector3.Lerp(HitFX[ElementIndex].transform.position, Beams[ElementIndex].GetPosition(0), Time.deltaTime * reflectSpeed);
                        HitFX[ElementIndex].transform.localScale = Vector3.Lerp(HitFX[ElementIndex].transform.localScale, HitFX[ElementIndex].transform.localScale * 2, Time.deltaTime * reflectScaleSpeed);
                        if (Vector3.Distance(HitFX[ElementIndex].transform.position, Beams[ElementIndex].GetPosition(0)) <= reflectDistanceLimit && !MonsterController.isStunned)
                        {
                            MonsterController.isStunned = true;
                        }
                    }
                }
                else
                {
                    Beams[ElementIndex].SetPosition(1, targetBone.position);
                    HitFX[ElementIndex].transform.position = Beams[ElementIndex].GetPosition(1);
                    HitFX[ElementIndex].transform.localScale = Vector3.one;
                    HitFX[ElementIndex].Play();
                    PlayerRef.GetComponent<PlayerController>().TakeDamage(0.1f);
                }
                break;

            case 1:
                if (hit.collider.tag.Equals("Ice"))
                {
                    Beams[ElementIndex].SetPosition(1, hit.point);
                    if (!reflect)
                    {
                        reflect = true;
                        HitFX[ElementIndex].transform.localPosition = hit.point;
                        HitFX[ElementIndex].Play();
                    }
                    else if (reflect)
                    {
                        HitFX[ElementIndex].transform.position = Vector3.Lerp(HitFX[ElementIndex].transform.position, Beams[ElementIndex].GetPosition(0), Time.deltaTime * reflectSpeed);
                        HitFX[ElementIndex].transform.localScale = Vector3.Lerp(HitFX[ElementIndex].transform.localScale, HitFX[ElementIndex].transform.localScale * 2, Time.deltaTime * reflectScaleSpeed);
                        if (Vector3.Distance(HitFX[ElementIndex].transform.position, Beams[ElementIndex].GetPosition(0)) <= reflectDistanceLimit && !MonsterController.isStunned)
                        {
                            MonsterController.isStunned = true;
                        }
                    }
                }
                else
                {
                    Beams[ElementIndex].SetPosition(1, targetBone.position);
                    HitFX[ElementIndex].transform.position = Beams[ElementIndex].GetPosition(1);
                    HitFX[ElementIndex].transform.localScale = Vector3.one;
                    HitFX[ElementIndex].Play();
                    PlayerRef.GetComponent<PlayerController>().TakeDamage(0.1f);
                }
                break;

            case 2:
                if (hit.collider.tag.Equals("Fire"))
                {
                    Beams[ElementIndex].SetPosition(1, hit.point);
                    if (!reflect)
                    {
                        reflect = true;
                        HitFX[ElementIndex].transform.localPosition = hit.point;
                        HitFX[ElementIndex].Play();
                    }
                    else if (reflect)
                    {
                        HitFX[ElementIndex].transform.position = Vector3.Lerp(HitFX[ElementIndex].transform.position, Beams[ElementIndex].GetPosition(0), Time.deltaTime * reflectSpeed);
                        HitFX[ElementIndex].transform.localScale = Vector3.Lerp(HitFX[ElementIndex].transform.localScale, HitFX[ElementIndex].transform.localScale * 2, Time.deltaTime * reflectScaleSpeed);
                        if (Vector3.Distance(HitFX[ElementIndex].transform.position, Beams[ElementIndex].GetPosition(0)) <= reflectDistanceLimit && !MonsterController.isStunned)
                        {
                            MonsterController.isStunned = true;
                        }
                    }
                }
                else
                {
                    Beams[ElementIndex].SetPosition(1, targetBone.position);
                    HitFX[ElementIndex].transform.position = Beams[ElementIndex].GetPosition(1);
                    HitFX[ElementIndex].transform.localScale = Vector3.one;
                    HitFX[ElementIndex].Play();
                    PlayerRef.GetComponent<PlayerController>().TakeDamage(0.1f);
                }
                break;
        }
    }
}
