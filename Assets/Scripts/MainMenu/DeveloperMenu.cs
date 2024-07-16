using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeveloperMenu : MonoBehaviour
{
    [SerializeField] MenuUiDocument menu;
    [SerializeField] private Button devButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject DeveloperPanel;
    [SerializeField] private Sprite succeedSprite;
    [SerializeField] private Sprite failSprite;
    [SerializeField] private Sprite normalSprite;

    [SerializeField] private AudioClip ButtonSFX;

    // Start is called before the first frame update
    void Start()
    {
        Image devImage = devButton.GetComponent<Image>();
        backButton.onClick.AddListener(OnBackButtonClicked);
        DeveloperProperties.DiceRoll = EDiceRoll.DICE_ROLL_NORMAL;
        devImage.sprite = normalSprite;
    }

    public void OnBackButtonClicked()
    {
        SFXManager.instance.PlaySfxClip(ButtonSFX, transform, .1f);
        DeveloperPanel.SetActive(false);
        menu.DisplayMenu();
    }

    public void OnDiceRollClicked()
    {
        Image devImage = devButton.GetComponent<Image>();

        SFXManager.instance.PlaySfxClip(ButtonSFX, transform, .1f);
        if (DeveloperProperties.DiceRoll == EDiceRoll.DICE_ROLL_NORMAL)
        {
            DeveloperProperties.DiceRoll = EDiceRoll.DICE_ROLL_SUCCEED;
            devImage.sprite = succeedSprite;
        }
        else if (DeveloperProperties.DiceRoll == EDiceRoll.DICE_ROLL_SUCCEED)
        {
            DeveloperProperties.DiceRoll = EDiceRoll.DICE_ROLL_FAIL;
            devImage.sprite = failSprite;
        }
        else if (DeveloperProperties.DiceRoll == EDiceRoll.DICE_ROLL_FAIL)
        {
            DeveloperProperties.DiceRoll = EDiceRoll.DICE_ROLL_NORMAL;
            devImage.sprite = normalSprite;
        }

    }
      
    // Update is called once per frame
    void Update()
    {

    }
}
