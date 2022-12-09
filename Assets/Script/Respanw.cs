using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respanw : MonoBehaviour
{
    private GameManager _gameManager;
    public GameObject slimeSpawn;
    public GameObject slimeArmorSpawn;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    // Update is called once per frame
    void Update()
    {
        //RespawnMob();
    }
    void RespawnMob()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Slime");
        if (gameObjects.Length < 3)
        {
            var range = Random.Range(0, 5);
            Instantiate(slimeSpawn, _gameManager.slimePoints[range].position, _gameManager.slimePoints[range].rotation);
        }
        GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("SlimeArmor");
        if (gameObjects2.Length < 3)
        {
            var range = Random.Range(0, 5);
            Instantiate(slimeArmorSpawn, _gameManager.slimeArmorPoints[range].position, _gameManager.slimeArmorPoints[range].rotation);
        }
    }
}
