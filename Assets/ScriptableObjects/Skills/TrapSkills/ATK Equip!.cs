using UnityEngine;

[CreateAssetMenu(fileName = "AttackEquip", menuName = "TrapSkills/AttackEquip")]


public class AttackEquip : TrapSkill
{
    private bool isEffectApplied = false;

    public override void TriggerEffect(CharacterStats userStats, CharacterStats enemyStats,Skill enemySkill)
    {
        if (userStats != null && !isEffectApplied)
        {
            // Directly modify the user's current power
            userStats.currentPower += 15; 
          

            isEffectApplied = true; // Prevent further application
        }
    }
}
