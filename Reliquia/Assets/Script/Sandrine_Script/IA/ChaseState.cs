﻿using System;
using UnityEngine;

public class ChaseState : BaseState
{
    public Enemy _enemy;
    private Vector3 _enemyPosition;

    private float seekCounter = 0;
    private bool flagStopWainting = false;


    public ChaseState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;

    }

    public override Type Tick()
    {
        if (_enemy.Target == null)
            return typeof(WanderState);

        // Assigne la position de l'ennemi
        _enemyPosition = _enemy.transform.position;
        _enemy.NavAgent.speed = _enemy.EnemyChaseSpeed;

        // l'IA arrête de suivre la cible et revient à sa position initiale
        // si elle est au bord du Nav Mesh 
        // L'unique cas dans cet état où elle peut rejoindre sa destination.
        if (_enemy.NavAgent.remainingDistance <= 0.5f)
        {
            _enemy.SetTarget(null);
            return typeof(ReturnState);
        }

        // Suivre la cible
        followTarget(_enemy.Target);

        var distance = Vector3.Distance(transform.position, _enemy.Target.transform.position);

        // Si le player passe en zone rouge => chgmt d'état vers AttackState.
        // Dans cette zone le joueur ne peut plus se cacher.
        if (distance <= GameSettings.AttackRange) 
        {
            if (_enemy.Target != null)
                return typeof(AttackState);
        }

        // si le joueur sort de la zone de pistage
        // le joeur n'est plus la cible de l'IA.
        if (distance > GameSettings.ChaseRange) 
        {
            _enemy.SetTarget(null);
            return null;
        }

        // si le joueur se cache
        // le joeur n'est plus la cible de l'IA
        if (!CheckToContinue(GameSettings.ChaseWaintingTime)) // Check if target hide.
        {
            _enemy.SetTarget(null);
            return null;
        }

        seekCounter++; // compteur utilisé pour la fonction CheckToContinue

        return null;
    }

    //// <summary>
    /// La fonctionne assigne la position du joueur comme destination de l'IA
    /// </summary>
    /// <param name="target">La cible de l'IA</param>
    private void followTarget(Transform target)
    {
        _enemy.NavAgent.isStopped = false;
        _enemy.Anim.SetBool("Avancer", true);
        transform.LookAt(target);
        _enemy.NavAgent.SetDestination(target.position);
    }

    //// <summary>
    /// Vérifie si le joueur est caché 
    /// </summary>
    /// <param name="waitTime">Internvelle de temps entre 2 vérifications</param>
    /// <returns>retourne true s'il est caché et false sinon </returns>
    private bool CheckToContinue(float waitTime)
    {
        if (seekCounter >= waitTime)
        {
            flagStopWainting = true;
            seekCounter = 0;
        }

        var target = transform;
        var pos = transform.position;
        RaycastHit hit;

        if (flagStopWainting && 
            Physics.Raycast(pos, _enemy.Target.position - pos, out hit, GameSettings.ChaseRange)) 
        {
            target = hit.transform;
            flagStopWainting = false;
        }

        if (target != null && target != _enemy.Target)
        {
            return false;
        }
        return true;
    }

}
