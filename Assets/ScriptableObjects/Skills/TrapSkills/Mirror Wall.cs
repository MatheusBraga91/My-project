using UnityEngine;

[CreateAssetMenu(fileName = "MirrorWall", menuName = "TrapSkills/MirrorWall")]
public class MirrorWall : TrapSkill
{
    private bool isEffectApplied = false;

    // Override the TriggerEffect method
    public override void TriggerEffect(CharacterStats userStats, CharacterStats enemyStats, Skill enemySkill)
    {
        if (!isEffectApplied && userStats != null && enemyStats != null)
        {
            // Use the enemy's skill power and the user's defense to calculate the damage
            int damageToUser = enemySkill.CalculateDamage(enemyStats.currentPower, userStats.currentDefense);

            // Redirect the damage to the enemy instead of the user
            enemyStats.TakeDamage(damageToUser); // Apply the damage to the enemy's health

            isEffectApplied = true; // Prevent further effect application
            Debug.Log($"Mirror Wall activated! {damageToUser} damage redirected to the enemy.");
        }
    }
}
