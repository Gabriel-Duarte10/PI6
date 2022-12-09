using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<CinemachineVirtualCamera>().Follow = player;
        player.position = new Vector3(-10.32457f, 0.4942331f, -19.25416f);
        player.eulerAngles = new Vector3(0,0,0);
    }
}
