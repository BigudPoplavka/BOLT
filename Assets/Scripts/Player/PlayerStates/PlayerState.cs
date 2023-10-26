public abstract class PlayerState
{
    protected PlayerState() { }

    public virtual void EnterState() { }

    public virtual void ExitState() { }

    public virtual void Invoke() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }
}
