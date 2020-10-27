using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGiverFSM : MonoBehaviour, IFSM
{
    public Machine<SkillGiverFSM> machine;
    public FSM<SkillGiverFSM>[] states = new FSM<SkillGiverFSM>[(int)State.End];

    public State currState;
    public State prevState;

    public Animator animator;
    public SkillGiver giver;
    public Player target;
    public Transform targetTransform;
    public float startPosX;

    public float ineractionRange;
    public float moveSpeed;
    public bool isGiveSkill;

    public SkillGiverFSM()
    {
        Init();
    }

    public void Init()
    {
        machine = new Machine<SkillGiverFSM>();

        states[(int)State.Idle] = new SkillGiverIdle(this);
        states[(int)State.Ready] = new SkillGiverReady(this);
        states[(int)State.Move] = new SkillGiverMove(this);
        states[(int)State.Give] = new SkillGiverGiveSkill(this);

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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        giver = GetComponent<SkillGiver>();
        ineractionRange = 2.0f;
        moveSpeed = 3.0f;
        isGiveSkill = true;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        startPosX = gameObject.transform.position.x;

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
/// SkillGiver의 상태
/// </summary>
public class SkillGiverIdle : FSM<SkillGiverFSM>
{
    private SkillGiverFSM owner;

    public SkillGiverIdle(SkillGiverFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Idle;
    }

    public override void Run()
    {
        if(!owner.isGiveSkill)
        {
            if (FindTarget())
            {
                owner.ChangeState(State.Ready);
            }
        }

    }

    public override void Exit()
    {
        owner.prevState = State.Idle;
    }

    public bool FindTarget()
    {
        if (owner.target != null)
        {
            owner.targetTransform = owner.target.transform;

            Vector3 dir = owner.targetTransform.position.x > owner.target.transform.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
            owner.giver.transform.localScale = dir;

            return true;
        }
        return false;
    }
}

public class SkillGiverReady : FSM<SkillGiverFSM>
{
    private SkillGiverFSM owner;

    public SkillGiverReady(SkillGiverFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Ready;
    }

    public override void Run()
    {
        Vector3 giverPos = owner.transform.position;
        Vector3 targetPos = owner.targetTransform.position;
        if (Vector2.Distance(giverPos, targetPos) > owner.ineractionRange)
        {
            owner.ChangeState(State.Move);
        }
        else
        {
            owner.ChangeState(State.Give);
        }
    }

    public override void Exit()
    {
        owner.prevState = State.Ready;
    }
}

public class SkillGiverMove : FSM<SkillGiverFSM>
{
    private SkillGiverFSM owner;

    public SkillGiverMove(SkillGiverFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Move;
    }

    public override void Run()
    {
        if(!owner.isGiveSkill)
        {
            Vector3 giverPos = owner.transform.position;
            Vector3 targetPos = owner.targetTransform.position;
            if (Vector2.Distance(giverPos, targetPos) > owner.ineractionRange)
            {
                owner.transform.position = Vector2.MoveTowards(giverPos, targetPos, owner.moveSpeed * Time.deltaTime);
            }
            else
            {
                owner.ChangeState(State.Ready);
            }
        }
        else
        {
            Vector3 giverPos = owner.transform.position;
            owner.transform.position = Vector2.MoveTowards(giverPos, new Vector2(owner.startPosX, owner.transform.position.y), owner.moveSpeed * Time.deltaTime);
            if(giverPos.x == owner.startPosX)
            {
                owner.ChangeState(State.Idle);
            }
        }

    }

    public override void Exit()
    {
        owner.prevState = State.Move;
    }
}

public class SkillGiverGiveSkill : FSM<SkillGiverFSM>
{
    private SkillGiverFSM owner;

    public SkillGiverGiveSkill(SkillGiverFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Give;
    }

    public override void Run()
    {
        if(!owner.isGiveSkill)
        {
            owner.giver.GiveSkill();
            owner.ChangeState(State.Move);
        }
    }

    public override void Exit()
    {
        owner.prevState = State.Give;
    }
}