using System;
using UnityEngine;

public class LostState : BaseState
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

    public LostState(Companion companion) : base(companion.gameObject)
    {
        _companion = companion;

    }
    public override Type Tick()
    {
        
        _companionPosition = _companion.transform.position;
        playerPosition = _companion.Player.position;

        var distance = Vector3.Distance(_companionPosition, playerPosition);

        if (distance >= 10f)
        {
            Debug.Log("Go to walkState 6");
            return typeof(WalkState);
        }

        _direction = playerPosition; 

        Vector3 lookAtDirection = _direction; 
        transform.LookAt(lookAtDirection);

        RaycastHit hit;
        Quaternion angle = transform.rotation;
        var direction = angle * Vector3.forward;
        if (Physics.Raycast(_companionPosition, direction, out hit, GameSettings.AggroRadius / 5f))
        {
            Transform target = hit.transform;
            Debug.DrawRay(_companionPosition, direction, Color.green);

            Companion otherCompanion = target.GetComponent<Companion>();
            if (target != null && otherCompanion != null)
            {
                return typeof(WaitState);
            }
            if (target != null && target == _companion.Player)
            {
                return typeof(WaitState); //WalkState
            }
        }

        _companion.Move(playerPosition, GameSettings.SpeedWalking);

        return null;
    }
}
