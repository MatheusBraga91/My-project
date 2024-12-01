using UnityEngine;

[CreateAssetMenu(fileName = "Hack God", menuName = "TrapSkills/HackGod")]


public class HackGod : TrapSkill
{
    private bool isEffectApplied = false;

    public override void TriggerEffect(CharacterStats userStats, CharacterStats enemyStats,Skill enemySkill)
    {
        if (enemyStats != null && !isEffectApplied)
        {
            // Directly modify the emeny's current stats
            enemyStats.currentPower -= 15;
            enemyStats.currentDefense -= 10;
            

            isEffectApplied = true; // Prevent further application
        }
    }
}
