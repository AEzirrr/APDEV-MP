using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    public string characterName;
    public int maxHealth;
    public int currentHealth;
    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    protected virtual void Die()
    {
        Debug.Log(characterName + " has died.");
    }
}
