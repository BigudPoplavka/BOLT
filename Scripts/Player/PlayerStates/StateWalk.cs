using UnityEngine;

public class StateWalk : PlayerState
{
    private readonly Player _player;

    public StateWalk(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Walk");

        _player.HeadAnimator.SetTrigger("Walk");
    }

    public override void ExitState()
    {
        base.ExitState();

        _player.HeadAnimator.ResetTrigger("Walk");
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
