using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Move,
    Attack,
    Hit,
    Die,
    End
}

public abstract class FSM<T>
{
    public abstract void Begine();
    public abstract void Run();
    public abstract void Exit();
}

public class Machine<T>
{
    private T owner;
    private FSM<T> currState = null;
    private FSM<T> prevState = null;

    public void Begine()
    {
        if(currState != null)
        {
            currState.Begine();
        }
    }

    public void Run()
    {
        CheckState();
    }

    public void Exit()
    {
        currState.Exit();
        currState = null;
        prevState = null;
    }

    public void CheckState()
    {
        if(currState != null)
        {
            currState.Run();
        }
    }

    public void ChangeState(FSM<T> state)
    {
        if (currState == state)
            return;

        prevState = currState;
        if (currState != null)
            currState.Exit();

        currState = state;
        if (currState != null)
            currState.Begine();
    }

    public void Revert()
    {
        if (prevState != null)
            ChangeState(prevState);
    }

    public void SetState(FSM<T> state, T owner)
    {
        this.owner = owner;
        currState = state;
        if (currState != state && currState != null)
            prevState = currState;
    }
}
