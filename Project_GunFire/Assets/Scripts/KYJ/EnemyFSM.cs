using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    EnemyHP enemyHp;

    //열거형 상수
    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        AttackDamaged,
        Die

    }

    public EnemyState eState;

    public Transform player;

    // 시야범위
    public float sightRange = 5f;

    // 회전속도
    public float rotSpeed = 20f;

    // 공격범위
    public float attackRange = 1.5f;

    bool isDamaged = false;

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

    // 내비게이션 에이전트 변수
    private NavMeshAgent agent;

    void Start()
    {
        // enemy 대기상태로 시작
        eState = EnemyState.Idle;

        player = GameObject.Find("Player").transform;

        // 내비게이션 에이젼트 컴포넌트 받아오기
        agent = GetComponent<NavMeshAgent>();

        enemyHp = GetComponent<EnemyHP>();
    }

    void Update()
    {
        switch(eState)
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
        if(isDamaged && (eState == EnemyState.Idle || eState == EnemyState.Move))
        {
            agent.SetDestination(player.position);
        }
    }

    private void AttackDamaged()
    {
        if(enemyHp.CurrentHP <= 0)
        {
            eState = EnemyState.Die;
        }
        else
        {
            eState = EnemyState.Move;
        }
    }

    private void Idle()
    {
        // enemy(나)와 플레이어(타겟) 사이 거리
        float distance = (player.position - transform.position).magnitude;

        // 만일 타겟이 시야범위안에 있다면..
        if( distance <= sightRange)
        {
            // move상태로 전환한다
            eState = EnemyState.Move;
        }
    }

    private void Move()
    {
        
        // 플레이어를 향해 이동
        Vector3 dir = player.position - transform.position;
        float distance = dir.magnitude;
        dir.Normalize();

        agent.SetDestination(player.position);

        // 타켓 회전 위치
        //Quaternion targetRot = Quaternion.LookRotation(dir);
        // 이동방향을 바라보도록 회전
        //cc.transform.rotation = Quaternion.Slerp(cc.transform.rotation, targetRot, rotSpeed * Time.deltaTime);
        //cc.Move(dir * moveSpeed * Time.deltaTime);

        
        //만일, 플레이어가 어텍범위 안에 들어오면
        if( attackRange >= distance)
        {
            // attack상태로 전환한다
            eState = EnemyState.Attack;
        }
        
    }

    float currentTime = 0;
    float attackTime;

    private void Attack()
    {
        currentTime += Time.deltaTime;

        // 일정시간(2초) 간격을 두고 공격하고 싶다
        // 만일 현재시간이 공격시간이 되면
        if( currentTime >= attackTime)
        {
            // 플레이어를 공격한다

        }

        float distance = (player.position - transform.position).magnitude;
        // 만일, 플레이어가 어텍범위를 벗어나면
        if( distance >= attackRange)
        {
            // move상태로 전환한다
            eState = EnemyState.Move;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void EnemyTakeDamage(float _dmg, Vector3 _vec)
    {
        enemyHp.TakeDamage = _dmg;
        isDamaged = true;
        eState = EnemyState.AttackDamaged;
        //벡터값으로 넉백
    }
}
