using UnityEngine;

[CreateAssetMenu(fileName = "HeavyStats", menuName = "Character/Stats/Heavy")]
public class CharacterStats : ScriptableObject
{
    [Header("Character Info")]
    public string characterName = "Heavy";

    [Header("Base Stats")]
    public int baseHealth = 120; // Health per bar
    public int basePower = 40;
    public int baseSpeed = 20;
    public int baseDefense = 50;

    [Header("Dynamic Stats")]
    [HideInInspector] public int currentHealth; // Current health in the active bar
    [HideInInspector] public int currentPower;
     [HideInInspector] public int currentSpeed;
    [HideInInspector] public int currentDefense;
    [HideInInspector] public int activeHealthBar = 1; // Tracks which health bar is active

    [Header("Skills")]
    public Skill[] defaultSkills = new Skill[4]; // Selected skills

    [Header("Passive Skill")]
    public Skill passiveSkill;  // Passive skill

      [Header("Trap Skills")]
    public TrapSkill[] trapSkills = new TrapSkill[5]; // Trap skills (slots)


    public void AddTrapSkill(TrapSkill skill)
{
    // Loop through the array to find contiguous empty slots
    for (int i = 0; i <= trapSkills.Length - skill.slotsRequired; i++)
    {
        bool slotsAvailable = true;

        // Check if there are enough consecutive empty slots to fit the trap skill
        for (int j = 0; j < skill.slotsRequired; j++)
        {
            if (trapSkills[i + j] != null) // If any slot is already occupied
            {
                slotsAvailable = false;
                break;
            }
        }

        if (slotsAvailable)
        {
            // If enough slots are available, place the skill into these slots
            for (int j = 0; j < skill.slotsRequired; j++)
            {
                trapSkills[i + j] = skill; // Assign the skill to the empty slots
            }

            Debug.Log("Trap Skill added: " + skill.name);
            return; // Exit the method after adding the skill
        }
    }

    // If we reach here, it means there were no sufficient contiguous empty slots
    Debug.Log("Not enough contiguous slots for this trap skill.");
}

    


    public void Initialize()
    {
        // Initialize stats based on the first health bar
        currentHealth = baseHealth;
        currentPower = basePower;
        currentSpeed = baseSpeed;
        currentDefense = baseDefense;
        activeHealthBar = 1;
    }

    public void UpdateStatsOnHealthBarChange()
    {
        // Adjust stats based on the active health bar
        switch (activeHealthBar)
        {
            case 1: // First health bar
                currentPower = basePower;
                currentDefense = baseDefense;
                break;

            case 2: // Second health bar
                currentPower = basePower + 5; // Increase power
                currentDefense = baseDefense - 5; // Decrease defense
                break;

            case 3: // Third health bar
                currentPower = basePower + 10; // Further increase power
                currentDefense = baseDefense - 10; // Further decrease defense
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
            // Move to the next health bar
            activeHealthBar++;
            currentHealth = baseHealth; // Reset health for the next bar
            UpdateStatsOnHealthBarChange();
            return true; // Indicates a health bar was depleted
        }

        return currentHealth > 0; // Returns whether the character is still alive
    }


    public void Heal(int amount)
{
    if (currentHealth + amount <= baseHealth)
    {
        // Heal within the current health bar's limit
        currentHealth += amount;
    }
    else
    {
        // Calculate overflow healing that spills into the next bar
        int overflowHealing = (currentHealth + amount) - baseHealth;

        currentHealth = baseHealth; // Fill current health bar

        // Move to the next health bar if applicable
        if (activeHealthBar < 3)
        {
            activeHealthBar++;
            currentHealth = overflowHealing > baseHealth ? baseHealth : overflowHealing; // Heal in the next bar
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
    // Add any additional logic for speed limits if necessary
}

public void ModifyPower(int amount)
{
    currentPower += amount;
    // Add any additional logic for power limits if necessary
}

public void ModifyDefense(int amount)
{
    currentDefense += amount;
    // Add any additional logic for defense limits if necessary
}


}


 

