using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum enemyState{
    IDLE, PATROL, FURY, EXPLORE, ALERT, FOLLOW
}
public class GameManager : MonoBehaviour
{
    [Header("Slime IA")] public Transform[] slimePoints;
    [Header("Slime Armor IA")] public Transform[] slimeArmorPoints;
    public GameObject slimeSpawn;
    public GameObject slimeArmorSpawn;
    public TextMeshProUGUI _text;

    public float idleWaitTime = 3f;
    public Transform playerPosition;

    public float slimeDistanceAttack;
    public float slimeArmorDistanceAttack;
    public float BossDistanceAttack; 
    public float AttackDelay = 1f;
    public float LookRotarionSpeed = 1f;

    public float SlimeAttackDamage = 1f;
    public float SlimeArmorAttackDamage = 3f;
    public float BossAttackDamage = 5f;

    public int xp = 0;
    public int level = 1;
    public float crono = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        RespawnMob();
        _text.text = crono.ToString("0.00");
        crono += 1 * Time.deltaTime;
    }
    void RespawnMob()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Slime");
        if (gameObjects.Length < 3)
        {
            var range = Random.Range(0, 5);
            Instantiate(slimeSpawn, slimePoints[range].position, slimePoints[range].rotation);
        }
        GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("SlimeArmor");
        if (gameObjects2.Length < 3)
        {
            var range = Random.Range(0, 5);
            Instantiate(slimeArmorSpawn, slimeArmorPoints[range].position, slimeArmorPoints[range].rotation);
        }
    }
}
