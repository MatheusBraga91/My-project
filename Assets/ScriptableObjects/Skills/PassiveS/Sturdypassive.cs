using UnityEngine;

[CreateAssetMenu(fileName = "Sturdy", menuName = "Character/Passive Skills/Sturdy")]
public class Sturdy : PassiveSkill
{
    public override void Activate(CharacterStats character)
    {
        if (character.currentHealth <= 0 && character.activeHealthBar == 1)
        {
            character.currentHealth = 1;
            Debug.Log("Sturdy activated: Health remains at 1.");
        }
    }
}
