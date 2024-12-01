using UnityEngine;

public abstract class TrapSkill : ScriptableObject
{
    public string skillName; // Name of the trap skill
    public string description; // Description of the trap skill
    public int slotRequired; // How many trap slots this skill occupies
    public int lifespan; // How many turns the trap stays untriggered
    public bool isActivatable; // Whether the trap can be manually activated

    [Tooltip("The category that triggers this trap if isActivatable is false.")]
    public SkillCategory activationCategory; // Skill category that triggers the trap

    public string activationMessage; // Message displayed when the trap is activated
    public string effectMessage; // Message displayed while the trap is active

    public int activeDuration; // How many turns the trap stays active after activation
    public bool isActive; // Whether the trap is currently active

    public abstract void TriggerEffect(CharacterStats userStats, CharacterStats enemyStats,Skill enemySkill);

    /// <summary>
    /// Called to check and manage the trap's lifecycle each turn.
    /// </summary>
    public void UpdateTrapState(int currentTurn, int setTurn, int activationTurn)
    {
        if (!isActive)
        {
            // Trap expires if not activated within its lifespan
            if (currentTurn - setTurn >= lifespan)
            {
                Debug.Log($"{skillName} expired without being activated.");
                RemoveTrap();
            }
        }
        else
        {
            // Trap expires after its active duration
            if (currentTurn - activationTurn >= activeDuration)
            {
                Debug.Log($"{skillName} expired after being active.");
                RemoveTrap();
            }
        }
    }

    /// <summary>
    /// Handles removing the trap from the field.
    /// </summary>
    public void RemoveTrap()
    {
        Debug.Log($"{skillName} is removed from the field.");
        // Add logic to remove the trap from the field (e.g., notify the battle manager)
    }
}
