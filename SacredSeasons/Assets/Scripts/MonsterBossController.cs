using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterBossController : MonoBehaviour
{

    NavMeshAgent NavAgent;
    Animator AnimController;
    GameObject PlayerRef;

    [SerializeField]
    GameObject uiComp;
    [SerializeField]
    Image HealthBar;
    float MaxHealth = 100;
    float CurrentHealth;

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

    public bool isStunned = false;
    [SerializeField]
    int StunDuration;

    public bool IsDead = false;

    // Start is called before the first frame update
    void Start()
    {
        uiComp.SetActive(false);
        CurrentHealth = MaxHealth;
        NavAgent = this.GetComponent<NavMeshAgent>();
        AnimController = this.GetComponent<Animator>();
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        laserScript = Laser.GetComponent<Laser>();
        assignWaypoint = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag.Equals("Projectile"))
        {
            CurrentHealth -= 1f;
            HealthBar.fillAmount = CurrentHealth / MaxHealth;
            if(CurrentHealth <= 0)
            {
                UIManager.instance.ShowScreen("Victory Screen");
                //Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            UIManager.instance.ShowScreen("Victory Screen");
        }

        //print("Is Stunned = "+isStunned);
        if (!isStunned)
        {
            CheckOffMeshLink();
            if (assignWaypoint)
            {
                AssignWaypoint();
            }
            else if (moveToWaypoint)
            {
                MoveToWaypoint();
            }
            else if (persuePlayer)
            {
                PersuePlayer();
            }
            else if (doLaserAttack)
            {
                LaserAttack();
            }
        }
        else if(isStunned)
        {
            AnimController.SetBool("Stunned", true);
            uiComp.SetActive(true);
            if (!corutineActive)
            {
                EndAttack();
                StartCoroutine(StopIsStunned());
            }
        }
    }

    void CheckOffMeshLink()
    {
        if (NavAgent.isOnOffMeshLink && !usingNavLink)
        {
            //print("Jump");
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
            //print("At Waypoint");
            moveToWaypoint = false;
            doLaserAttack = true;
        }
    }

    void LaserAttack()
    {
        transform.LookAt(PlayerRef.transform.position);
        //Vector3 dir = PlayerRef.transform.position -transform.position;
        //transform.rotation = Quaternion.LookRotation(dir);
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
        if (Vector3.Distance(transform.position, PlayerRef.transform.position) <= 8.5f)
        {
            AnimController.SetTrigger("Melee");
            PlayerRef.GetComponent<PlayerController>().TakeDamage(0.1f);
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

    IEnumerator StopIsStunned()
    {
        corutineActive = true;
        yield return new WaitForSecondsRealtime(StunDuration);
        AnimController.SetBool("Stunned", false);
        uiComp.SetActive(false);
        isStunned = false;
        corutineActive = false;
    }

    IEnumerator Death()
    {
        yield return new WaitForSecondsRealtime(1);
        //print("Boss Defeated!");
    }

    public void BeginAttack()
    {
        //print("Attack Triggered!");
        laserScript.ElementIndex = Random.Range(0, 3);
        laserScript.PlayParticles(true);
    }

    public void ActivateBeam()
    {
        laserScript.Activated = true;
    }

    public void EndAttack()
    {
        //print("Attack End!");
        laserScript.Activated = false;
        laserScript.PlayParticles(false);
        doLaserAttack = false;
        blockTrigger = false;
        TryPersuePlayer();
    }
}
