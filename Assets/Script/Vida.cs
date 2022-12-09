using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Vida : MonoBehaviour
{
    public PlayerController _object;

    private TextMeshProUGUI _text;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        _text = GetComponent<TextMeshProUGUI>();
        _object = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        _text.text = _object.HP.ToString();
    }
}
