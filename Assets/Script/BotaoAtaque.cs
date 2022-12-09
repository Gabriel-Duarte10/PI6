using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotaoAtaque : MonoBehaviour
{

    void Start()
    {
        var platerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(delegate () { platerController.Atacar(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
