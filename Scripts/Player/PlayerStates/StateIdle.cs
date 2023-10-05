using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : PlayerState
{
    private readonly Player _player;

    public StateIdle(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Idle");

        _player.HeadAnimator.SetTrigger("Idle");
    }

    public override void ExitState()
    {
        base.ExitState();

        _player.HeadAnimator.ResetTrigger("Idle");
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
