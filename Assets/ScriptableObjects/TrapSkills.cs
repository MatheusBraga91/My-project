using UnityEngine;

[CreateAssetMenu(fileName = "NewTrapSkill", menuName = "Skills/TrapSkill")]
public class TrapSkill : ScriptableObject
{
    public string skillName; // Name of the trap skill
    public string description; // Description of the trap skill
    public int slotRequired; // How many trap slots this skill occupies
    public int lifespan; // How many turns the trap stays active
    public bool isActivatable; // Whether the trap can be manually activated

    public SkillCategory activationCategory; // Skill category that triggers the trap
    public string activationMessage; // Message displayed when the trap is set
    public string effectMessage; // Message displayed when the trap is activated

    // Virtual method to be overridden for custom effects
    public virtual void TriggerEffect(/*BattleContext context*/)
    {
        Debug.Log($"{skillName} triggered, but no effect implemented.");
    }
}
