using System;
using UnityEngine;

public class WaitState : BaseState
{
    public Companion _companion;
    private Vector3 _companionPosition;
    private Vector3 playerPosition;
    private Vector3 _destination;

    public WaitState(Companion companion) : base(companion.gameObject)
    {
        _companion = companion;

    }

    public override Type Tick()
    {
        _companionPosition = _companion.transform.position;
        playerPosition = _companion.Player.position;
        var distance = Vector3.Distance(_companionPosition, playerPosition);

        if (distance > GameSettings.DistanceToWalk + 1)
        {
            return typeof(WalkState);
        } 

        if (_companion.NavAgent.remainingDistance <= 1f || distance <= 1.5f) // l'agent a atteint sa destination
        {
            _companion.Anim.SetBool("Avancer", false);
            _companion.NavAgent.isStopped = true;
            transform.LookAt(_companion.Player);
           
        } 

        return null;

    }


}
