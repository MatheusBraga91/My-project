using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public AIPanelController aiPanelController;
    public UserPanelController userPanelController;
    public EnemyUIUpdater enemyUIUpdater;


    public static CharacterStats aiCharacterClone; // Static so it persists across scenes
    public static CharacterStats userCharacterClone;

    public void StartBattle()
    {
        if (aiPanelController == null || userPanelController == null)
        {
            aiPanelController = FindObjectOfType<AIPanelController>(); // Manually find if not assigned
            userPanelController = FindObjectOfType<UserPanelController>(); // Manually find if not assigned
        }

        if (aiPanelController.characterStats == null || userPanelController.characterStats == null)
        {
            Debug.LogError("Assigned characters are missing! Cannot start the battle.");
            return;
        }

        aiCharacterClone = CloneCharacterStats(aiPanelController.characterStats);
        userCharacterClone = CloneCharacterStats(userPanelController.characterStats);

        // Initialize the cloned character stats (this is where we ensure currentHealth is set)
        aiCharacterClone.Initialize();
        userCharacterClone.Initialize();

        Debug.Log("Battle started with cloned characters!");

        DontDestroyOnLoad(gameObject);

        UnityEngine.SceneManagement.SceneManager.LoadScene("BattleTraining");
    }

    private CharacterStats CloneCharacterStats(CharacterStats original)
    {
        CharacterStats clone = ScriptableObject.CreateInstance<CharacterStats>();  // Use CreateInstance for ScriptableObject

        // Now copy all the properties from the original to the clone
        clone.characterName = original.characterName;
        clone.baseHealth = original.baseHealth;
        clone.basePower = original.basePower;
        clone.baseSpeed = original.baseSpeed;
        clone.baseDefense = original.baseDefense;
        clone.passiveSkill = original.passiveSkill; // Assuming passiveSkill is a simple reference
        clone.defaultSkills = CloneSkills(original.defaultSkills);  // Clone default skills array
        clone.trapSkills = CloneTrapSkills(original.trapSkills);  // Clone trap skills array
        clone.frontViewImage = original.frontViewImage; // Clone the frontViewImage too
        clone.backViewImage = original.backViewImage; // Clone the frontViewImage too

        return clone;
    }

    private Skill[] CloneSkills(Skill[] originalSkills)
    {
        Skill[] cloneSkills = new Skill[originalSkills.Length];
        for (int i = 0; i < originalSkills.Length; i++)
        {
            cloneSkills[i] = originalSkills[i];  // Assuming no deep copy needed for Skill objects, just references
        }
        return cloneSkills;
    }

    private TrapSkill[] CloneTrapSkills(TrapSkill[] originalTrapSkills)
    {
        TrapSkill[] cloneTrapSkills = new TrapSkill[originalTrapSkills.Length];
        for (int i = 0; i < originalTrapSkills.Length; i++)
        {
            cloneTrapSkills[i] = originalTrapSkills[i];  // Assuming no deep copy needed for TrapSkill objects, just references
        }
        return cloneTrapSkills;
    }

    // New method to apply skill damage
    public void ApplyDamage(CharacterStats attacker, CharacterStats defender, Skill skill)
    {
        if (skill == null)
        {
            Debug.Log($"{attacker.characterName} has no skill to use!");
            return;
        }

        // Apply defense change if the skill has one
        if (skill.enemyDefenseChange != 0)
        {
            // Log before and after defense values
            Debug.Log($"{defender.characterName}'s defense before: {defender.baseDefense}");

            // Modify the defense (could be an increase or decrease based on skill)
            defender.baseDefense += skill.enemyDefenseChange;

            // Ensure that defense doesn't go below 0 (optional)
            defender.baseDefense = Mathf.Max(defender.baseDefense, 0);

            Debug.Log($"{defender.characterName}'s defense after: {defender.baseDefense}");
        }

         // Apply defense change if the skill has one
        if (skill.enemySpeedChange != 0)
        {
            // Log before and after defense values
            Debug.Log($"{defender.characterName}'s speed before: {defender.baseSpeed}");

            // Modify the defense (could be an increase or decrease based on skill)
            defender.baseSpeed += skill.enemySpeedChange;

            // Ensure that defense doesn't go below 0 (optional)
            defender.baseSpeed = Mathf.Max(defender.baseSpeed, 0);

            Debug.Log($"{defender.characterName}'s speed after: {defender.baseSpeed}");
        }

        
            // Apply defense change if the skill has one
        if (skill.enemyPowerChange != 0)
        {
            // Log before and after defense values
            Debug.Log($"{defender.characterName}'s power before: {defender.basePower}");

            // Modify the defense (could be an increase or decrease based on skill)
            defender.basePower += skill.enemyPowerChange;

            // Ensure that defense doesn't go below 0 (optional)
            defender.basePower = Mathf.Max(defender.basePower, 0);

            Debug.Log($"{defender.characterName}'s power after: {defender.basePower}");
        }


        // Calculate damage (using the modified defense)

        int damage = 0;
        if (skill.power > 0)

        {
         damage = attacker.basePower + skill.power - defender.baseDefense; // Adjusted formula to use modified defense
        damage = Mathf.Max(damage, 0); // Ensure damage can't be negative
}

        // Apply the damage to the defender
        defender.currentHealth -= damage;

        if (damage > 0)
{
        // Log the damage event
        Debug.Log($"{attacker.characterName} used {skill.skillName} and dealt {damage} damage to {defender.characterName}!");
        Debug.Log($"{defender.characterName}'s current health: {defender.currentHealth}");

     if (enemyUIUpdater != null)
        {
            enemyUIUpdater.UpdateEnemyHealthUI(defender.currentHealth, defender.baseHealth);
        }
    }


    }
}