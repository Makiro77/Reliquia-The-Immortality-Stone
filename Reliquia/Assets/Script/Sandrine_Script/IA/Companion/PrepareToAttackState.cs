using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareToAttackState : BaseState
{
    public Companion _companion;
    private Vector3 _companionPosition;
    private Vector3 playerPosition;
    private Vector3 targetPosition;
    private Transform chaseTarget;
    private Quaternion startingAngle = Quaternion.AngleAxis(-135, Vector3.up);
    private Quaternion checkPlayerAngle = Quaternion.AngleAxis(-5, Vector3.up);
    private Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    public PrepareToAttackState(Companion companion) : base(companion.gameObject)
    {
        _companion = companion;

    }

    public override Type Tick()
    {
        Debug.Log("prepareToAttak");
        _companionPosition = _companion.transform.position;
        playerPosition = _companion.Player.position;

        if (_companion.Target != null)
        {
            Debug.Log("target is not null");
            targetPosition = _companion.Target.position;
            Vector3 relativePos = targetPosition - _companionPosition;


            

            _companion.LookAt(relativePos, 10f);

            // Si le joueur est sistué entre la target et le compagnon, le compagnon s'arrête.
            if (checkBeInTheWayOfPlayer(targetPosition, playerPosition))
            {
                Debug.Log("checkBeInTheWayOfPlayer");
                return typeof(WaitState);
            }

            _companion.Move(targetPosition, GameSettings.SpeedAttackWalking);

            return typeof(CompagnonAttackState);
        }

        chaseTarget = CheckForAggro();
        if (chaseTarget != null)
        {
            

            _companion.SetTarget((Transform)chaseTarget);

            

            targetPosition = _companion.Target.position;

            Vector3 relativePos = targetPosition - _companionPosition;
            _companion.LookAt(relativePos, 10f);

            // Si le joueur est sistué entre la target et le compagnon, le compagnon s'arrête.
            if (checkBeInTheWayOfPlayer(targetPosition, playerPosition))
            {
                Debug.Log("checkBeInTheWayOfPlayer");
                return typeof(WaitState);
            }

            _companion.Move(targetPosition, GameSettings.SpeedAttackWalking);

            return typeof(CompagnonAttackState);
        }
        
        return typeof(WalkState);
    }

    private bool checkBeInTheWayOfPlayer(Vector3 _destination, Vector3 playerPosition)
    {

        RaycastHit hit;
        var angle = transform.rotation * checkPlayerAngle;
        var direction = angle * Vector3.forward;

        for (var i = 0; i < 2; i++)
        {
            if (Physics.Raycast(_companionPosition, direction, out hit, GameSettings.AggroRadius))
            {
                var target = hit.transform;
                if (target != null && target == _companion.Player )
                {
                    return true;
                }
            }

            direction = stepAngle * direction;
        }
        return false;
    }

    private Transform CheckForAggro()
    {
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;

        for (var i = 0; i < 45; i++)
        {

            if (Physics.Raycast(_companionPosition, direction, out hit, GameSettings.AggroRadius))
            {
                var target = hit.transform;

                if (target != null && null != target.GetComponent<Enemy>()) 
                {
                    Debug.DrawRay(_companionPosition, direction * hit.distance, Color.red);
                    return target.transform;
                }
                else
                {
                    Debug.DrawRay(_companionPosition, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(_companionPosition, direction * hit.distance, Color.white);
            }
            
            direction = stepAngle * direction;

        }
        return null;

    }

}
