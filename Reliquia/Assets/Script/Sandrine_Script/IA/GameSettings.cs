using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    // Enemy Setting
    //[SerializeField] private float speed = 2f;
    public float speed = 2f; // TOSUP
    //[SerializeField] private float aggroRadius = 20f;
    public float aggroRadius = 20f;
    //[SerializeField] private float attackRange = 3f;
    public float attackRange = 3f;
    //[SerializeField] private float chaseRange = 15f;
    public float chaseRange = 15f;
    //[SerializeField] private float outOfChaseRange = 20f;
    public float outOfChaseRange = 20f;
    //[SerializeField] private float chaseWaitingTime = 4500;
    public float chaseWaitingTime = 4500;


    // Companion Settings
    //[SerializeField] private float distanceToWalk = 4f;
    public float distanceToWalk = 5f;




    public static GameSettings Instance { get; private set; }

    public static float Speed => Instance.speed;
    public static float AggroRadius => Instance.aggroRadius;
    public static float AttackRange; // => Instance.attackRange;
    public static float ChaseRange => Instance.chaseRange;
    public static float OutOfChaseRange => Instance.outOfChaseRange;
    public static float ChaseWaintingTime => Instance.chaseWaitingTime;
    public static float DistanceToWalk => Instance.distanceToWalk;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        AttackRange = Instance.attackRange;
    }
}