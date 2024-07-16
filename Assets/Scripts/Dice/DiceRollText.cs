using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceRollText : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    private int lastDiceRollResult;

    void Start()
    {
        UpdateDiceRollText();
    }

    void Update()
    {
        if (DiceRollProperties.DiceRollResult != lastDiceRollResult)
        {
            UpdateDiceRollText();
        }
    }

    void UpdateDiceRollText()
    {
        lastDiceRollResult = DiceRollProperties.DiceRollResult;
        text.text = "Last Dice Roll: " + lastDiceRollResult;
    }
}
