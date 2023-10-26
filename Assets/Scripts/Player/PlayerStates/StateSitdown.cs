using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSitdown : PlayerState
{
    private readonly Player _player;

    public StateSitdown(Player player)
    {
        _player = player;
    }
    public override void EnterState()
    {
        base.EnterState();
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
