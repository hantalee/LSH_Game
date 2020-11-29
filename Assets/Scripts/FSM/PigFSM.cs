using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigFSM : MonoBehaviour, IFSM
{
    public Machine<PigFSM> machine;
    public FSM<PigFSM>[] states = new FSM<PigFSM>[(int)State.End];

    public State currState;
    public State prevState;

    public Animator animator;
    public BaseMonster Monster { get; set; }
    public Player target;
    public Transform targetTransform;

    public float attackRange;

    public PigFSM()
    {
        Init();
    }

    public void Init()
    {
        machine = new Machine<PigFSM>();

        states[(int)State.Idle] = new PigIdle(this);
        states[(int)State.Ready] = new PigReady(this);
        states[(int)State.Move] = new PigMove(this);
        states[(int)State.Attack] = new PigAttack(this);
        states[(int)State.Hit] = new PigHit(this);
        states[(int)State.Die] = new PigDie(this);
        states[(int)State.Return] = new PigReturn(this);

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
        Monster = GetComponent<BaseMonster>();
        attackRange = 1.0f;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

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

    public float GetAnimPlayingTime(string name)
    {
        RuntimeAnimatorController animController = animator.runtimeAnimatorController;
        float animTime = 0.0f;

        for (int i = 0; i < animController.animationClips.Length; ++i)
        {
            if (animController.animationClips[i].name == name)
            {
                animTime = animController.animationClips[i].length;
                break;
            }
        }
        return animTime;
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
        if(FindTarget())
        {
            owner.ChangeState(State.Ready);
        }
    }

    public override void Exit()
    {
        owner.prevState = State.Idle;
    }

    public bool FindTarget()
    {
        if(owner.target != null)
        {
            owner.targetTransform = owner.target.transform;

            Vector3 dir = owner.targetTransform.position.x > owner.target.transform.position.x ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
            owner.Monster.transform.localScale = dir;

            return true;
        }
        return false;
    }
}

public class PigReady : FSM<PigFSM>
{
    private PigFSM owner;

    public PigReady(PigFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Ready;

        owner.animator.SetBool("IsReady", true);
    }

    public override void Run()
    {
        Vector3 monsterPos = owner.transform.position;
        Vector3 targetPos = owner.targetTransform.position;
        if (Vector2.Distance(monsterPos, targetPos) > owner.attackRange)
        {
            owner.ChangeState(State.Move);
        }
        else
        {
            owner.ChangeState(State.Attack);
        }
    }

    public override void Exit()
    {
        owner.prevState = State.Ready;
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

        owner.animator.SetBool("IsWalk", true);
    }

    public override void Run()
    {
        Vector3 monsterPos = owner.transform.position;
        Vector3 targetPos = owner.targetTransform.position;
        if (Vector2.Distance(monsterPos, targetPos) > owner.attackRange)
        {
            int moveSpeed = owner.Monster.Stat.GetStat(BaseStat.BaseStatType.MoveSpeed).GetFinalValue();
            owner.transform.position = Vector2.MoveTowards(monsterPos, targetPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            owner.ChangeState(State.Ready);
        }
    }

    public override void Exit()
    {
        owner.prevState = State.Move;

        owner.animator.SetBool("IsWalk", false);
    }
}

public class PigAttack : FSM<PigFSM>
{
    private PigFSM owner;

    private float attackTime;
    private float currTime = 0.0f;
    private string animName = "Pig_Attack";
    private float animPlayingTime = 0.0f;
    private IEnumerator IChangeState = null;

    public PigAttack(PigFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Attack;

        attackTime = (float)owner.Monster.Stat.GetStat(BaseStat.BaseStatType.AttackSpeed).GetFinalValue();
        currTime = attackTime;

        animPlayingTime = owner.GetAnimPlayingTime(animName);
        IChangeState = ChangeStateToReady(animPlayingTime);
    }

    public override void Run()
    {
        currTime -= Time.deltaTime;
        if (currTime <= 0.0f)
        {
            currTime = attackTime;
            owner.animator.SetTrigger("Attack");
            owner.Monster.PerformAttack();

            owner.StartCoroutine(IChangeState);
        }
    }

    public override void Exit()
    {
        owner.prevState = State.Attack;
    }

    public IEnumerator ChangeStateToReady(float time)
    {
        yield return new WaitForSeconds(time);

        if (!owner.Monster.isDead)
            owner.ChangeState(State.Ready);
    }
}

public class PigHit : FSM<PigFSM>
{
    private PigFSM owner;

    private string animName = "Pig_Hit";
    private float animPlayingTime;
    private IEnumerator IChangeState = null;

    public PigHit(PigFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Hit;

        animPlayingTime = owner.GetAnimPlayingTime(animName);
        IChangeState = ChangeStateToReady(animPlayingTime);

        owner.animator.SetTrigger("Hit");
        ExecuteCoroutine();
    }

    public override void Run()
    {
    }

    public override void Exit()
    {
        owner.prevState = State.Hit;
    }

    public void ExecuteCoroutine()
    {
        if(IChangeState != null)
        {
            owner.StopCoroutine(IChangeState);
            IChangeState = null;
        }
        IChangeState = ChangeStateToReady(animPlayingTime);
        owner.StartCoroutine(IChangeState);
    }

    public IEnumerator ChangeStateToReady(float time)
    {
        yield return new WaitForSeconds(time);

        if (!owner.Monster.isDead)
            owner.ChangeState(State.Ready);
    }
}

public class PigDie : FSM<PigFSM>
{
    private PigFSM owner;

    private string animName = "Pig_Dead";
    private float animPlayingTime;
    private IEnumerator IChangeState = null;
    private float currTime;

    public PigDie(PigFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Die;

        animPlayingTime = owner.GetAnimPlayingTime(animName);
        currTime = animPlayingTime;

        owner.animator.SetTrigger("Die");
        ExecuteCoroutine();
    }

    public override void Run()
    {
    }

    public override void Exit()
    {
        owner.prevState = State.Die;
    }

    public void ExecuteCoroutine()
    {
        if (IChangeState != null)
        {
            owner.StopCoroutine(IChangeState);
            IChangeState = null;
        }
        IChangeState = ReturnMonster(animPlayingTime);
        owner.StartCoroutine(IChangeState);
    }

    public IEnumerator ReturnMonster(float time)
    {
        yield return new WaitForSeconds(time);

        CharacterManager.Instance.ReturnMonster(owner.Monster);
    }
}

public class PigReturn : FSM<PigFSM>
{
    private PigFSM owner;

    private IEnumerator IChangeState = null;

    public PigReturn(PigFSM owner)
    {
        this.owner = owner;
    }

    public override void Begine()
    {
        owner.currState = State.Return;

        ExecuteCoroutine();
    }

    public override void Run()
    {
    }

    public override void Exit()
    {
        owner.prevState = State.Return;
    }

    public void ExecuteCoroutine()
    {
        if (IChangeState != null)
        {
            owner.StopCoroutine(IChangeState);
            IChangeState = null;
        }
        IChangeState = ReturnMonster();
        owner.StartCoroutine(IChangeState);
    }

    public IEnumerator ReturnMonster()
    {
        yield return new WaitForSeconds(0.01f);

        CharacterManager.Instance.ReturnMonster(owner.Monster);
    }
}
