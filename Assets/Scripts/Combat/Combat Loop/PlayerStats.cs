using UnityEngine;

public class PlayerStats : CharacterStats
{
    public ClassStats classStats;

    private void Start()
    {
        if (classStats != null)
        {
            InitializeStatsFromClass(classStats);
        }
    }

    private void InitializeStatsFromClass(ClassStats classStats)
    {
        characterName = classStats.className;
        strength = classStats.strength;
        dexterity = classStats.dexterity;
        constitution = classStats.constitution;
        intelligence = classStats.intelligence;
        wisdom = classStats.wisdom;
        charisma = classStats.charisma;

        
        maxHealth = constitution * 10; // Example formula
        currentHealth = maxHealth;
    }
}
