using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class cameraDinamica : MonoBehaviour
{
    public GameObject camA;
    public GameObject camB;
    private bool achar = true;

    
    private void OnTriggerEnter(Collider other)
    {
        if (achar)
        {
            camA = GameObject.Find("CM vcam1");
            camB = GameObject.Find("CM vcam2");
            achar = false;
        }

        switch (other.gameObject.tag)
        {
            case "CamTrigger":
                camA.SetActive(false);
                camB.GetComponent<CinemachineVirtualCamera>().enabled = true;
                camB.SetActive(true);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "CamTrigger":
                camA.SetActive(true);
                camB.SetActive(false);
                camB.GetComponent<CinemachineVirtualCamera>().enabled = false;
                break;
        }
    }
    
}
