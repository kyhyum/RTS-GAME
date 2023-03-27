using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AIUnit : MonoBehaviour
{
    public bool is_range_long;
    public GameObject longRangeWeapon;
    private Animator animator;
    public Transform target;
    public Unit unit;

    public int attackRange;
    public int seekRange;
    public bool isDead = false;
    public bool isCreep;

    public IState[] _IStates;

    private State _state;

    public enum State
    {
        Idle,
        Walk,
        Attack,
        Dead,
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
        animator.GetComponent<Animator>();
        _IStates = new IState[System.Enum.GetValues(typeof(State)).Length];
        _IStates[(int)State.Idle] = new IdleState(this);
        _IStates[(int)State.Walk] = new WalkState(this);
        _IStates[(int)State.Attack] = new AttackState(this);
        _IStates[(int)State.Dead] = new DeadState(this);
        _IStates[(int)State.Victory] = new VictoryState(this);
        _IStates[(int)State.GetHit] = new GetHitState(this);
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        _IStates[(int)_state].Stay();
    }

    public void PlayAnimation(State state)
    {
        switch(state)
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
            case State.Dead:
                animator.Play("Dead");
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
