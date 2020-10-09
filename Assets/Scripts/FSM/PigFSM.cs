using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigFSM : MonoBehaviour
{
    public Machine<PigFSM> machine;
    public FSM<PigFSM>[] states = new FSM<PigFSM>[(int)State.End];

    public State currState;
    public State prevState;

    public PigFSM()
    {
        Init();
    }

    public void Init()
    {
        machine = new Machine<PigFSM>();

        states[(int)State.Idle] = new PigIdle(this);
        states[(int)State.Move] = new PigMove(this);
        states[(int)State.Attack] = new PigAttack(this);
        states[(int)State.Hit] = new PigHit(this);
        states[(int)State.Die] = new PigDie(this);

        machine.SetState(states[(int)State.Idle], this);
    }

    public void ChangeState(State newState)
    {
        int index = (int)newState;
        for (int i = 0; i < (int)State.End; ++i)
        {
            if (index == i)
                machine.ChangeState(states[index]);
        }
    }

    private void Start()
    {
        Begine();
    }

    private void Update()
    {
        Run();
    }

    public void Begine()
    {
        machine.Begine();
    }

    public void Run()
    {
        machine.Run();
    }

    public void Exit()
    {
        machine.Exit();
    }
}


/// <summary>
/// 몬스터의 상태들
/// </summary>
public class PigIdle : FSM<PigFSM>
{
    private PigFSM owner;

    public PigIdle(PigFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Idle;
    }

    public override void Run()
    {
    }

    public override void Exit()
    {
        owner.prevState = State.Idle;
    }
}

public class PigMove : FSM<PigFSM>
{
    private PigFSM owner;

    public PigMove(PigFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Move;
    }

    public override void Run()
    {
    }

    public override void Exit()
    {
        owner.prevState = State.Move;
    }
}

public class PigAttack : FSM<PigFSM>
{
    private PigFSM owner;

    public PigAttack(PigFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Attack;
    }

    public override void Run()
    {
    }

    public override void Exit()
    {
        owner.prevState = State.Attack;
    }
}

public class PigHit : FSM<PigFSM>
{
    private PigFSM owner;

    public PigHit(PigFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Hit;
    }

    public override void Run()
    {
    }

    public override void Exit()
    {
        owner.prevState = State.Hit;
    }
}

public class PigDie : FSM<PigFSM>
{
    private PigFSM owner;

    public PigDie(PigFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Die;
    }

    public override void Run()
    {
    }

    public override void Exit()
    {
        owner.prevState = State.Die;
    }
}
