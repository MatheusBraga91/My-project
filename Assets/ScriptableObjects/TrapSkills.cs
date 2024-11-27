using UnityEngine;

[CreateAssetMenu(fileName = "NewTrapSkill", menuName = "Skills/TrapSkill")]
public class TrapSkill : ScriptableObject
{
    public string skillName;
    public string description;
    public int slotsRequired; // How many trap slots this skill occupies
}
