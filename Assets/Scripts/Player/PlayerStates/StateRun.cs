using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRun : PlayerState
{
    private readonly Player _player;

    public StateRun(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Run");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Invoke()
    {
        base.Invoke();
    }
}
