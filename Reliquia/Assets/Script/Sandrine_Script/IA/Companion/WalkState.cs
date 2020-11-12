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

        if (distance >= GameSettings.DistanceToWalk)
        {
            _companion.Anim.SetBool("Avancer", true);
            _companion.NavAgent.isStopped = false;
            FollowPlayer(); // assigne une nouvelle destination et rotation          
            transform.rotation = Quaternion.Lerp(transform.rotation, _desiredRotation, Time.deltaTime * 0.5f);
            _companion.NavAgent.SetDestination(_destination);
        } 
        else
        {
            _destination = new Vector3(playerPosition.x, y: 1f, playerPosition.z);
            Vector3 gap = new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), 0, UnityEngine.Random.Range(-2.0f, 2.0f));
            _companion.NavAgent.SetDestination(_destination + gap);

            return typeof(WaitState);
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
