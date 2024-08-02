using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public PlayerStats player;
    public EnemyStats enemy;
    public InternalDice internalDice;
    public CombatUIManager combatUIManager;
    public GridManager gridManager;
    public Pathfinding pathfinding;
    [SerializeField] private MarketManager marketManager;

    private int turnCounter = 0;

    private void Start()
    {
        // Update references to instantiated player and enemy
        player = gridManager.playerInstance.GetComponent<PlayerStats>();
        enemy = gridManager.enemyInstance.GetComponent<EnemyStats>();

        StartCoroutine(CombatRoutine());
    }

    private IEnumerator CombatRoutine()
    {
        while (player.currentHealth > 0 && enemy.currentHealth > 0)
        {
            UpdateTurnCounterUI();
            yield return PlayerTurn();
            if (enemy.currentHealth <= 0) break;

            UpdateTurnCounterUI();
            yield return EnemyTurn();
        }

        EndCombat();
    }

    private void UpdateTurnCounterUI()
    {
        turnCounter++;
        combatUIManager.UpdateTurnCounter(turnCounter);
    }

    private IEnumerator PlayerTurn()
    {
        // Wait for player input via UI
        yield return new WaitUntil(() => combatUIManager.ActionSelected);

        // Perform the selected action
        if (combatUIManager.SelectedAction == CombatUIManager.ActionType.Attack)
        {
            PlayerAttack();
        }
        else if (combatUIManager.SelectedAction == CombatUIManager.ActionType.Heal)
        {
            PlayerHeal();
        }
        else if (combatUIManager.SelectedAction == CombatUIManager.ActionType.Move)
        {
            // Move action will be handled by clicking on the target tile
            combatUIManager.StartTileSelection();
            yield return new WaitUntil(() => !combatUIManager.ActionSelected); // Wait until the move action is completed
        }

        // Reset action state
        combatUIManager.ResetActionState();
    }

    public void PlayerAttack()
    {
        if (IsInRange(player, enemy, 1)) // Check if the enemy is in range using the trigger collider
        {
            // Roll the dice
            internalDice.OnInternalDiceRolled();
            int damage = internalDice.diceResult + player.strength;

            // Deal damage to the enemy
            enemy.TakeDamage(damage);

            // Display the dice roll result in the UI
            combatUIManager.UpdateDiceRollResult(internalDice.diceResult);

            // Display the damage dealt in the UI
            combatUIManager.UpdateDamageDealt(damage);

            // Print the result to the console
            Debug.Log($"Player rolled {internalDice.diceResult} + {player.strength} strength = {damage} damage.");
            Debug.Log($"Enemy Health: {enemy.currentHealth}/{enemy.maxHealth}");

            // Continue to the enemy's turn
            StartCoroutine(EnemyTurn());
        }
        else
        {
            Debug.Log("Enemy is out of melee range.");
            StartCoroutine(EnemyTurn()); // Continue to the enemy's turn even if the attack fails
        }
    }

    public void PlayerHeal()
    {
        // Roll the dice
        internalDice.OnInternalDiceRolled();
        int healAmount = internalDice.diceResult + player.wisdom;

        // Heal the player
        player.Heal(healAmount);

        // Display the dice roll result in the UI
        combatUIManager.UpdateDiceRollResult(internalDice.diceResult);

        // Display the healing amount in the UI
        combatUIManager.UpdateDamageDealt(healAmount);

        // Print the result to the console
        Debug.Log($"Player rolled {internalDice.diceResult} + {player.wisdom} wisdom = {healAmount} healing.");
        Debug.Log($"Player Health: {player.currentHealth}/{player.maxHealth}");

        // Continue to the enemy's turn
        StartCoroutine(EnemyTurn());
    }

    public void PlayerMove(Vector2Int targetCords)
    {
        Vector2Int playerCoords = gridManager.GetCoordinatesFromPosition(player.transform.position);
        List<Node> path = pathfinding.GetNewPath(playerCoords, targetCords);
        StartCoroutine(MoveAlongPath(path));
        // Continue to the enemy's turn after moving
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator MoveAlongPath(List<Node> path)
    {
        foreach (Node node in path)
        {
            Vector3 targetPosition = gridManager.GetPositionFromCoordinates(node.cords);
            player.transform.position = targetPosition; // Simplified movement logic
            yield return new WaitForSeconds(0.5f); // Adjust as needed for smoothness
        }
    }

    private IEnumerator EnemyTurn()
    {
        if (IsInRange(enemy, player, 1)) // Check if the player is in range using the trigger collider
        {
            // Roll the dice
            internalDice.OnInternalDiceRolled();
            int damage = internalDice.diceResult + enemy.strength;

            // Deal damage to the player
            player.TakeDamage(damage);

            // Display the dice roll result in the UI
            combatUIManager.UpdateDiceRollResult(internalDice.diceResult);

            // Display the damage dealt in the UI
            combatUIManager.UpdateDamageDealt(damage);

            // Print the result to the console
            Debug.Log($"Enemy rolled {internalDice.diceResult} + {enemy.strength} strength = {damage} damage.");
            Debug.Log($"Player Health: {player.currentHealth}/{player.maxHealth}");
        }
        else
        {
            Debug.Log("Player is out of enemy's range.");
        }
        yield return null;
    }

    private bool IsInRange(CharacterStats attacker, CharacterStats target, int range)
    {
        Vector2 attackerPos = new Vector2(attacker.transform.position.x, attacker.transform.position.z);
        Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.z);
        return Vector2.Distance(attackerPos, targetPos) <= range;
    }

    private void EndCombat()
    {
        if (player.currentHealth <= 0)
        {
            Debug.Log("Player defeated!");
            CombatProperties.CombatResult = false;
            CloseCombat();
        }
        else if (enemy.currentHealth <= 0)
        {
            Debug.Log("Enemy defeated!");
            CombatProperties.CombatResult = true;
            CloseCombat();
        }
    }

    ///////////////////////// DEV BUTTONS //////////////////////////

    public void OnWinButton()
    {
        CombatProperties.CombatResult = true;
        CloseCombat();
    }

    public void OnLoseButton()
    {
        CombatProperties.CombatResult = false;
        CloseCombat();
    }

    private void CloseCombat()
    {
        if (marketManager != null)
        {
            marketManager.OnCombatClosed();
        }
    }
}
