using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LootLocker.Requests;

public class LootLocker_Sistema : MonoBehaviour
{
    public TMP_InputField Nome, Placar;
    public int ID;
    int max = 5;
    public TextMeshProUGUI[] Jogadores;
    private GameManager _gameManager;
    private void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
                Debug.Log("Conectado");
            else
                Debug.Log("Erro na conex�o");
        });
    }

    public void EnviarPlacar()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

        LootLockerSDKManager.SubmitScore(Nome.text, (int)_gameManager.crono, ID, (response) =>
        {
            if (response.success)
                Debug.Log("Enviado");
            else
                Debug.Log("Erro no envio");
        });
    }

    public void MostrarPlacar()
    {
        Debug.Log("Erro na placar");
        LootLockerSDKManager.GetScoreList(ID, max, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] placares = response.items;
                for (int i = 0; i < placares.Length; i++)
                    Jogadores[i].text = placares[i].rank + " " + placares[i].member_id + " - " + placares[i].score;

                if (placares.Length < max)
                {
                    for (int i = placares.Length; i < max; i++)
                        Jogadores[i].text = (i + 1).ToString() + " 0";
                }
            }
            else
                Debug.Log("Erro na placar");
        });
    }
}

