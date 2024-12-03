using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI enemyNameText;  // Reference to the TextMeshPro object in your UI
    public Image enemyImage;  // Reference to the Image object for the front image view
    public TextMeshProUGUI healthText;  // Reference to the health text UI
    public Image healthBarImage; // Reference to the health bar image UI (fillable health bar)


    private CharacterStats defender; // Variable to store the current defender (AI character)

    void Start()
    {
        // Set the defender to the AI character from the BattleManager
        if (BattleManager.aiCharacterClone != null)
        {
            defender = BattleManager.aiCharacterClone;

            // Set the enemy's name in the TextMeshPro object
            enemyNameText.text = defender.characterName;

            // Set the enemy's front image in the Image object
            if (enemyImage != null)
            {
                enemyImage.sprite = defender.frontViewImage;
            }
            else
            {
                Debug.LogError("Enemy Image component is not assigned!");
            }

            // Initialize the health text and bar
            UpdateEnemyHealthUI(defender.currentHealth, defender.baseHealth);
        }
        else
        {
            Debug.LogError("AI character clone is not found!");
        }
    }

    // This function will update both health text and health bar
    // This function will update the enemy's UI whenever health changes
    public void UpdateEnemyHealthUI(int currentHealth, int baseHealth)
    {
        if (enemyNameText != null)
        {
            enemyNameText.text = defender.characterName;
        }

        // Update health text (current health / base health)
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{baseHealth}";
        }

        // Update health bar (use current health percentage)
        if (healthBarImage != null)
        {
            float healthPercentage = (float)currentHealth / baseHealth;
            healthBarImage.fillAmount = healthPercentage;
        }
    }
}
