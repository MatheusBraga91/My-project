using UnityEngine;

// Enum for Skill Categories: Attack or Special
public enum SkillCategory
{
    No,
    Attack,  // For skills that deal damage
    Special  // For debuffs, buffs, healing, etc.
}

[CreateAssetMenu(fileName = "New Skill", menuName = "Character/Skill")]
public class Skill : ScriptableObject
{
    public string skillName; // Name of the skill
    public int power; // Power of the skill (can be used for damage, healing, etc.)
    public int cooldown; // Cooldown time for the skill
    public string description; // Optional description of the skill

    public SkillCategory skillCategory; // Category of the skill (Attack or Special)

    // Flags and values for effects on the user
    public bool affectsUser = false; // Whether the skill affects the user (e.g. damage to the user, healing)
    public int userDamage; // If the skill damages the user
    public int userHealing; // If the skill heals the user
    public int speedChange; // Changes to user speed (e.g. +10 for speed boost)
    public int powerChange; // Changes to user attack power
    public int defenseChange; // Changes to user defense power

    // Flags and values for effects on the enemy (target)
    public bool affectsEnemy = false; // Whether the skill affects the enemy (e.g. debuffing)
    public int enemySpeedChange; // Changes to enemy speed (-5 for slowng dowwn)
    public int enemyPowerChange; // Changes to enemy attack power
    public int enemyDefenseChange; // Changes to enemy defense power

    // Virtual function that can be overridden by child classes (like Body Slam) to handle damage calculation
    public virtual int CalculateDamage(int attackerPower, int targetDefense)
    {
        return Mathf.Max((attackerPower * power / 10) - targetDefense, 1); // Default damage formula
    }

    // Apply effects to the user (if any)
    public void ApplyUserEffects(CharacterStats user)
    {
        if (affectsUser)
        {
            if (userDamage > 0)
            {
                user.TakeDamage(userDamage); // Assuming TakeDamage is a method in CharacterStats
            }

            if (userHealing > 0)
            {
                user.Heal(userHealing); // Assuming Heal is a method in CharacterStats
            }

            if (speedChange != 0)
            {
                user.currentSpeed += speedChange; // Assuming currentSpeed exists in CharacterStats
            }

            if (powerChange != 0)
            {
                user.currentPower += powerChange; // Assuming currentPower exists in CharacterStats
            }

            if (defenseChange != 0)
            {
                user.currentDefense += defenseChange; // Assuming currentDefense exists in CharacterStats
            }
        }
    }

    // Apply effects to the enemy (if any)
    public void ApplyEnemyEffects(CharacterStats enemy)
    {
        if (affectsEnemy)
        {        

            if (enemySpeedChange != 0)
            {
                enemy.currentSpeed += enemySpeedChange; // Assuming currentSpeed exists in CharacterStats
            }

            if (enemyPowerChange != 0)
            {
                enemy.currentPower += enemyPowerChange; // Assuming currentPower exists in CharacterStats
            }

            if (enemyDefenseChange != 0)
            {
                enemy.currentDefense += enemyDefenseChange; // Assuming currentDefense exists in CharacterStats
            }
        }
    }
}