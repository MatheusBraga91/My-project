using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    public CharacterStats userCharacterStats; // To store the enemy character's stats
    public CharacterStats enemyCharacterStats; // To store the enemy character's stats
    public UserPanelController userPanelController; // Reference to AIPanelController
    public AIPanelController aiPanelController; // Reference to AIPanelController

    private void Awake()
    {
        // Singleton pattern: Make sure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy if another instance exists
        }
    }

    public void UpdateEnemyCharacterStats()
    {
        if (aiPanelController != null)
        {
            enemyCharacterStats = aiPanelController.characterStatsList[aiPanelController.currentCharacterIndex]; // Get character based on the current index
        }
        else
        {
            Debug.LogError("AIPanelController reference not set in GameManager!");
        }
    }

        public void UpdateUserCharacterStats()
    {
        if (aiPanelController != null)
        {
            userCharacterStats = userPanelController.characterStatsList[userPanelController.currentCharacterIndex]; // Get character based on the current index
        }
        else
        {
            Debug.LogError("UserPanelController reference not set in GameManager!");
        }
    }


    
}


