using UnityEngine;

public abstract class PassiveSkill : ScriptableObject
{
    public string skillName;
    public string skillDescription;

    public abstract void Activate(CharacterStats character);
}
