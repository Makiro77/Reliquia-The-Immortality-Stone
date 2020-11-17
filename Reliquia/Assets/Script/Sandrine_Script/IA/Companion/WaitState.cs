﻿using System;
using UnityEngine;

public class WaitState : BaseState
{
    public Companion _companion;
    private Vector3 _companionPosition;
    private Vector3 playerPosition;
    private Vector3 _destination;
    private Vector3 _direction;
    private Quaternion _desiredRotation;

    public WaitState(Companion companion) : base(companion.gameObject)
    {
        _companion = companion;

    }

    public override Type Tick()
    {
        _companionPosition = _companion.transform.position;
        playerPosition = _companion.Player.position;
        var distance = Vector3.Distance(_companionPosition, playerPosition);

        // si le compagnon est toujours en train de courrir alors il doit s'arrêter.
        if (!_companion.Anim.IsInTransition(0) && _companion.Anim.GetCurrentAnimatorStateInfo(0).IsName("Running"))
        {
            _companion.Anim.SetBool("Course", false);
            _companion.Anim.SetBool("Avancer", false);

            _companion.NavAgent.speed = 0;
            return null;
        }

        // si le player marche le compagnon passe à l'état Walk
        if (distance > GameSettings.DistanceToWalk || 
            (!_companion.AnimPlayer.IsInTransition(0) && _companion.AnimPlayer.GetCurrentAnimatorStateInfo(0).IsName("Walking")) )
        {
            return typeof(WalkState);
        }

        // Si le compagnon marche il doit s'arrêter
        if (!_companion.Anim.IsInTransition(0) && _companion.Anim.GetCurrentAnimatorStateInfo(0).IsName("Walking")) {
        
            _companion.Anim.SetBool("Avancer", false);
            _companion.NavAgent.isStopped = true;
            return null;
        }

        // Lorsue le compagnon s'est arrêté, il regarde vers le player
        if (!_companion.Anim.IsInTransition(0) && _companion.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            _direction = playerPosition - _companionPosition;
            _desiredRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, _desiredRotation, Time.deltaTime * 5f); 

        }

        return null;

    }


}
