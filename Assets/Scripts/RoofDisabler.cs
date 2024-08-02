using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofDisabler : MonoBehaviour
{
    [SerializeField] private GameObject roof;

    private void Start()
    {
       
        if (roof == null)
        {
            Debug.LogError("Roof GameObject is not assigned in the inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (roof != null && other.CompareTag("Player")) 
        {
            roof.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (roof != null && other.CompareTag("Player")) 
        {
            roof.SetActive(true);
        }
    }
}
