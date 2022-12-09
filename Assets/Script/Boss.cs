using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    private GameManager _gameManager;
    public SumPause _Pause;
    private Animator _animator;
    public LootLocker_Sistema _object;
    private bool isHit;
    public int HP;
    private bool isDie = false;
    private enemyState state = enemyState.IDLE;
    private NavMeshAgent agent;
    private Vector3 destino;
    private bool isWalk = false;
    private bool isAttack = false;

    void Start()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        _Pause = GameObject.FindWithTag("Pause").GetComponent<SumPause>();
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        _object = GameObject.FindWithTag("GuardarDados").GetComponent<LootLocker_Sistema>();

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
            case enemyState.IDLE: 
                break;
            case enemyState.ALERT:
                break;
            case enemyState.PATROL:
                break;
            case enemyState.EXPLORE:
                break;
            case enemyState.FURY:
                LookAt();
                destino = _gameManager.playerPosition.position;
                agent.stoppingDistance = _gameManager.BossDistanceAttack;
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
                break;
            case enemyState.FURY: 
                
                break;
        }
    }

    IEnumerator Died()
    {
        isDie = true;
        yield return new WaitForSeconds(1f);
        _object.EnviarPlacar();
        _Pause.TogglePause();

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
            StartCoroutine(Died());
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
        if (isAttack == false )
        {
            isAttack = true;
            if (Random.Range(0, 100) > 70)
            {
                _animator.SetTrigger("Attack");
            }
            else
            {
                _animator.SetTrigger("AttackSpell");
            }
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