// MockupBattle.cs

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MockupBattle : MonoBehaviour
{
    [Header("Player UI Elements")]
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerHealthText;
    public Image playerHealthBar;
    public Image userImage;
    public Button[] skillButtons;
    public Button trapSkillButton;

    [Header("Enemy UI Elements")]
    public TextMeshProUGUI enemyNameText;
    public TextMeshProUGUI enemyHealthText;
    public Image enemyHealthBar;
    public Image enemyImage;  // This will show the front view image of the enemy

    [Header("Character Stats")]
    public CharacterStats playerStats;   // Player stats will be assigned from GameManager
    public CharacterStats enemyStats;    // Enemy stats will be assigned from GameManager

    private bool isPlayerTurn = true;

    void Start()
    {

    

        // Initialize both player and enemy stats
        InitializePlayerStats();
        InitializeEnemyStats();
        
          playerStats.SaveSnapshot();
          enemyStats.SaveSnapshot();

        PopulateSkillButtons();
        SetPlayerBackViewImage();
        SetEnemyFrontViewImage();  // Set the enemy's front view image

        UpdateHealthUI();

        Debug.Log("Battle Start!");
        Debug.Log($"Player: {playerStats.characterName} vs Enemy: {enemyStats.characterName}");

        StartCoroutine(BattleSequence());
    }

    // Initialize player stats from GameManager
    void InitializePlayerStats()
    {
        if (GameManager.Instance.userCharacterStats != null)
        {
            playerStats = GameManager.Instance.userCharacterStats; // Assign player stats from GameManager
            playerStats.Initialize(); // Reset player stats
            playerNameText.text = playerStats.characterName; // Update the UI
        }
        else
        {
            Debug.LogError("Player Character Stats are not assigned in GameManager.");
        }
    }

    // Initialize enemy stats from GameManager
    void InitializeEnemyStats()
    {
        if (GameManager.Instance.enemyCharacterStats != null)
        {
            enemyStats = GameManager.Instance.enemyCharacterStats; // Assign enemy stats from GameManager
            enemyStats.Initialize(); // Reset enemy stats
            enemyNameText.text = enemyStats.characterName; // Update the UI
        }
        else
        {
            Debug.LogError("Enemy Character Stats are not assigned in GameManager.");
        }
    }

    void PopulateSkillButtons()
    {
        // Ensure we don't go out of bounds of the skillButtons array
        for (int i = 0; i < skillButtons.Length && i < playerStats.defaultSkills.Length; i++)
        {
            Skill skill = playerStats.defaultSkills[i];

            // Set the button's text to the skill's name
            skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = skill.name;

            // Add a listener to each button to use the skill
            int skillIndex = i;  // Capture the index for the listener
            skillButtons[i].onClick.AddListener(() => OnSkillButtonClicked(skillIndex));
        }
    }

    void OnSkillButtonClicked(int skillIndex)
    {
        if (skillIndex >= 0 && skillIndex < playerStats.defaultSkills.Length)
        {
            Skill selectedSkill = playerStats.defaultSkills[skillIndex];
            Debug.Log($"Player used skill: {selectedSkill.name}");
            // Call UseSkill method to apply the skill to the enemy
            UseSkill(playerStats, enemyStats, selectedSkill);
        }
    }

    void SetPlayerBackViewImage()
    {
        // Set the user image to the player's back view image
        if (userImage != null && playerStats != null && playerStats.backViewImage != null)
        {
            userImage.sprite = playerStats.backViewImage;
        }
        else
        {
            Debug.LogWarning("User Image or Player Back View Image is missing!");
        }
    }

     void SetEnemyFrontViewImage()
    {
        // Set the enemy image to the enemy's front view image
        if (enemyImage != null && enemyStats != null && enemyStats.frontViewImage != null)
        {
            enemyImage.sprite = enemyStats.frontViewImage;
        }
        else
        {
            Debug.LogWarning("Enemy Image or Enemy Front View Image is missing!");
        }
    }

    IEnumerator BattleSequence()
    {
        while (playerStats.currentHealth > 0 && enemyStats.currentHealth > 0)
        {
            if (isPlayerTurn)
            {
                Debug.Log("Player's Turn");
                UseSkill(playerStats, enemyStats, playerStats.defaultSkills[0]); // Use the first skill for simplicity
            }
            else
            {
                Debug.Log("Enemy's Turn");
                UseSkill(enemyStats, playerStats, enemyStats.defaultSkills[Random.Range(0, enemyStats.defaultSkills.Length)]);
            }

            isPlayerTurn = !isPlayerTurn; // Alternate turns
            UpdateHealthUI(); // Update the UI after each turn
            yield return new WaitForSeconds(1f); // Pause for readability
        }

    
    }

    void Update()
    {
        // Check if the health is <= 0 for either player or enemy
        if (playerStats.currentHealth <= 0 || enemyStats.currentHealth <= 0)
        {
            // Log who wins
            Debug.Log(playerStats.currentHealth > 0 ? "Player Wins!" : "Enemy Wins!");

            // Start coroutine to change scene after 3 seconds, regardless of who wins
            StartCoroutine(ChangeSceneAfterDelay());
        }
    }

    // Coroutine to wait 3 seconds before loading the new scene
    private IEnumerator ChangeSceneAfterDelay()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3);

         // Restore the character stats to their original state before changing scenes
    playerStats.RestoreSnapshot();
    enemyStats.RestoreSnapshot();

    // Log the restored stats for debugging
    Debug.Log($"Restored Player Health: {playerStats.currentHealth}/{playerStats.baseHealth}");
    Debug.Log($"Restored Enemy Health: {enemyStats.currentHealth}/{enemyStats.baseHealth}");


        // Change to the "Training" scene
        SceneManager.LoadScene("Training");
    }





    void UseSkill(CharacterStats attacker, CharacterStats defender, Skill skill)
    {
        if (skill == null)
        {
            Debug.Log($"{attacker.characterName} has no skill to use!");
            return;
        }


             // Apply power change if the skill has one
   

         // Apply speed change if the skill has one
    if (skill.enemySpeedChange != 0)
    {
        // Log before and after speed values
        Debug.Log($"{defender.characterName}'s speed before: {defender.baseSpeed}");

        // Calculate the minimum allowed speed (50% of base speed)
        float minAllowedSpeed = (float)defender.baseSpeed * 0.5f;

        // Modify the speed but ensure it doesn't go below 50% of the base speed
        float newSpeed = (float)defender.currentSpeed + skill.enemySpeedChange;

        if (newSpeed < minAllowedSpeed)
        {
            // If the speed would go below 50% of base speed, prevent the change
            Debug.Log($"{defender.characterName}'s speed can't go lower than {minAllowedSpeed} (50% of base speed!");
        }
        else
        {
            // Apply the speed change if within limits
            defender.currentSpeed = (int)newSpeed; // Cast back to int after modification
            Debug.Log($"{defender.characterName}'s speed after: {defender.currentSpeed}");
        }
    }

 // Apply defense change if the skill has one
    if (skill.enemyDefenseChange != 0)
    {
        // Log before and after defense values
        Debug.Log($"{defender.characterName}'s defense before: {defender.baseDefense}");

        // Calculate the minimum allowed defense (50% of base defense)
        float minAllowedDefense = (float)defender.baseDefense * 0.5f;

        // Modify the defense, but ensure it doesn't go below 50% of the base defense
        float newDefense = (float)defender.currentDefense + skill.enemyDefenseChange;

        if (newDefense < minAllowedDefense)
        {
            // If the defense would go below 50% of base defense, prevent the change
            Debug.Log($"{defender.characterName}'s defense can't go lower than {minAllowedDefense} (50% of base defense)!");
        }
        else
        {
            // Apply the defense change if within limits
            defender.currentDefense = (int)newDefense; // Cast back to int after modification
            Debug.Log($"{defender.characterName}'s defense after: {defender.currentDefense}");
        }
    }

        // If skill power is 0, skip damage calculation
    if (skill.power == 0)
    {
        Debug.Log($"{attacker.characterName} used {skill.name} but it has no attack power!");
        return;  // No damage dealt, exit early
    }

        // Apply skill logic (basic example: damage based on attack power)
        int damage = attacker.currentPower + skill.power; // Example calculation
        defender.TakeDamage(damage);

        Debug.Log($"{attacker.characterName} used {skill.name}, dealing {damage} damage to {defender.characterName}!");
        Debug.Log($"{defender.characterName}'s Health: {defender.currentHealth}");
    }

    void UpdateHealthUI()
    {
        // Update Player UI
        playerNameText.text = playerStats.characterName;  // Set player name in UI
        playerHealthText.text = $"{playerStats.currentHealth}/{playerStats.baseHealth}";
        playerHealthBar.fillAmount = (float)playerStats.currentHealth / playerStats.baseHealth;

        // Update Enemy UI
        enemyHealthText.text = $"{enemyStats.currentHealth}/{enemyStats.baseHealth}";
        enemyHealthBar.fillAmount = (float)enemyStats.currentHealth / enemyStats.baseHealth;
    }

}
