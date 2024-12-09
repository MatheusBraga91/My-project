using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UserPanelController : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text characterNameText; // Character name
    public TMP_Text healthText; // Health value
    public TMP_Text powerText; // Power value
    public TMP_Text speedText; // Speed value
    public TMP_Text defenseText; // Defense value

    [Header("Skill Buttons")]
    public Button passiveSkillButton; // Button for passive skill
    public Button[] defaultSkillButtons = new Button[4]; // Buttons for default skills
    public Button[] trapSkillButtons = new Button[5]; // Buttons for trap skills

    [Header("Assigned Character")]
    public CharacterStats characterStats; // The assigned character stats (for single character)

    [Header("Character Navigation")]
    public CharacterStats[] characterStatsList; // Array of multiple character stats for navigation
    public Button leftArrowButton; // Button to navigate to the previous character
    public Button rightArrowButton; // Button to navigate to the next character
    public int currentCharacterIndex = 0; // Index of the currently displayed character

    [Header("Character Front Image")]
    public Image frontViewImage; // Reference to the Image component for the character's front view image

    private void Start()
    {
        if (characterStatsList.Length > 0)
        {
            // Use first character in the list if multiple characters are provided
            characterStats = characterStatsList[currentCharacterIndex];
        }

        if (characterStats == null)
        {
            Debug.LogError("CharacterStats not assigned to UserPanelController!");
            return;
        }

        // Initialize UI with the assigned or first character
        UpdateUI(characterStats);

        // Set up navigation buttons if using multiple characters
        if (characterStatsList.Length > 1)
        {
            leftArrowButton.onClick.AddListener(() => NavigateCharacter(-1)); // Left button: navigate backwards
            rightArrowButton.onClick.AddListener(() => NavigateCharacter(1)); // Right button: navigate forwards
        }
        else
        {
            // Hide navigation buttons if only one character is provided
            leftArrowButton.gameObject.SetActive(false);
            rightArrowButton.gameObject.SetActive(false);
        }
    }

    private void UpdateUI(CharacterStats characterStats)
    {
        // Populate character stats
        characterNameText.text = characterStats.characterName;
        healthText.text = $"{characterStats.baseHealth}";
        powerText.text = $"{characterStats.basePower}";
        speedText.text = $"{characterStats.baseSpeed}";
        defenseText.text = $"{characterStats.baseDefense}";


             // Set front view image
        if (frontViewImage != null && characterStats.frontViewImage != null)
        {
            frontViewImage.sprite = characterStats.frontViewImage;  // Set the front image of the character
        }
        else
        {
            Debug.LogError("Front View Image or character's frontViewImage is missing!");
        }

        // Set up passive skill button
        if (characterStats.passiveSkill != null)
        {
            passiveSkillButton.GetComponentInChildren<TMP_Text>().text = characterStats.passiveSkill.skillName;
            passiveSkillButton.onClick.RemoveAllListeners();
            passiveSkillButton.onClick.AddListener(() =>
            {
                Debug.Log($"Passive Skill: {characterStats.passiveSkill.skillName}");
            });
        }

        // Set up default skill buttons
        for (int i = 0; i < defaultSkillButtons.Length; i++)
        {
            if (characterStats.defaultSkills[i] != null)
            {
                defaultSkillButtons[i].GetComponentInChildren<TMP_Text>().text = characterStats.defaultSkills[i].skillName;
                int skillIndex = i; // Capture index for lambda
                defaultSkillButtons[i].onClick.RemoveAllListeners();
                defaultSkillButtons[i].onClick.AddListener(() =>
                {
                    Debug.Log($"Default Skill: {characterStats.defaultSkills[skillIndex].skillName}");
                });
                defaultSkillButtons[i].gameObject.SetActive(true);
            }
            else
            {
                defaultSkillButtons[i].gameObject.SetActive(false); // Hide unused button
            }
        }

        // Set up trap skill buttons dynamically
        for (int i = 0; i < trapSkillButtons.Length; i++)
        {
            if (characterStats.trapSkills[i] != null)
            {
                trapSkillButtons[i].GetComponentInChildren<TMP_Text>().text = characterStats.trapSkills[i].skillName;
                int skillIndex = i; // Capture index for lambda
                trapSkillButtons[i].onClick.RemoveAllListeners();
                trapSkillButtons[i].onClick.AddListener(() =>
                {
                    Debug.Log($"Trap Skill: {characterStats.trapSkills[skillIndex].skillName}");
                });
                SetButtonTransparency(trapSkillButtons[i], 1f); // Fully visible
                trapSkillButtons[i].gameObject.SetActive(true);
            }
            else
            {
                trapSkillButtons[i].GetComponentInChildren<TMP_Text>().text = ""; // No text
                SetButtonTransparency(trapSkillButtons[i], 0f); // Fully transparent
                trapSkillButtons[i].gameObject.SetActive(false);
            }
        }
    }

   private void NavigateCharacter(int direction)
{
    // Update the index cyclically
    currentCharacterIndex = (currentCharacterIndex + direction + characterStatsList.Length) % characterStatsList.Length;

    // Update the characterStats with the new character
    characterStats = characterStatsList[currentCharacterIndex];

    // Synchronize the Assigned Character with the current selection
    UpdateAssignedCharacter();

    // Update the UI with the new character stats
    UpdateUI(characterStats);

      // Update GameManager with the selected character's stats
    if (GameManager.Instance != null)
    {
        GameManager.Instance.UpdateUserCharacterStats(); // Sync selected character with GameManager
    }
}

private void UpdateAssignedCharacter()
{
    // Synchronize the assigned character with the current character
    characterStats = characterStatsList[currentCharacterIndex];
}


    // Method to adjust button transparency
    private void SetButtonTransparency(Button button, float alpha)
    {
        Image buttonImage = button.GetComponent<Image>();
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

        if (buttonImage != null)
        {
            Color imageColor = buttonImage.color;
            imageColor.a = alpha;
            buttonImage.color = imageColor;
        }

        if (buttonText != null)
        {
            Color textColor = buttonText.color;
            textColor.a = alpha;
            buttonText.color = textColor;
        }
    }
}
