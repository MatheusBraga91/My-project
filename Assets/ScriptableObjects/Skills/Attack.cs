using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Character/Skill/Attack")]
public class AttackSkill : Skill
{
    private void OnEnable()
    {
        // Set the skill category to Attack
        skillCategory = SkillCategory.Attack;
    }
}