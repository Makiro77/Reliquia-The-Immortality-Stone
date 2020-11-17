using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    // Propriétés privées de l'IA
    private Animator anim;
    private NavMeshAgent navAgent;
    private Vector3 initPosition;
    [SerializeField] private float enemyWanderSpeed = 1f;
    [SerializeField] private float enemyChaseSpeed = 2f;
    [SerializeField] private float enemyAttackSpeed = 2f;

    // Zone colorée autour de l'IA à supprimer
    public GameObject alphaSurface;  // Provisoire à sup
    [HideInInspector]
    public Renderer alphaRenderer;  // Provisoire à sup

    public Transform Target { get; private set; }
    
    // Les getters des propriétés de l'IA
    public StateMachine StateMachine => GetComponent<StateMachine>();
    public Vector3 InitPosition => initPosition;
    public float EnemyWanderSpeed => enemyWanderSpeed;
    public float EnemyChaseSpeed => enemyChaseSpeed;
    public float EnemyAttackSpeed => enemyAttackSpeed;
    public Animator Anim { get { return anim; } }
    public NavMeshAgent NavAgent { get { return navAgent; } }


    private void Awake()
    {
        InitializeStateMachine();
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        initPosition = transform.position;

        // A supprimer
        alphaRenderer = alphaSurface.GetComponent<Renderer>(); // Provisoire Attack Effect
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(WanderState), new WanderState(enemy: this) },
            {typeof(ReturnState), new ReturnState(enemy: this) },
            {typeof(ChaseState), new ChaseState(enemy: this) },
            {typeof(AttackState), new AttackState(enemy: this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }


    /// <summary>
    /// fonction qui décrit les actions de l'attaque 
    /// Spécifique à chaque ennemi
    /// Pour l'instant l'IA s'arrête et sa couleur est modifée
    /// </summary>
    internal void LaunchAttack()
    {
        //if (NavAgent.remainingDistance <= 2f) // l'agent a atteint sa destination
        //{
            // assigne une nouvelle destination et rotation          
            navAgent.isStopped = true;
            // Set anim Attack
            alphaRenderer.material.SetColor("_ColorTint", Color.black); // Provisoire
            anim.SetBool("Avancer", false);
        //}

        

    }
    /// <summary>
    /// Assigne la cible à l'IA
    /// </summary>
    /// <param name="target">la cible</param>
    public void SetTarget(Transform target)
    {
        Target = target;
        if (target == null)
        {
            alphaRenderer.material.SetColor("_ColorTint", Color.white); // Provisoire 
        }
    }

}
