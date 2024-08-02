using UnityEngine;
using UnityEngine.UI;

public class CombatUIManager : MonoBehaviour
{
    public CombatManager combatManager;
    public Button attackButton;
    public Button healButton;
    public Button moveButton;
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
        // Handle clicking on tiles to select the target position for movement
        if (Input.GetMouseButtonDown(0) && SelectedAction == ActionType.Move)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Tile"))
                {
                    Tile tile = hit.collider.GetComponent<Tile>();
                    if (tile != null)
                    {
                        selectedTargetTile = tile.cords;
                        combatManager.PlayerMove(selectedTargetTile);
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

    public void UpdateDiceRollResult(int result)
    {
        // Implement UI logic to update dice roll result
        Debug.Log("Dice Roll Result: " + result);
    }

    public void ResetActionState()
    {
        ActionSelected = false;
        SelectedAction = ActionType.None;
    }
}
