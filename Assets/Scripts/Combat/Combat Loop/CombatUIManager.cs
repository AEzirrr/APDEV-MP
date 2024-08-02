using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatUIManager : MonoBehaviour
{
    public CombatManager combatManager;
    public Button attackButton;
    public Button healButton;
    public Button moveButton;
    public TextMeshProUGUI turnCounterText;
    public TextMeshProUGUI diceRollResultText;
    public TextMeshProUGUI damageDealtText;
    public bool ActionSelected { get; set; }
    public ActionType SelectedAction { get; private set; }

    private Vector2Int selectedTargetTile; // Track the selected tile

    public enum ActionType
    {
        None,
        Attack,
        Heal,
        Move
    }

    private void Start()
    {
        attackButton.onClick.AddListener(OnAttackButton);
        healButton.onClick.AddListener(OnHealButton);
        moveButton.onClick.AddListener(OnMoveButton);
    }

    private void Update()
    {
        // Handle tapping on tiles to select the target position for movement
        if (SelectedAction == ActionType.Move && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Tile"))
                {
                    Tile tile = hit.collider.GetComponent<Tile>();
                    if (tile != null)
                    {
                        selectedTargetTile = tile.cords;
                        combatManager.PlayerMove(selectedTargetTile);
                        ResetActionState(); // Reset action state after movement
                    }
                }
            }
        }
    }

    private void OnAttackButton()
    {
        SelectedAction = ActionType.Attack;
        ActionSelected = true;
    }

    private void OnHealButton()
    {
        SelectedAction = ActionType.Heal;
        ActionSelected = true;
    }

    private void OnMoveButton()
    {
        SelectedAction = ActionType.Move;
        ActionSelected = true; // Enable tile selection in Update
    }

    public void StartTileSelection()
    {
        // Any setup needed for tile selection
    }

    public void UpdateTurnCounter(int turn)
    {
        turnCounterText.text = "Turn: " + turn;
    }

    public void UpdateDiceRollResult(int result)
    {
        diceRollResultText.text = "Dice Roll: " + result;
        Debug.Log("Dice Roll Result: " + result);
    }

    public void UpdateDamageDealt(int damage)
    {
        damageDealtText.text = "Damage: " + damage;
    }

    public void ResetActionState()
    {
        ActionSelected = false;
        SelectedAction = ActionType.None;
    }
}
