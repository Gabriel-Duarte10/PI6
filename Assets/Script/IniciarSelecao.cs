using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;
public class IniciarSelecao : MonoBehaviour
{
    public UnityEngine.UI.Button Iniciar;
    public GameObject imageBackGroundInicio;
    public GameObject imageBackGroundSelecao;
    public GameObject botaoInicio;
    public GameObject botaoSelect;
    public GameObject botaoNext;
    public GameObject botaoPreview;
    public GameObject Player;
    public GameObject titulo;
    public GameObject name;
    public GameObject Placar;
    private LootLocker_Sistema _LootLocker_Sistema;

    // Start is called before the first frame update
    void Start()
    {
        _LootLocker_Sistema = FindObjectOfType(typeof(LootLocker_Sistema)) as LootLocker_Sistema;
        Iniciar.onClick = new Button.ButtonClickedEvent();

        Iniciar.onClick.AddListener(() => Select());
    }
    void Select()
    {
        imageBackGroundInicio.SetActive(false);
        botaoInicio.SetActive(false);
        titulo.SetActive(false);

        Placar.SetActive(true);
        imageBackGroundSelecao.SetActive(true);
        botaoSelect.SetActive(true);
        botaoNext.SetActive(true);
        botaoPreview.SetActive(true);
        Player.SetActive(true);
        name.SetActive(true);

        _LootLocker_Sistema.MostrarPlacar();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
