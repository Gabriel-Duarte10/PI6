using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeBasic : MonoBehaviour
{
    private GameManager _gameManager;
    private Animator _animator;
    private bool isHit;
    public int HP;
    private bool isDie = false;
    public enemyState state = enemyState.IDLE;
    private NavMeshAgent agent;
    private Vector3 destino;
    private bool isWalk = false;
    public bool isAttack = false;

    void Start()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        changeState(state);
    }
    void Update()
    {
        StateManager();
        AnimationMoviment();
    }

    void StateManager()
    {
        switch (state)
        {
            case enemyState.FURY:
                LookAt();
                destino = _gameManager.playerPosition.position;
                agent.stoppingDistance = _gameManager.slimeDistanceAttack;
                agent.destination = destino;
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    Attack();
                }
                break;
        }
        
    }

    void changeState(enemyState newState)
    {
        StopAllCoroutines();
        state = newState;
        switch (state)
        {
            case enemyState.IDLE:
                destino = transform.position;
                agent.stoppingDistance = 0;
                agent.destination = destino;
                StartCoroutine("IDLE");
                break;
            case enemyState.PATROL:
                destino = _gameManager.slimePoints[Random.Range(0, _gameManager.slimePoints.Length)].position;
                agent.stoppingDistance = 0;
                agent.destination = destino;

                StartCoroutine("PATROL");
                break;
        }
    }

    IEnumerator IDLE()
    {
        yield return new WaitForSeconds(_gameManager.idleWaitTime);
        if (Rand(20))
        {
            changeState(enemyState.IDLE);
        }
        else
        {
            changeState(enemyState.PATROL);
        }
    }
    IEnumerator PATROL()
    {
        yield return new WaitUntil(() => agent.remainingDistance <= 0);
        if (Rand(60))
        {
            changeState(enemyState.IDLE);
        }
        else
        {
            changeState(enemyState.PATROL);
        }
    }
    IEnumerator Died()
    {
        isDie = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    void GetHit(int dano)
    {
        if(isDie == true){return;}
        HP = HP - dano;
        if (HP > 0)
        {
            changeState(enemyState.FURY);
            _animator.SetTrigger("GetHit");
        }
        else
        {
            _animator.SetTrigger("Die");
            agent.destination = this.transform.position;
            isDie = true;
            Destroy(gameObject);
            _gameManager.xp = _gameManager.xp + 10;
        }
           
    }

    bool Rand(int percent)
    {
        int rand = Random.Range(0, 100);
        if (rand <= percent)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    void AnimationMoviment()
    {
        if (agent.desiredVelocity.magnitude >= 0.1f)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }
        _animator.SetBool("isWalk", isWalk);
    }

    void Attack()
    {
        if (isAttack == false)
        {
            isAttack = true;
            _animator.SetTrigger("Attack");
        }
    }
    void AttackIsDone()
    {
        StartCoroutine("ATTACK");
    }

    IEnumerator ATTACK()
    {
        yield return new WaitForSeconds(_gameManager.AttackDelay);
        isAttack = false;
    }

    void LookAt()
    {
        Vector3 lookDirection = (_gameManager.playerPosition.position - transform.position).normalized;
        Quaternion lookQuaternion = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookQuaternion,
            _gameManager.LookRotarionSpeed * Time.deltaTime);
    }
}
