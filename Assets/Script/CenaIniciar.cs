using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenaIniciar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerController>()._gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
