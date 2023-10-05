using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StateMachine
{
    public PlayerState CurrState { get; private set; }

    public StateMachine(PlayerState initState)
    {
        CurrState = initState;

        CurrState.EnterState();
    }

    public void SetState(PlayerState playerState)
    {
        CurrState.ExitState();
        CurrState = playerState;
        CurrState.EnterState();
    }
}
