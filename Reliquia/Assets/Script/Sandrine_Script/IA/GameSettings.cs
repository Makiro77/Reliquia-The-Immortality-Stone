using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float aggroRadius = 20f;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float chaseRange = 15f;
    [SerializeField] private float chaseWaitingTime = 30f;



    private bool flagEndGame = false;
    private float gameTime;


    public static GameSettings Instance { get; private set; }

    public static float Speed => Instance.speed;
    public static float AggroRadius => Instance.aggroRadius;
    public static float AttackRange; // => Instance.attackRange;
    public static float ChaseRange => Instance.chaseRange;
    public static float ChaseWaintingTime => Instance.chaseWaitingTime;

    private void Start()
    {
        gameTime = Time.time;
    }
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        AttackRange = Instance.attackRange;
    }
}