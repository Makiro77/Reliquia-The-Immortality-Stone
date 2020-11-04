using System;
using UnityEngine;

public class WalkState : BaseState
{
    public Companion _companion;
    private Vector3 _companionPosition;

    private Vector3 playerPosition;

    private float seekCounter = 0;
    private bool flagStopWainting = false;

    private Vector3 _destination;
    private Vector3 _direction;
    private Quaternion _desiredRotation;


    public WalkState(Companion companion) : base(companion.gameObject)
    {
        _companion = companion;

    }

    public override Type Tick()
    {
        _companionPosition = _companion.transform.position;
        playerPosition = _companion.Player.position;
        var distance = Vector3.Distance(_companionPosition, playerPosition);

        if (distance >= 3f)
        {
            _companion.Anim.SetBool("Avancer", true);
            _companion.NavAgent.isStopped = false;
            FollowPlayer(); // assigne une nouvelle destination et rotation          
            transform.rotation = Quaternion.Lerp(transform.rotation, _desiredRotation, Time.deltaTime * 0.5f);
            _companion.NavAgent.SetDestination(_destination);
        } 
        else
        {
            var xPlayer = playerPosition.x;
            var zPlayer = playerPosition.z;
            _companion.NavAgent.SetDestination(_destination + new Vector3(1, 0, 0));
            if (_companion.NavAgent.remainingDistance <= 0.5f) // l'agent a atteint sa destination
            {
                _companion.Anim.SetBool("Avancer", false);
                _companion.NavAgent.isStopped = true;
                transform.LookAt(_companion.Player);
            }
        }

        return null;
    }

    private void FollowPlayer()
    {
        transform.LookAt(_companion.Player);
        _destination = new Vector3(playerPosition.x, y: 1f, playerPosition.z);

        _direction = Vector3.Normalize(_destination - _companionPosition);
        _direction = new Vector3(_direction.x, y: 0f, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);
    }

}
