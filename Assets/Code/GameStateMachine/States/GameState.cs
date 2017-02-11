using UnityEngine;
using System.Collections;

public abstract class GameState 
{
    public GameStateMachine StateMachine;

    public virtual void EnterState()
    {
        
    }

    public virtual void Update()
    {
        
    }

    public virtual void ExitState()
    {
        
    }
}
