using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class EnemyUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI enemyNameText;
    public Image enemyImage;
    public TextMeshProUGUI healthText;
    public Image healthBarImage;

    private CharacterStats defender;

    private void Start()
    {
        StartCoroutine(InitializeUI());
    }

    public IEnumerator InitializeUI()
    {
        yield return new WaitUntil(() => BattleManager.Instance.aiCharacter != null);

        defender = BattleManager.Instance.aiCharacter;

        if (defender != null)
        {
            enemyNameText.text = defender.characterName;
            enemyImage.sprite = defender.frontViewImage;
            UpdateEnemyHealthUI();
        }
        else
        {
            Debug.LogError("Enemy character not found during UI initialization.");
        }
    }

    public void UpdateEnemyHealthUI()
    {
        if (defender == null)
        {
            Debug.LogWarning("Defender not set for UI Update.");
            return;
        }

        healthText.text = $"{defender.currentHealth}/{defender.baseHealth}";
        healthBarImage.fillAmount = (float)defender.currentHealth / defender.baseHealth;


    }

    private void UpdateMultipleHealthBars(CharacterStats character)
    {
        // Logic to handle layered health bars (if applicable)
        // For example, updating UI for multiple health layers
        Debug.Log("Updating multiple health bars not yet implemented.");
    }
}
