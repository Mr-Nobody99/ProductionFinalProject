using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class AI_Controller_Water : MonoBehaviour
{

    Animator animController;
    NavMeshAgent navMeshAgent;
    private GameObject playerRef;

    [SerializeField]
    List<AI_Waypoint> patrolPoints;
    private int currentPatrolIndex;
    [SerializeField]
    float directionSwitchProbability = 0.2f;

    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    float persueDistance;

    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Transform projectileSpawner;
    bool shootActive = false;
    [SerializeField]
    Transform eyes;

    private bool isPatrolling;
    private bool persuePlayer;

    private RaycastHit Hit;

    // Start is called before the first frame update
    void Start()
    {
        animController = this.GetComponent<Animator>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        playerRef = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxHealth;

        if(patrolPoints != null && patrolPoints.Count >= 2)
        {
            currentPatrolIndex = 0;
            navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].transform.position);
            isPatrolling = true;
        }
        else
        {
            print("Not enough patrol points set!");
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

        if (persuePlayer)
        {
            navMeshAgent.SetDestination(playerRef.transform.position);
            navMeshAgent.stoppingDistance = persueDistance;
            navMeshAgent.speed = 5.5f;

            if (!shootActive)
            {
                StartCoroutine(Shoot());
            }

            float speed = navMeshAgent.velocity.magnitude > 0 ? (navMeshAgent.speed / navMeshAgent.velocity.magnitude) : 0;
            animController.SetFloat("Speed", speed);
        }
        else if(isPatrolling)
        {
            navMeshAgent.stoppingDistance = 1f;
            navMeshAgent.speed = 2f;
            animController.SetFloat("Speed", 0.25f);
        }

        if(isPatrolling && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            ChangePatrolPoint();
        }

        if (!persuePlayer)
        {
            Debug.DrawLine(eyes.transform.position, eyes.transform.position + eyes.transform.forward * 10f, Color.green);
            if (Physics.SphereCast(eyes.transform.position, 5.0f, eyes.transform.forward, out Hit, 50.0f))
            {
                if (Hit.collider.tag.Equals("Player"))
                {
                    persuePlayer = true;
                    isPatrolling = false;
                }
            }
        }
    }

    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= directionSwitchProbability)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        else if (--currentPatrolIndex < 0)
        {
            currentPatrolIndex = patrolPoints.Count - 1;
        }

        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].transform.position);

    }

    IEnumerator Shoot()
    {
        shootActive = true;
        Vector3 target = playerRef.transform.position;
        target.y += 1;
        projectileSpawner.LookAt(target);
        Instantiate(projectile, projectileSpawner.position, projectileSpawner.rotation);
        yield return new WaitForSecondsRealtime(Random.Range(1.0f, 2.0f));
        shootActive = false;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
