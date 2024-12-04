using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public AIPanelController aiPanelController;
    public UserPanelController userPanelController;
    public EnemyUIUpdater enemyUIUpdater;
    public UserUIUpdater userUIUpdater;

       public static BattleManager Instance { get; private set; }

    public CharacterStats userCharacter;
    public CharacterStats aiCharacter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartBattle()
{
    if (aiPanelController == null || userPanelController == null)
    {
        aiPanelController = FindObjectOfType<AIPanelController>();
        userPanelController = FindObjectOfType<UserPanelController>();
    }

    if (aiPanelController.characterStats == null || userPanelController.characterStats == null)
    {
        Debug.LogError("Assigned characters are missing! Cannot start the battle.");
        return;
    }

    aiCharacter = aiPanelController.characterStats;
    userCharacter = userPanelController.characterStats;

    aiCharacter.Initialize();
    userCharacter.Initialize();

    Debug.Log("Battle started with assigned characters!");

    DontDestroyOnLoad(gameObject);
    UnityEngine.SceneManagement.SceneManager.LoadScene("BattleTraining");

    InitializeUIComponents();
}

private void InitializeUIComponents()
{
    enemyUIUpdater?.InitializeUI();
    userUIUpdater?.InitializeUI();
}


    // Updated ApplyDamage method
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
            enemyUIUpdater.UpdateEnemyHealthUI();
        }
    }

        // Apply damage to the user (self-inflicted damage)
        if (skill.userDamage > 0)
        {
            Debug.Log($"{attacker.characterName} takes {skill.userDamage} damage from {skill.skillName}!");
            attacker.TakeDamage(skill.userDamage); // Assuming TakeDamage reduces health and handles related logic
        }

        // Update user UI (similar to how enemy UI is updated)
        if (userUIUpdater != null) // Assuming you have a playerUIUpdater like enemyUIUpdater
        {
            userUIUpdater.UpdateHealthUI();
        }

        // Apply other user effects like healing or stat changes
        if (skill.userHealing > 0)
        {
            Debug.Log($"{attacker.characterName} heals for {skill.userHealing} using {skill.skillName}!");
            attacker.Heal(skill.userHealing); // Assuming Heal adds to health and caps it at max health
        }

        if (skill.speedChange != 0)
        {
            Debug.Log($"{attacker.characterName}'s speed before: {attacker.baseSpeed}");
            attacker.baseSpeed = Mathf.Max(attacker.baseSpeed + skill.speedChange, 0);
            Debug.Log($"{attacker.characterName}'s speed after: {attacker.baseSpeed}");
        }

        if (skill.powerChange != 0)
        {
            Debug.Log($"{attacker.characterName}'s power before: {attacker.basePower}");
            attacker.basePower = Mathf.Max(attacker.basePower + skill.powerChange, 0);
            Debug.Log($"{attacker.characterName}'s power after: {attacker.basePower}");
        }

        if (skill.defenseChange != 0)
        {
            Debug.Log($"{attacker.characterName}'s defense before: {attacker.baseDefense}");
            attacker.baseDefense = Mathf.Max(attacker.baseDefense + skill.defenseChange, 0);
            Debug.Log($"{attacker.characterName}'s defense after: {attacker.baseDefense}");
        }

    }
}