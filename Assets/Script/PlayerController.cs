using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_InputSystem.Base;

public class PlayerController : MonoBehaviour
{
    public bool CconfiguracaoPC = true;
    private CharacterController _controller;
    private Animator _animator;
    public GameManager _gameManager;
    public SumPause _Pause;
    public LootLocker_Sistema _object;

    [Header("Player")]
    public float velocidadeMovimento = 3f;
    public float HP = 10;
    public float TotalHP = 10;

    private Vector3 direcao;
    private bool Andar;

    private float horizontal;
    private float vertical;

    public ParticleSystem fxAttack;
    private bool isAttack; 
    private bool isDie;

    public Transform hitBox;
    [Range(0.2f, 1f)]
    public float hitRange = 0.5f;

    public LayerMask hitMask;
    public Collider[] hitInfo;

    public int dano;
    public float crono = 0;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        _Pause = GameObject.FindWithTag("Pause").GetComponent<SumPause>();
        _object = GameObject.FindWithTag("GuardarDados").GetComponent<LootLocker_Sistema>();
        TotalHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        MovePlayer();
        UpdateAnimetor();
        XpLevel();
        recover();
    }
    void recover()
    {
        crono += 1 * Time.deltaTime;
        if(crono >= 15 && HP < TotalHP)
        {
            HP = HP + 1;
        }
    }
    IEnumerator Recover()
    {
        yield return new WaitForSeconds(1f);
        HP = HP + 1;

    }
    void XpLevel()
    {
        if(_gameManager.xp >= 100)
        {
            _gameManager.level = _gameManager.level + 1;
            _gameManager.xp = 0;
            HP = HP + 5;
            TotalHP = TotalHP + 5;
            dano = dano + 1;
        }
    }
    void Inputs()
    {
        if (CconfiguracaoPC)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            horizontal = UIInputSystem.ME.GetAxisHorizontal(JoyStickAction.Movement);
            vertical = UIInputSystem.ME.GetAxisVertical(JoyStickAction.Movement);
        }
        
        direcao = new Vector3(horizontal, 0f, vertical).normalized;
    }

    public void Atacar()
    { 
            if (!isAttack)
            {
                isAttack = true;
                _animator.SetTrigger("Atacar");
                fxAttack.Emit(1);

                hitInfo = Physics.OverlapSphere(hitBox.position, hitRange, hitMask);

                foreach (var c in hitInfo)
                {
                    c.gameObject.SendMessage("GetHit", dano, SendMessageOptions.DontRequireReceiver);
                }
            }
    }

    void AttackIsDone()
    {
        isAttack = false;
    }
    void MovePlayer()
    {
        _controller.Move(direcao * velocidadeMovimento * Time.deltaTime);
        if(direcao.magnitude > 0.1f)
        {
            float angulo = Mathf.Atan2(direcao.x, direcao.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angulo, 0);
            Andar = true;
        }
        else
        {
            Andar = false;
        }
    }

    void UpdateAnimetor()
    {
        _animator.SetBool("Andar", Andar);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hitBox.position, hitRange);
    }
    void GetHit(float dano)
    {
        HP = HP - dano;
        crono = 0;
        if (HP > 0)
        {
            _animator.SetTrigger("Hit");
        }
        else
        {
            _animator.SetTrigger("Die");
            StartCoroutine(Died());
        }

    }
    IEnumerator Died()
    {
        yield return new WaitForSeconds(1f);
        _Pause.TogglePause();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SlimeArmoDamage")
        {
            GetHit(_gameManager.SlimeArmorAttackDamage / 2);
        }
        if (other.gameObject.tag == "SlimeDamage")
        {
            GetHit(_gameManager.SlimeAttackDamage/2);
        }
       
        if (other.gameObject.tag == "BossDamage")
        {
            GetHit(_gameManager.BossAttackDamage/2);
        }
    }
}
