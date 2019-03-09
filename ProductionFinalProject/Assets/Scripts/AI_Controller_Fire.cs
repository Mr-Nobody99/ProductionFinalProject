﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller_Fire : MonoBehaviour
{

    private GameObject playerRef;
    [SerializeField]
    private GameObject eyes;

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

    // Start is called before the first frame update
    void Start()
    {
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
        }
        else
        {
            animController.SetBool("isStopped", false);
        }

        //Change nav agent settings for patrol and persue behaviors
        if(persuePlayer)
        {
            animController.SetBool("persuePlayer", true);
            navMeshAgent.SetDestination(playerRef.transform.position);
            navMeshAgent.speed = 5.5f;
            navMeshAgent.stoppingDistance = 3.5f;
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
}
