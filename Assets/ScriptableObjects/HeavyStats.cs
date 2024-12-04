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
    public int currentHealth;
    public int currentPower;
    public int currentSpeed;
    public int currentDefense;
    public int activeHealthBar = 1;

    [Header("Skills")]
    public Skill[] defaultSkills = new Skill[4];

    [Header("Passive Skill")]
    public PassiveSkill passiveSkill;

    [Header("Trap Skills")]
    public TrapSkill[] trapSkills = new TrapSkill[5]; // Slot for 5 trap skills

    [Header("Character Images")]
    public Sprite frontViewImage; // Front view image
    public Sprite backViewImage;  // Back view image

    [Header("Snapshots")]
    private CharacterStatsSnapshot savedSnapshot;

    // Method to add a passive skill to the character
    public bool AddPassiveSkill(PassiveSkill skill)
    {
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

    // Save the current stats to a snapshot
    public void SaveSnapshot()
    {
        savedSnapshot = new CharacterStatsSnapshot(this);
        Debug.Log($"{characterName} snapshot saved!");
    }

    // Restore stats from the snapshot
    public void RestoreSnapshot()
    {
        if (savedSnapshot != null)
        {
            savedSnapshot.Restore(this);
            Debug.Log($"{characterName} snapshot restored!");
        }
        else
        {
            Debug.LogWarning("No snapshot to restore!");
        }
    }
}

// CharacterStatsSnapshot Class
[System.Serializable]
public class CharacterStatsSnapshot
{
    public int currentHealth;
    public int currentPower;
    public int currentSpeed;
    public int currentDefense;
    public int activeHealthBar;

    public CharacterStatsSnapshot(CharacterStats stats)
    {
        currentHealth = stats.currentHealth;
        currentPower = stats.currentPower;
        currentSpeed = stats.currentSpeed;
        currentDefense = stats.currentDefense;
        activeHealthBar = stats.activeHealthBar;
    }

    public void Restore(CharacterStats stats)
    {
        stats.currentHealth = currentHealth;
        stats.currentPower = currentPower;
        stats.currentSpeed = currentSpeed;
        stats.currentDefense = currentDefense;
        stats.activeHealthBar = activeHealthBar;
    }
}
