using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterBossController : MonoBehaviour
{

    NavMeshAgent NavAgent;
    Animator AnimController;

    [SerializeField]
    GameObject Laser;
    Laser laserScript;
    bool beamActivated = false;

    GameObject PlayerRef;

    // Start is called before the first frame update
    void Start()
    {
        NavAgent = this.GetComponent<NavMeshAgent>();
        AnimController = this.GetComponent<Animator>();
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        laserScript = Laser.GetComponent<Laser>();
    }

    // Update is called once per frame
    void Update()
    {
        //NavAgent.SetDestination(PlayerRef.transform.position);
    }

    public void BeginAttack()
    {
        print("Attack Triggered!");
        laserScript.PlayParticles(true);
    }

    public void ActivateBeam()
    {
        laserScript.Activated = true;
    }

    public void EndAttack()
    {
        print("Attack End!");
        laserScript.Activated = false;
        laserScript.PlayParticles(false);
    }
}
