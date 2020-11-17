using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Companion : MonoBehaviour
{
    // Propriétés privées de l'IA
    private Animator anim;
    private NavMeshAgent navAgent;

    public String Name;

    private Transform player;
    private Animator animPlayer;

    

    public Transform Player => player;
    public Animator Anim => anim;
    public NavMeshAgent NavAgent => navAgent;

    public Animator AnimPlayer => animPlayer;


    // A supprimer
    public GameObject alphaSurface;  // Provisoire à sup
    [HideInInspector]
    public Renderer alphaRenderer;  // Provisoire à sup

    private void Awake()
    {
        InitializeStateMachine();
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        animPlayer = player.GetComponent<Animator>();

        // A supprimer
        alphaRenderer = alphaSurface.GetComponent<Renderer>(); // Provisoire Attack Effect
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(ReachPlayerState), new ReachPlayerState(companion: this) },
            {typeof(WalkState), new WalkState(companion: this) },
            {typeof(WaitState), new WaitState(companion: this) },
            {typeof(AttackEnemyState), new AttackEnemyState(companion: this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }

}