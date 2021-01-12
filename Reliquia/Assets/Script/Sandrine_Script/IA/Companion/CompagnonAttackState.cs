using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompagnonAttackState : BaseState
{
    public Companion _companion;
    private Vector3 _companionPosition;
    private Vector3 targetPosition;
    private Vector3 playerPosition;

    private float _attackReadyTimer = 100f;

    public CompagnonAttackState(Companion companion) : base(companion.gameObject)
    {
        _companion = companion;

    }

    public override Type Tick()
    {
        if (_companion.Target == null)
            return typeof(WalkState);


        // Assignation des positions
        _companionPosition = _companion.transform.position;
        targetPosition = _companion.Target.position;
        playerPosition = _companion.Player.position;

        _companion.SetSpeed(_companion.CompanionAttackSpeed);

        var distance = Vector3.Distance(_companionPosition, targetPosition);
        var distanceToPlayer = Vector3.Distance(_companionPosition, playerPosition);

        
        _attackReadyTimer -= Time.deltaTime;

        if (_companion.NavAgent.remainingDistance <= 5f && _attackReadyTimer <= 0f)
        {

            Vector3 relativePos = targetPosition - _companionPosition;
            _companion.LookAt(relativePos, 10f);
            _companion.Attack(3f);
            _attackReadyTimer = 100f;

        }

        // Si le joeur sort de la zone d'attaque
        // Retour à l'état Chase
        if (_companion.NotAttacking() && distanceToPlayer > GameSettings.PlayerLeavingRange)
        {
            _companion.SetTarget(null);
            return typeof(WalkState);
        }

        // Suivre l'ennemi et continuer à attaquer
        if (distance >= 5f ) // To Replace GameSettings.FollowInAttackStateDistance) //2f
        {
            Vector3 relativePos = targetPosition - _companionPosition;
            _companion.LookAt(relativePos, 10f);

            _companion.Move(targetPosition, GameSettings.SpeedAttackWalking); //3f

        }

        return null; 
    }
}
