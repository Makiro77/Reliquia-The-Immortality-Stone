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

        _destination = new Vector3(playerPosition.x, y: 1f, playerPosition.z);

        float gapX = playerPosition.x < _companionPosition.x ? -1f : 1f;
        float gapZ = playerPosition.z < _companionPosition.z ? -1f : 1f;
        Vector3 gap = _companionPosition.x - playerPosition.x > _companionPosition.z - playerPosition.z ? 
            new Vector3(gapX, 0, 0) : new Vector3(0, 0, gapZ);

        _companion.NavAgent.SetDestination(_destination + gap);

        if (_companion.NavAgent.remainingDistance <= 2.5f) // l'agent a atteint sa destination
        {
            _companion.Anim.SetBool("Avancer", false);
            _companion.NavAgent.isStopped = true;
            transform.LookAt(_companion.Player);
           
        }

        if (distance > 3f)
        {
            return typeof(WalkState);
        }

        return null;

    }


}
