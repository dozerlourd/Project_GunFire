using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    protected EnemyHP enemyHp;
    [SerializeField] GameObject destEffect;

    // 열거형 상수
    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        AttackToMove,
        AttackDamaged,
        Die
    }

    // 회전속도
    public float rotSpeed = 20f;

    public bool IsChained
    {
        get => isChained; set
        {
            isChained = value;
        }
    }
    bool isChained = false;

    public IEnumerator ChainDeactivate(float _time)
    {
        yield return new WaitForSeconds(_time);
        agent.enabled = true;
        isChained = false;
    }

    public EnemyState eState;

    public float sightRange = 5.0f;
    public float attackRange = 2.0f;
    public int attackPower = 30;
    public float delayTime = 1.0f;

    protected Transform player;
    protected float currentTime = 0;
    protected Animator enemyAnim;
    protected NavMeshAgent agent;

    protected bool isDamaged = false;
    protected bool isBooked = false;

    protected Coroutine co;

    public Transform GetTargetTransform() => player;


    protected void Start()
    {
        // 시작하면 maxHp로 체력 초기healthPoint;
        //healthPoint = maxHp;

        // 최초의 상태는 대기 상태이다.
        eState = EnemyState.Idle;
        //eState = 0;

        // 플레이어를 찾는다.
        player = GameObject.Find("Player").transform;

        // 자식 오브젝트로부터 Animator 컴포넌트를 가져온다.
        enemyAnim = GetComponentInChildren<Animator>();

        // NavMeshAgent 컴포넌트를 가져온다
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange;

        enemyHp = GetComponent<EnemyHP>();
    }

    protected void Update()
    {
        switch (eState)
        {
            case EnemyState.AttackDamaged:
                AttackDamaged();
                break;

            case EnemyState.Die:
                Die();
                break;
        }

        if (IsChained)
        {
            enemyAnim.SetTrigger("Idle");
            agent.enabled = false;
            return;
        }

        // 각 상태별 처리
        switch (eState)
        {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Move:
                Move();
                break;

            case EnemyState.Attack:
                Attack();
                break;
        }
        if (isDamaged && (eState == EnemyState.Idle || eState == EnemyState.Move))
        {
            agent.SetDestination(player.position);
        }

        if (isDamaged)
        {
            if (eState == EnemyState.Idle || eState == EnemyState.Move)
            {
                enemyAnim.SetTrigger("IdleToMove");

                if (agent.enabled == false) return;

                agent.SetDestination(player.position);
            }
        }
    }

    protected void Idle()
    {
        // 만일, 시야 범위에 플레이어가 있으면 이동 상태로 전환한다.
        // 필요 요소: 시야 범위, 플레이어와 나와의 거리, 플레이어
        float distance = (player.position - transform.position).magnitude;

        if (sightRange >= distance)
        {
            // 이동 상태로 전환한다.
            co = StartCoroutine(SetMoveState());
        }
    }




    protected void Move()
    {
        agent.enabled = true;

        //플레이어의 위치를 NavMesh의 목적지로 설정한다
        agent.SetDestination(player.position);
        //다시 move로 전환할때
        agent.isStopped = false;

        float dist = Vector3.Distance(player.position, transform.position);
        if (dist <= attackRange)
        {
            eState = EnemyState.Attack;
            enemyAnim.SetTrigger("MoveToAttack");
            agent.enabled = false;
        }
        //else if(dist < sightRange)
        //{
        //    enemyAnim.SetTrigger("Idle");
        //    eState = EnemyState.Idle;
        //}

    }

    protected void AttackDamaged()
    {
        if (enemyHp.CurrentHP <= 0)
        {
            enemyAnim.SetTrigger("Die");
            eState = EnemyState.Die;
        }
        else
        {
            enemyAnim.SetTrigger("Hit");
            eState = EnemyState.Idle;
        }
    }

    protected void Attack()
    {
        // 만일, 플레이어가 공격 범위 이내라면...
        if (Vector3.Distance(player.position, transform.position) < attackRange)
        {
            if (co != null) StopCoroutine(co);
            // 매 1초마다 타겟의 체력을 나의 공격력만큼 감소시킨다.
            if (currentTime > delayTime)
            {
                currentTime = 0;
                enemyAnim.SetTrigger("DelayToAttack");
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
        // 공격 범위 밖이라면...
        else
        {
            if(!isBooked)
            {
                // 1.5초 뒤에 이동 상태로 전환한다.
                co = StartCoroutine(SetMoveState());
                isBooked = true;
            }
        }
    }

    protected IEnumerator SetMoveState()
    {
        //print("d");
        yield return new WaitForSeconds(1.0f);

        // 이동 상태로 전환한다.
        eState = EnemyState.Move;

        // 이동 애니메이션을 실행한다.
        enemyAnim.SetTrigger("IdleToMove");
        isBooked = false;
    }


    protected void Die()
    {

        //오브젝트 자기 자신을 제거한다
        Destroy(gameObject);
        Instantiate(destEffect, transform.position, Quaternion.identity);
    }


    public void EnemyTakeDamage(float _dmg, Vector3 _vec)
    {
        enemyHp.TakeDamage = _dmg;
        isDamaged = true;
        eState = EnemyState.AttackDamaged;
        //벡터값으로 넉백
    }

}












