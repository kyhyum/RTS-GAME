using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AIUnit : MonoBehaviour
{
    public int character_num;

    //우리편 상대편 확인
    public bool is_Enemy;
    public string Opposite_team;

    //원거리 공격 확인
    public bool is_range_long;
    public GameObject longRangeWeapon;
    public Transform Weapon;

    //공중 공격 가능 확인
    public bool AIr_Unit_Attack;

    //공격 범위
    public float attackRange;
    //공격 애니메이션 속도
    public float attack_anim_speed;
    //방어력
    public float armor;
    //공격력
    public float attack;

    //공중 유닛인지 확인
    public bool isInAir;

    public Hp_Bar hp_bar;

    public Tower EnemyTower;
    public Tower MyTower;

    private Animator animator;
    public Transform DefaultTarget;
    public Transform target;
    public Unit unit;

    public bool isDead = false;
    public bool isEnable = false;
    public bool isVictory = false;
    public bool isLose = false;

    public IState[] _IStates;

    private State _state;

    public void Settarget(Transform tower)
    {
        DefaultTarget = tower;
        target = tower;
        unit.target = tower;
        EnemyTower = DefaultTarget.GetComponent<Tower>();
    }

    public enum State
    {
        Idle,
        Walk,
        Attack,
        Air_Attack,
        Death,
        Victory,
        GetHit
    }

    public State States
    {
        get { return _state; }
        set
        {
            _IStates[(int)_state].Exit();
            _state = value;
            _IStates[(int)_state].Enter();
        }
    }

    void Awake()
    {
        hp_bar = GetComponent<Hp_Bar>();
        if (is_Enemy == false)
        {
            Opposite_team = "Enemy";
        }
        else
        {
            Opposite_team = "Player";
        }
        unit = GetComponent<Unit>();
        animator = GetComponent<Animator>();
        attack_anim_speed = animator.speed;
        _IStates = new IState[System.Enum.GetValues(typeof(State)).Length];
        _IStates[(int)State.Idle] = new IdleState(this);
        _IStates[(int)State.Walk] = new WalkState(this);
        _IStates[(int)State.Attack] = new AttackState(this);
        _IStates[(int)State.Death] = new DeadState(this);
        _IStates[(int)State.Victory] = new VictoryState(this);
        _IStates[(int)State.GetHit] = new GetHitState(this);
    }

    private void Update()
    {
        isDead = hp_bar.isdead;
        if ((isDead && isEnable) || MyTower.IsDestroy)
        {
            this.States = AIUnit.State.Death;
        }
        if (EnemyTower.IsDestroy)
        {
            this.States = AIUnit.State.Victory;
        }
        _IStates[(int)_state].Stay();
    }

    public void PlayAnimation(State state)
    {
        switch (state)
        {
            case State.Idle:
                animator.Play("Idle");
                break;
            case State.Victory:
                animator.Play("Victory");
                break;
            case State.GetHit:
                animator.Play("GetHit");
                break;
            case State.Attack:
                animator.Play("Attack");
                break;
            case State.Air_Attack:
                animator.Play("Air_Attack");
                break;
            case State.Death:
                animator.Play("Death");
                break;
            case State.Walk:
                animator.Play("Walk");
                break;
        }
    }
}

public interface IState
{
    //상태 진입
    void Enter();

    //상태 유지
    void Stay();

    //상태 탈출
    void Exit();

}