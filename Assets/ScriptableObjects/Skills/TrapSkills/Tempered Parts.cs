using UnityEngine;

[CreateAssetMenu(fileName = "Tempered Parts", menuName = "TrapSkills/TemperedParts")]


public class TemperedParts : TrapSkill
{
    private bool isEffectApplied = false;

    public override void TriggerEffect(CharacterStats userStats, CharacterStats enemyStats,Skill enemySkill)
    {
        if (userStats != null && !isEffectApplied)
        {
            // Directly modify the user's current power
            userStats.currentDefense += 15; 
          

            isEffectApplied = true; // Prevent further application
        }
    }
}
