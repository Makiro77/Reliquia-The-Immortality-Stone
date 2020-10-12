using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyControlState
{
    WALK,
    RUN,
    PAUSE,
    FOLLOW,
    GOBACK,
    ATTACK,
    DEATH
}

public class EnemyControl: MonoBehaviour
{
    // Enemy properties
    private bool finishedMovement = true;
    private bool finishedPause = false;
    private bool StartPause = false;
    private Animator anim;
    private NavMeshAgent navAgent;
    private Vector3 initialPosition;
    private Vector3 nextDestination;
    private float speed = 1f;
    private float speedAttack = 3f;
    [HideInInspector]
    public EnemyControlState currentState, lastState = EnemyControlState.WALK;

    // Walk Points
    public Transform[] walkPoints;
    private int walk_Index = 0;

    // Player Properties
    private Transform playerTarget;

    // Distance between player and enemy
    private float followDistance = 8f;
    private float attackDistance = 3f;
    private float goBackDistance = 10f;

    private float enemyToPlayerDistance;
    private float enemyToInitDistance;

    // Time
    private float waitPauseTime = 0f;
    private float waitTimeLimit = 2f;

    // Provisoire pour effet Attack
    public GameObject alphaSurface;
    private Renderer alphaRenderer;


    void Awake()
    {
        anim = GetComponent<Animator>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;

        alphaRenderer = alphaSurface.GetComponent<Renderer>(); // Provisoire Attack Effect

    }

    // Update is called once per frame
    void Update()
    {
        // Set Distances
        enemyToPlayerDistance = Vector3.Distance(transform.position, playerTarget.position);
        enemyToInitDistance = Vector3.Distance(transform.position, initialPosition);
       
        SetEnemyState();
        GetStateControl();
        
    }

    void SetEnemyState()
    {
        lastState = currentState;
        if (enemyToInitDistance > goBackDistance || currentState == EnemyControlState.GOBACK)
        {
            if (currentState == EnemyControlState.FOLLOW)
            {
                StartPause = false;
            }
            
            if (!StartPause)
            {
                currentState = EnemyControlState.PAUSE; // Pause 
            }
            else if (finishedPause) 
            {
                currentState = EnemyControlState.GOBACK; // Puis Go Back Home
            }

        } 
        else if (enemyToPlayerDistance < attackDistance && currentState != EnemyControlState.GOBACK)
        {            
            currentState = EnemyControlState.ATTACK; // Attack
        } 
        else if (enemyToPlayerDistance < followDistance && currentState != EnemyControlState.GOBACK)
        {
            if (!StartPause)  // Pause
            {
                currentState = EnemyControlState.PAUSE;
            } 
            else if (finishedPause) // Puis Follow Player
            {
                currentState = EnemyControlState.FOLLOW;
            }

        }
        else
        {
            currentState = EnemyControlState.WALK;
            waitPauseTime = 0;
            finishedPause = false;
            StartPause = false;
        }
    }

    void GetStateControl()
    {

        if (currentState == EnemyControlState.PAUSE)
        {
            if (finishedPause) // New Pause, reset timer
            {
                waitPauseTime = 0f;
                finishedPause = false;
                StartPause = true;
            }
            waitPauseTime += Time.deltaTime;
            anim.SetBool("Avancer", false);
            navAgent.isStopped = true;

            if (waitPauseTime > waitTimeLimit) // End Pause 
            {
                finishedPause = true;
            }
        } 
        else if (currentState == EnemyControlState.WALK)
        {
            alphaRenderer.material.SetColor("_ColorTint", Color.white); // Provisoire 
            anim.SetBool("Avancer", true);
            move();
        }
        else if (currentState == EnemyControlState.PAUSE)
        {          
            anim.SetBool("Avancer", false);
            navAgent.isStopped = true;
        }
        else if (currentState == EnemyControlState.FOLLOW)
        {
            alphaRenderer.material.SetColor("_ColorTint", Color.white); // Provisoire 
            anim.SetBool("Avancer", true);
            followPlayer();
        }
        else if (currentState == EnemyControlState.ATTACK)
        {
            alphaRenderer.material.SetColor("_ColorTint", Color.red); // Provisoire
            anim.SetBool("Avancer", false);
            navAgent.isStopped = true;
        }
        else if (currentState == EnemyControlState.GOBACK)
        {
            alphaRenderer.material.SetColor("_ColorTint", Color.white); // Provisoire 
            anim.SetBool("Avancer", true);
            goBackHome();
        }
    }

    void move()
    {
        float distance = Vector3.Distance(transform.position, playerTarget.position);

        if (distance > followDistance)
        {
            if (navAgent.remainingDistance <= 0.5f)
            {

                nextDestination = walkPoints[walk_Index].position;

                navAgent.isStopped = false;
                navAgent.speed = speed;
                navAgent.SetDestination(nextDestination);

                if (walk_Index == walkPoints.Length - 1)
                {
                    walk_Index = 0;
                }
                else
                {
                    walk_Index++;
                }

            }
        }
        else if (distance > attackDistance)
        {
            navAgent.isStopped = false;
            navAgent.speed = speedAttack;
            navAgent.SetDestination(playerTarget.position);
        }
        else if (distance <= attackDistance)
        {
            navAgent.isStopped = true;
        }

    }

    void followPlayer()
    {
        navAgent.isStopped = false;
        navAgent.speed = speedAttack;
        navAgent.SetDestination(playerTarget.position);
    }

    void goBackHome()
    {
        if (navAgent.remainingDistance >= 0.5f)
        {
            navAgent.isStopped = false;
            navAgent.speed = speedAttack;
            navAgent.SetDestination(initialPosition);
        } else
        {
            currentState = EnemyControlState.WALK;
        }
    }
}
