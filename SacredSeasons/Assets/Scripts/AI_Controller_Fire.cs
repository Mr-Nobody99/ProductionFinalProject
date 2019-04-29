using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI_Controller_Fire : MonoBehaviour
{

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float currentHealth;
    private bool blockDamage = false;
    [SerializeField]
    float persueDistance = 5.0f;

    private GameObject playerRef;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Transform projectileSpawner;
    bool shootActive = false;
    [SerializeField]
    private GameObject eyes;
    [SerializeField]
    private GameObject iceBlockPrefab;

    Animator animController;
    NavMeshAgent navMeshAgent;

    RaycastHit Hit;

    [SerializeField]
    List<AI_Waypoint> patrolPoints;

    private int currentPatrolIndex;
    [SerializeField]
    float directionSwitchProbability = 0.2f;

    private bool isPatrolling;
    private bool persuePlayer;
    private bool frozen = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        animController = this.GetComponent<Animator>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if(navMeshAgent == null)
        {
            print("There is no navMeshAgent component attached to:" + gameObject.name);
        }
        else
        {
            if(patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolIndex = 0;
                navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].transform.position);
                isPatrolling = true;
            }
            else
            {
                print("Not enough patrol points set");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Hit.point, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //print(navMeshAgent.velocity.magnitude);

        //Set animation bool for idle/walk
        if(navMeshAgent.velocity.magnitude <= 0.0f)
        {
            animController.SetBool("isStopped", true);
            transform.LookAt(playerRef.transform.position, Vector3.up);
        }
        else
        {
            animController.SetBool("isStopped", false);
        }

        //Change nav agent settings for patrol and persue behaviors
        if(persuePlayer)
        {
            if (!shootActive && !frozen)
            {
                StartCoroutine(Shoot());
            }
            animController.SetBool("persuePlayer", true);
            navMeshAgent.SetDestination(playerRef.transform.position);
            navMeshAgent.speed = 5.5f;
            navMeshAgent.stoppingDistance = persueDistance;

            // Lose health on collision with spirit
            //if(Vector3.Distance(transform.position, playerRef.transform.position) <= 3.0f)
            //{
            //  print("Collision with player");
            //  playerRef.GetComponent<PlayerController>().TakeDamage(0.1f);
            //}
        }
        else if(isPatrolling)
        {
            animController.SetBool("persuePlayer", false);
            navMeshAgent.speed = 2f;
            navMeshAgent.stoppingDistance = 1f;
        }


        if(isPatrolling && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            ChangePatrolPoint();
        }

        Debug.DrawLine(eyes.transform.position, eyes.transform.position + eyes.transform.forward * 10f, Color.green);
        if (Physics.SphereCast(eyes.transform.position, 5.0f, eyes.transform.forward, out Hit, 50.0f))
        {
            if(Hit.collider.tag == "Player")
            {
                if(!persuePlayer)
                {
                    animController.SetTrigger("playerSpotted");
                }
                persuePlayer = true;
                isPatrolling = false;
            }
        }
    }

        public void TakeDamage(float dmg)
        {
            if (!blockDamage)
            {
                if (frozen) blockDamage = true;
                currentHealth -= dmg;
                healthBar.fillAmount = currentHealth / maxHealth;

                if (currentHealth <= 0)
                {
                    Die();
                }
            }
        }

    private void ChangePatrolPoint()
    {
        if(UnityEngine.Random.Range(0f,1f) <= directionSwitchProbability)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        else if(--currentPatrolIndex < 0)
        {
            currentPatrolIndex = patrolPoints.Count - 1;
        }

        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].transform.position);

    }

    IEnumerator Shoot()
    {
        if (!frozen)
        {
            shootActive = true;
            Vector3 target = playerRef.transform.position;
            target.y += 1;
            projectileSpawner.LookAt(target);
            Instantiate(projectile, projectileSpawner.position, projectileSpawner.rotation);
        }
        yield return new WaitForSecondsRealtime(Random.Range(1.0f, 2.0f));
        shootActive = false;
    }

    public void Freeze()
    {
        if (!frozen)
        {
            frozen = true;
            animController.SetTrigger("Freeze");
            navMeshAgent.isStopped = true;
            StartCoroutine(FreezeDelay());
        }
    }

    IEnumerator FreezeDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        animController.SetBool("Frozen", frozen);
        var Ice = Instantiate(iceBlockPrefab, transform.position, transform.rotation);
        yield return new WaitForSecondsRealtime(5);
        Destroy(Ice);
        navMeshAgent.isStopped = false;
        frozen = false;
        animController.SetBool("Frozen", frozen);
        blockDamage = false;
    }



    private void Die()
    {
        PlayerScore.AddToCurrentScore(100);
        Destroy(gameObject);
    }
}
