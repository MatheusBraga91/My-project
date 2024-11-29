using UnityEngine;

[CreateAssetMenu(fileName = "HeavyStats", menuName = "Character/Stats/Heavy")]
public class CharacterStats : ScriptableObject
{
    [Header("Character Info")]
    public string characterName = "Heavy";

    [Header("Base Stats")]
    public int baseHealth = 120;
    public int basePower = 40;
    public int baseSpeed = 20;
    public int baseDefense = 50;

    [Header("Dynamic Stats")]
    [HideInInspector] public int currentHealth;
    [HideInInspector] public int currentPower;
    [HideInInspector] public int currentSpeed;
    [HideInInspector] public int currentDefense;
    [HideInInspector] public int activeHealthBar = 1;

    [Header("Skills")]
    public Skill[] defaultSkills = new Skill[4]; 

    [Header("Passive Skill")]
    public PassiveSkill passiveSkill;  

    [Header("Trap Skills")]
    public TrapSkill[] trapSkills = new TrapSkill[5]; // Slot for 5 trap skills
    public int availablePoints = 5; // Total points available for trap skills

    // Method to add a trap skill to the character
    public bool AddTrapSkill(TrapSkill skill)
    {
        // Check if we have enough points for the selected skill
        if (skill.slotRequired <= availablePoints)
        {
            // Find the first empty slot to place the skill
            for (int i = 0; i < trapSkills.Length; i++)
            {
                if (trapSkills[i] == null)
                {
                    // Add the trap skill to the slot
                    trapSkills[i] = skill;

                    // Subtract the required points from availablePoints
                    availablePoints -= skill.slotRequired;
                    Debug.Log($"{skill.skillName} added! Available points left: {availablePoints}");
                    return true;
                }
            }
        }
        else
        {
            Debug.Log("Not enough points available to add this skill.");
            return false;
        }

        return false;
    }

    // Optional: Method to remove a trap skill and return its points to availablePoints
    public void RemoveTrapSkill(int index)
    {
        if (index >= 0 && index < trapSkills.Length && trapSkills[index] != null)
        {
            availablePoints += trapSkills[index].slotRequired; // Restore the points used by the skill
            trapSkills[index] = null;
            Debug.Log($"Trap skill removed! Available points left: {availablePoints}");
        }
    }

    // Optional: Method to check available points
    public int GetAvailablePoints()
    {
        return availablePoints;
    }

// Method to add a passive skill to the character
    public bool AddPassiveSkill(PassiveSkill skill)
    {
        // Ensure we only add passive skills to the passiveSkill slot
        if (skill != null && skill is PassiveSkill)
        {
            passiveSkill = skill;
            Debug.Log($"{skill.skillName} added as passive skill.");
            return true;
        }
        else
        {
            Debug.LogError("This skill cannot be assigned as a passive skill.");
            return false;
        }
    }


    // Initialize stats based on the character's default health bar
    public void Initialize()
    {
        currentHealth = baseHealth;
        currentPower = basePower;
        currentSpeed = baseSpeed;
        currentDefense = baseDefense;
        activeHealthBar = 1;
    }

    // Update stats when changing health bars
    public void UpdateStatsOnHealthBarChange()
    {
        switch (activeHealthBar)
        {
            case 1:
                currentPower = basePower;
                currentDefense = baseDefense;
                break;
            case 2:
                currentPower = basePower + 5;
                currentDefense = baseDefense - 5;
                break;
            case 3:
                currentPower = basePower + 10;
                currentDefense = baseDefense - 10;
                break;
            default:
                Debug.LogWarning($"{characterName} is out of health bars!");
                break;
        }
    }

    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && activeHealthBar < 3)
        {
            activeHealthBar++;
            currentHealth = baseHealth;
            UpdateStatsOnHealthBarChange();
            return true;
        }

        return currentHealth > 0;
    }

    public void Heal(int amount)
    {
        if (currentHealth + amount <= baseHealth)
        {
            currentHealth += amount;
        }
        else
        {
            int overflowHealing = (currentHealth + amount) - baseHealth;
            currentHealth = baseHealth;

            if (activeHealthBar < 3)
            {
                activeHealthBar++;
                currentHealth = overflowHealing > baseHealth ? baseHealth : overflowHealing;
            }
            else
            {
                Debug.Log("Healing exceeds max health across all bars!");
            }
        }

        Debug.Log($"Healed by {amount}. Current health: {currentHealth}, Active health bar: {activeHealthBar}");
    }

    public void ModifySpeed(int amount)
    {
        currentSpeed += amount;
    }

    public void ModifyPower(int amount)
    {
        currentPower += amount;
    }

    public void ModifyDefense(int amount)
    {
        currentDefense += amount;
    }
}
