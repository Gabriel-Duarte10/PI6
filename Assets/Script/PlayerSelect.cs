using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class PlayerSelect : MonoBehaviour
{
    GameObject player;
    int i;
    public GameObject[] players;
    public UnityEngine.UI.Button next;
    public UnityEngine.UI.Button previous;
    public UnityEngine.UI.Button select;
    public GameObject guardarDados;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        next.onClick = new Button.ButtonClickedEvent();
        previous.onClick = new Button.ButtonClickedEvent();
        select.onClick = new Button.ButtonClickedEvent();
        
        next.onClick.AddListener(() => Next());
        previous.onClick.AddListener(() => Previous());
        select.onClick.AddListener(() => Select());
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            player = GameObject.Find("Player");
            player = Instantiate(players[i], player.transform.position, player.transform.rotation);
        }
    }

    void Next()
    {
        i++;
        if (i >= players.Length)
        {
            i = 0;
        }
        Destroy(player);
    }
    void Previous()
    {
        i--;
        if (i < 0)
        {
            i = players.Length -1;
        }
        Destroy(player);
    }
    void Select()
    {
        SceneManager.LoadScene("Fase1", LoadSceneMode.Single);
        DontDestroyOnLoad(player); 
        DontDestroyOnLoad(guardarDados);

        player.name = "Player";
    }
}
