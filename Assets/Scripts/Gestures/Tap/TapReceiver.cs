using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapReceiver : MonoBehaviour
{
    // Template GameObject to be instantiated on tap
    [SerializeField] private GameObject template;
    [SerializeField] private List<GameObject> templateList;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to the OnTap event from the GestureManager
        GestureManager.Instance.OnTap += this.OnTap;
    }

    // Method called when the object is disabled
    private void OnDisable()
    {
        // Unsubscribe from the OnTap event when the object is disabled
        GestureManager.Instance.OnTap -= this.OnTap;
    }

    // Event handler method for the OnTap event
    public void OnTap(object sender, TapEventArgs args)
    {
        if (args.HitObject == null)
        {
            Debug.Log("TapReceiver: OnTap called");

            Ray ray = Camera.main.ScreenPointToRay(args.Position);
            Vector3 spawnPos = ray.GetPoint(10);

            GameObject instance = GameObject.Instantiate(this.template, spawnPos, Quaternion.identity);
            instance.SetActive(true);
            this.templateList.Add(instance);
        }
    }
}
