using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public GameObject dice;
    public float shakeThreshold = 2.0f;
    public float shakeTimeout = 1.0f;

    private Vector3 acceleration;
    private float lastShakeTime;
    private DiceFaceDetector diceFaceDetector;
    private bool hasRolled;

    void Start()
    {
        diceFaceDetector = dice.GetComponent<DiceFaceDetector>();
        hasRolled = false;
    }

    void Update()
    {
        if (!hasRolled)
        {
            acceleration = Input.acceleration;

            if (acceleration.sqrMagnitude >= shakeThreshold * shakeThreshold && Time.time >= lastShakeTime + shakeTimeout)
            {
                lastShakeTime = Time.time;
                RollDice();
            }
        }
    }

    void RollDice()
    {
        Rigidbody rb = dice.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(Random.onUnitSphere * 5.0f, ForceMode.Impulse);
            rb.AddTorque(Random.onUnitSphere * 10.0f, ForceMode.Impulse);
        }

        if (diceFaceDetector != null)
        {
            diceFaceDetector.StartRolling();
        }

        hasRolled = true;
    }
}
