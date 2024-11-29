using UnityEngine;

[CreateAssetMenu(fileName = "MirrorWall", menuName = "Skills/TrapSkill/MirrorWall")]
public class MirrorWall : TrapSkill
{
    public override void TriggerEffect(/*BattleContext context*/)
    {
        // Temporarily commenting out the BattleContext logic
        Debug.Log($"Mirror Wall logic commented out. Effect not implemented yet.");
        
        // Example placeholder logic, for demonstration purposes:
        Debug.Log($"Mirror Wall triggered! This is a placeholder message.");
        
        // Uncomment and replace the following logic when BattleContext is ready:
        /*
        if (context.enemySkill.category == SkillCategory.Attack)
        {
            int damageToReflect = context.damageDealt;
            context.enemy.TakeDamage(damageToReflect);
            Debug.Log($"Mirror Wall activated! Reflected {damageToReflect} damage to the enemy.");
        }
        else
        {
            Debug.Log($"Mirror Wall was not triggered because the enemy did not use an Attack skill.");
        }
        */
    }
}
