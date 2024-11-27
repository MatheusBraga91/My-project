using UnityEngine;

[CreateAssetMenu(fileName = "Special", menuName = "Character/Skill/Special")]
public class SpecialSkill : Skill
{
    private void OnEnable()
    {
        // Set the skill category to Special
        skillCategory = SkillCategory.Special;
    }
}