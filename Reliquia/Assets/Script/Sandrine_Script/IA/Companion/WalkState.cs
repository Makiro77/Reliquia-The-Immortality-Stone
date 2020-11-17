using System;
using UnityEngine;

public class WalkState : BaseState
{
    public Companion _companion;
    private Vector3 playerLastPosition;
    private Vector3 _companionPosition;

    private Vector3 playerPosition;

    private float seekCounter = 0;
    private bool flagStopWainting = false;

    private Vector3 _destination;
    private Vector3 _direction;
    private Quaternion _desiredRotation;

    private Quaternion stepAngle = Quaternion.AngleAxis(15, Vector3.up);

    public WalkState(Companion companion) : base(companion.gameObject)
    {
        _companion = companion;

    }

    public override Type Tick()
    {
        _companionPosition = _companion.transform.position;

        playerLastPosition = playerPosition;
        playerPosition = _companion.Player.position;

        var distance = Vector3.Distance(_companionPosition, playerPosition);

        FollowPlayer(); // assigne une nouvelle destination et rotation au compagnon
        
        transform.rotation = Quaternion.Lerp(transform.rotation, _desiredRotation, Time.deltaTime * 0.5f);
        _companion.NavAgent.SetDestination(_destination);


        // si le player court alors compagnon cours aussi
        if ( !_companion.AnimPlayer.IsInTransition(0) && _companion.AnimPlayer.GetCurrentAnimatorStateInfo(0).IsName("Running"))
        {
            _companion.NavAgent.speed = GameSettings.SpeedRunning;
            _companion.Anim.SetBool("Course", true);
            _companion.Anim.SetBool("Avancer", true);
            _companion.NavAgent.isStopped = false;
            return null;

        }

        // si le player s'arrete alors compagnon s'arrete aussi 
        if (!_companion.AnimPlayer.IsInTransition(0) && _companion.AnimPlayer.GetCurrentAnimatorStateInfo(0).IsName("run to stop"))
        {
            _companion.Anim.SetBool("Course", false);
            _companion.Anim.SetBool("Avancer", false);
            _companion.NavAgent.speed = 0;
            return typeof(WaitState);
        }

        // Si le player arrive à destination ou que la distance <= DistanceToWalk
        // Alors return typeof(WaitState);
        if (_companion.NavAgent.remainingDistance <= 0.05f && playerLastPosition == playerPosition) // || distance < GameSettings.DistanceToWalk
        {
            return typeof(WaitState);

        }

        // Si le player arrive à destination
        // Alors return typeof(WaitState);
        if (_companion.NavAgent.remainingDistance <= 0.05f) // || distance < GameSettings.DistanceToWalk
        {
            return typeof(WaitState);

        }

        // Si le compagnon arrive trop pres du player
        // Alors return typeof(WaitState);
        if (distance <= GameSettings.DistanceToWalk) // || distance < GameSettings.DistanceToWalk
        {
            return typeof(WaitState);

        }

        // Par défaut
        _companion.Anim.SetBool("Course", false);
        _companion.Anim.SetBool("Avancer", true);
        _companion.NavAgent.speed = GameSettings.SpeedWalking + Vector3.Distance(_destination, _companionPosition) / (Time.deltaTime * 20f);
        _companion.NavAgent.isStopped = false;

        return null;
    }

    private void FollowPlayer()
    {

        Vector3 _lastDirection = _direction == Vector3.zero ? playerPosition - _companionPosition : _direction;

        // Par défaut Compagnon = Roxane
        _destination = playerPosition + _companion.Player.right;

        if (_companion.Name == "David")
        {
            _destination = playerPosition - _companion.Player.right;
        }

        // Vérifier que la destination du compagnon est libre
        // Sinon placer le compagnon de l'autre côté du player
        float rayDistance = Vector3.Distance(_destination, _companionPosition);
        RaycastHit hit;
        Vector3 rayDirection = (_destination - _companionPosition) + Vector3.up;

        for (var i = 0; i < 2; i++)
        {
            if (Physics.Raycast(_companionPosition, rayDirection, out hit, rayDistance))
            {

                var target = hit.transform;
                if (target != null && target != _companion.Player)
                {

                    _destination = playerPosition - _companion.Player.right;
                    if (_companion.Name == "David")
                    {
                        _destination = playerPosition + _companion.Player.right;
                    }
                        break;
                }
            }

            rayDirection = stepAngle * rayDirection;
        }

        _destination = new Vector3(_destination.x, y: 1f, _destination.z);

        // _direction vers laquelle regarde le compagnon en se déplaçant
        _direction = playerPosition - playerLastPosition == Vector3.zero ? _lastDirection : (playerPosition - playerLastPosition );

        Vector3 lookAtDirection = _companionPosition + _direction; 
        _desiredRotation = Quaternion.LookRotation(lookAtDirection);
    }

}
