using System.Collections;
using UnityEngine;

public class MockupBattle : MonoBehaviour
{
    public CharacterStats playerStats;
    public CharacterStats enemyStats;

    private bool isPlayerTurn = true;

    void Start()
    {
        // Initialize both player and enemy
        playerStats.Initialize();
        enemyStats.Initialize();

        Debug.Log("Battle Start!");
        Debug.Log($"Player: {playerStats.characterName} vs Enemy: {enemyStats.characterName}");

        StartCoroutine(BattleSequence());
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
            yield return new WaitForSeconds(1f); // Pause for readability
        }

        Debug.Log(playerStats.currentHealth > 0 ? "Player Wins!" : "Enemy Wins!");
    }

    void UseSkill(CharacterStats attacker, CharacterStats defender, Skill skill)
    {
        if (skill == null)
        {
            Debug.Log($"{attacker.characterName} has no skill to use!");
            return;
        }

        // Apply skill logic (basic example: damage based on attack power)
        int damage = attacker.currentPower + skill.power; // Example calculation
        defender.TakeDamage(damage);

        Debug.Log($"{attacker.characterName} used {skill.name}, dealing {damage} damage to {defender.characterName}!");
        Debug.Log($"{defender.characterName}'s Health: {defender.currentHealth}");
    }
}
