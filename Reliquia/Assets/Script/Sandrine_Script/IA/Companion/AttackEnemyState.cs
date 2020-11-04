using System;
using UnityEngine;

public class AttackEnemyState : BaseState
{
    public Companion _companion;


    public AttackEnemyState(Companion companion) : base(companion.gameObject)
    {
        _companion = companion;

    }

    public override Type Tick()
    {
        
        return null;
    }


}
