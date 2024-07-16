using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceFaceDetector : MonoBehaviour
{

    private Rigidbody rb;
    private bool isRolling;
    private float rollDelay = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isRolling = false;
    }

    void Update()
    {
        if (isRolling && rb.velocity.sqrMagnitude <= 0.1f)
        {
            isRolling = false;
            Invoke("CheckTopFace", rollDelay);
        }
    }

    void CheckTopFace()
    {
        BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>();
        BoxCollider highestCollider = null;
        float highestY = float.MinValue;

        foreach (BoxCollider col in colliders)
        {
            float faceY = col.bounds.center.y + col.bounds.extents.y;
            if (faceY > highestY)
            {
                highestY = faceY;
                highestCollider = col;
            }
        }

        if (highestCollider != null)
        {
            SideProperty sideProperty = highestCollider.GetComponent<SideProperty>();
            if (sideProperty != null)
            {
                int result = sideProperty.SideValue;
                Debug.Log("Top Face: " + result);
                DiceRollProperties.DiceRollResult = result;
            }

        }

    }

    public void StartRolling()
    {
        isRolling = true;
    }
}
