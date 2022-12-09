using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XP : MonoBehaviour
{
    private TextMeshProUGUI _text;
    public GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        _text = GetComponent<TextMeshProUGUI>();
    }
        void Update()
    {
        _text.text = _gameManager.xp.ToString();
    }
}
