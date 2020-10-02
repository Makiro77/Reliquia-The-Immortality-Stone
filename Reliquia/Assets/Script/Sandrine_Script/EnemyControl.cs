using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public Transform[] walkPoints;
    private int walk_Index = 0;

    private Transform playerTarget;

    private NavMeshAgent navAgent;

    private float walk_Distance = 8f;
    private float attack_Distance = 1.5f; //2f;

    private Vector3 nextDestination;


    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float distance = Vector3.Distance(transform.position, playerTarget.position);

        if (distance > walk_Distance)
        {
            if (navAgent.remainingDistance <= 0.5f)
            {
                navAgent.isStopped = false;

                nextDestination = walkPoints[walk_Index].position;
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
        else if (distance > attack_Distance) 
        {
            navAgent.isStopped = false;
            navAgent.SetDestination(playerTarget.position);
        }
        else if (distance <= attack_Distance)
        {
            navAgent.isStopped = true;
        }

    }
}
