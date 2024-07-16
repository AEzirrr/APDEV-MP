using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MarketUIDocument : MonoBehaviour
{
    [SerializeField] MarketManager marketManager;
    private VisualElement _root;
    private VisualElement _dialogueBox;
    private Button _option1;
    private Button _option2;
    private Button _option3;
    private Button _option4;

    private void Start()
    {
        this._root = GetComponent<UIDocument>().rootVisualElement;
        this._dialogueBox = this._root.Q<VisualElement>("DialogueBoxContainer");
        this._option1 = this._root.Q<Button>("Option_1");
        this._option2 = this._root.Q<Button>("Option_2");
        this._option3 = this._root.Q<Button>("Option_3");
        this._option4 = this._root.Q<Button>("Option_4");

        this._option1.clicked += this.OnOption1Clicked;
        this._option2.clicked += this.OnOption2Clicked;
        this._option3.clicked += this.OnOption3Clicked;
        this._option4.clicked += this.OnOption4Clicked;

        this.HideDialogueBox();

        EventBroadcaster.Instance.AddObserver(EventNames.NPC_Dialogue_Events.ON_NPC_TAPPED, this.ShowDialogueBox);
        EventBroadcaster.Instance.AddObserver(EventNames.NPC_Dialogue_Events.ON_NPC_NOT_IN_RANGE, this.HideDialogueBox);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.NPC_Dialogue_Events.ON_NPC_TAPPED);
        EventBroadcaster.Instance.RemoveObserver(EventNames.NPC_Dialogue_Events.ON_NPC_NOT_IN_RANGE);
    }

    private void OnOption1Clicked()
    {
        marketManager.OnDiceButtonClicked("DiceRoll");

        int PlayerStat = PlayerPrefs.GetInt(DialogueStats.Op1Type, 0);
        int diceBonus;

        if (PlayerStat >= DialogueStats.Option1) {
            if (PlayerStat > 10) {
                diceBonus = PlayerStat - 10;
            }
            else{ 
                diceBonus = 0; 
            }

            int diceRoll = DiceRollProperties.DiceRollResult;

            if (((diceRoll + diceBonus) >= DialogueStats.Option1) || DeveloperProperties.DiceRoll == EDiceRoll.DICE_ROLL_SUCCEED) {
                //succeed
                Debug.Log("[Option 1] : Success");
            }
            else if (DeveloperProperties.DiceRoll == EDiceRoll.DICE_ROLL_FAIL) {
                //fail
                Debug.Log("[Option 1] : Failed");
            }
            else {
                //fail
                Debug.Log("[Option 1] : Failed");
            }
        }

        else {
            int diceRoll = DiceRollProperties.DiceRollResult;

            if ((diceRoll >= DialogueStats.Option1 + 1) || DeveloperProperties.DiceRoll == EDiceRoll.DICE_ROLL_SUCCEED) {
                //success
                Debug.Log("[Option 1] : Success");
            }
            else if (DeveloperProperties.DiceRoll == EDiceRoll.DICE_ROLL_FAIL) {
                //fail
                Debug.Log("[Option 1] : Failed");
            }
            else {
                //fail
                Debug.Log("[Option 1] : Failed");
            }
        }
    }

    private void OnOption2Clicked()
    {
        marketManager.OnDiceButtonClicked("DiceRoll");

        int PlayerStat = PlayerPrefs.GetInt(DialogueStats.Op2Type, 0);
        int diceBonus;

        if (PlayerStat >= DialogueStats.Option2)
        {
            if (PlayerStat > 10)
            {
                diceBonus = PlayerStat - 10;
            }
            else
            {
                diceBonus = 0;
            }

            int diceRoll = DiceRollProperties.DiceRollResult;

            if (((diceRoll + diceBonus) >= DialogueStats.Option2) || DeveloperProperties.DiceRoll == EDiceRoll.DICE_ROLL_SUCCEED)
            {
                //succeed
                Debug.Log("[Option 2] : Success");
            }
            else if (DeveloperProperties.DiceRoll == EDiceRoll.DICE_ROLL_FAIL)
            {
                //fail
                Debug.Log("[Option 2] : Failed");
            }
            else
            {
                //fail
                Debug.Log("[Option 2] : Failed");
            }
        }

        else
        {
            int diceRoll = DiceRollProperties.DiceRollResult;

            if ((diceRoll >= DialogueStats.Option2 + 1) || DeveloperProperties.DiceRoll == EDiceRoll.DICE_ROLL_SUCCEED)
            {
                //success
                Debug.Log("[Option 2] : Success");
            }
            else if (DeveloperProperties.DiceRoll == EDiceRoll.DICE_ROLL_FAIL)
            {
                //fail
                Debug.Log("[Option 2] : Failed");
            }
            else
            {
                //fail
                Debug.Log("[Option 2] : Failed");
            }
        }
    }

    private void OnOption3Clicked()
    {
        Debug.Log("Option_3 Clicked");
    }

    private void OnOption4Clicked()
    {
        this.HideDialogueBox();
    }

    public void HideDialogueBox()
    {
        Debug.Log("HideDialogueBox: Hiding the dialogue box");
        this._dialogueBox.style.visibility = Visibility.Hidden;
    }

    private void ShowDialogueBox()
    {
        Debug.Log("ShowDialogueBox: Showing the dialogue box");
        this._dialogueBox.style.visibility = Visibility.Visible;
        this._option1.text = DialogueStats.Op1Text + " >= " + DialogueStats.Option1.ToString();
        this._option2.text = DialogueStats.Op2Text + " >= " + DialogueStats.Option2.ToString();

        Debug.Log(DialogueStats.Op1Type);
        Debug.Log(DialogueStats.Option1);
        Debug.Log(DialogueStats.Op1Text);
        Debug.Log(DialogueStats.Op2Type);
        Debug.Log(DialogueStats.Option2);
        Debug.Log(DialogueStats.Op2Text);
    }

}
