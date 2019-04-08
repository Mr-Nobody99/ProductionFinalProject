using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterBossController : MonoBehaviour
{

    NavMeshAgent NavAgent;
    Animator AnimController;
    GameObject PlayerRef;

    [SerializeField]
    List<AI_Waypoint> Waypoints;
    int WaypointIndex = 0;

    [SerializeField]
    GameObject Laser;
    Laser laserScript;

    bool corutineActive = false;
    bool blockTrigger = false;
    bool persuePlayer = false;
    bool assignWaypoint = false;
    bool moveToWaypoint = false;
    bool doLaserAttack = false;
    bool beamActivated = false;
    bool usingNavLink = false;

    // Start is called before the first frame update
    void Start()
    {
        NavAgent = this.GetComponent<NavMeshAgent>();
        AnimController = this.GetComponent<Animator>();
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        laserScript = Laser.GetComponent<Laser>();
        assignWaypoint = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckOffMeshLink();
        if(assignWaypoint)
        {
            AssignWaypoint();
        }
        else if(moveToWaypoint)
        {
            MoveToWaypoint();
        }
        else if(persuePlayer)
        {
            PersuePlayer();
        }
        else if(doLaserAttack)
        {
            LaserAttack();
        }
    }

    void CheckOffMeshLink()
    {
        if (NavAgent.isOnOffMeshLink && !usingNavLink)
        {
            print("Jump");
            usingNavLink = true;
            AnimController.SetTrigger("Jump");
        }
        if (!NavAgent.isOnOffMeshLink && usingNavLink)
        {
            usingNavLink = false;
            AnimController.SetTrigger("Fall");
        }
    }

    void AssignWaypoint()
    {
        WaypointIndex = Random.Range(0, Waypoints.Count);
        assignWaypoint = false;
        moveToWaypoint = true;
    }

    void MoveToWaypoint()
    {
        NavAgent.SetDestination(Waypoints[WaypointIndex].transform.position);
        if(Vector3.Distance(transform.position, Waypoints[WaypointIndex].transform.position) <= 1.0f)
        {
            print("At Waypoint");
            moveToWaypoint = false;
            doLaserAttack = true;
        }
    }

    void LaserAttack()
    {
        Vector3 dir = PlayerRef.transform.position -transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
        if (!blockTrigger)
        {
            AnimController.SetTrigger("Laser");
            blockTrigger = true;
        }
    }

    void TryPersuePlayer()
    {
        int result = Random.Range(0, 2);
        if(result == 0)
        {
            persuePlayer = true;
        }
        else
        {
            persuePlayer = false;
            assignWaypoint = true;
        }
    }

    void PersuePlayer()
    {
        NavAgent.SetDestination(PlayerRef.transform.position);
        if(Vector3.Distance(transform.position, PlayerRef.transform.position) <= 10)
        {
            AnimController.SetTrigger("Melee");
        }
        if(!corutineActive)
        {
            StartCoroutine(StopPersuePlayer());
        }
    }

    IEnumerator StopPersuePlayer()
    {
        corutineActive = true;
        yield return new WaitForSecondsRealtime(12);
        persuePlayer = false;
        assignWaypoint = true;
        corutineActive = false;
    }

    public void BeginAttack()
    {
        print("Attack Triggered!");
        laserScript.ElementIndex = Random.Range(0, 3);
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
        doLaserAttack = false;
        blockTrigger = false;
        TryPersuePlayer();
    }
}
