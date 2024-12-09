using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUpdater : MonoBehaviour
{
    [Header("Player UI")]
    public Image playerHealthBar;
    public TextMeshProUGUI playerHealthText;

    [Header("Enemy UI")]
    public Image enemyHealthBar;
    public TextMeshProUGUI enemyHealthText;

    private Coroutine playerHealthCoroutine;
    private Coroutine enemyHealthCoroutine;

    public void UpdatePlayerHealthUI(int currentHealth, int baseHealth)
    {
        if (playerHealthCoroutine != null)
            StopCoroutine(playerHealthCoroutine);

        playerHealthCoroutine = StartCoroutine(UpdateHealthBarAndText(playerHealthBar, playerHealthText, currentHealth, baseHealth));
    }

    public void UpdateEnemyHealthUI(int currentHealth, int baseHealth)
    {
        if (enemyHealthCoroutine != null)
            StopCoroutine(enemyHealthCoroutine);

        enemyHealthCoroutine = StartCoroutine(UpdateHealthBarAndText(enemyHealthBar, enemyHealthText, currentHealth, baseHealth));
    }

    private IEnumerator UpdateHealthBarAndText(Image healthBar, TextMeshProUGUI healthText, int targetHealth, int baseHealth)
    {
        float startingFill = healthBar.fillAmount;
        float targetFill = (float)targetHealth / baseHealth;

        int startingHealth = Mathf.RoundToInt(startingFill * baseHealth);
        float duration = 0.5f; // Animation duration
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float lerpFactor = elapsedTime / duration;

            healthBar.fillAmount = Mathf.Lerp(startingFill, targetFill, lerpFactor);
            int currentDisplayedHealth = Mathf.RoundToInt(Mathf.Lerp(startingHealth, targetHealth, lerpFactor));
            healthText.text = $"{currentDisplayedHealth}/{baseHealth}";

            yield return null;
        }

        // Ensure final values are set precisely
        healthBar.fillAmount = targetFill;
        healthText.text = $"{targetHealth}/{baseHealth}";
    }
}
