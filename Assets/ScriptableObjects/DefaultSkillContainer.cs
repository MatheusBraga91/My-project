using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class DefaultSkillContainer : MonoBehaviour
{
    [Header("Heavy Stats Asset")]
    public CharacterStats heavyStats; // The HeavyStats asset containing the character's skills

    [Header("Skill Buttons")]
    public Button[] skillButtons; // Drag the 4 skill buttons here in the inspector

    [Header("Passive Skill Button (Display Only)")]
    public Button passiveSkillButton; // Drag the passive skill button here in the inspector

    [Header("Base Stats Texts")]
    public TextMeshProUGUI healthText; // Text for baseHealth
    public TextMeshProUGUI powerText;  // Text for basePower
    public TextMeshProUGUI speedText;  // Text for baseSpeed
    public TextMeshProUGUI defenseText; // Text for baseDefense

    private int selectedButtonIndex = -1; // To track which button in the DefaultSkillContainer is selected

    void Start()
    {
        PopulateSkills(); // Initialize the default skills on the buttons
        DisplayBaseStats(); // Display the base stats in the TextMeshPro objects
    }

    private void PopulateSkills()
    {
        // Ensure we have both the HeavyStats asset and buttons
        if (heavyStats == null || skillButtons.Length == 0) return;

        // Populate the default skills onto the skill buttons
        for (int i = 0; i < heavyStats.defaultSkills.Length; i++)
        {
            if (i < skillButtons.Length && heavyStats.defaultSkills[i] != null)
            {
                // Set button text to the skill name
                TextMeshProUGUI buttonText = skillButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = heavyStats.defaultSkills[i].skillName;
                }

                // Attach an onClick listener to track which button is selected
                int buttonIndex = i; // Capture the current index
                skillButtons[i].onClick.AddListener(() => OnDefaultSkillButtonClick(buttonIndex));
            }
        }

        // Display the passive skill on the passive skill button, if assigned
        if (passiveSkillButton != null && heavyStats.passiveSkill != null)
        {
            TextMeshProUGUI passiveButtonText = passiveSkillButton.GetComponentInChildren<TextMeshProUGUI>();
            if (passiveButtonText != null)
            {
                passiveButtonText.text = heavyStats.passiveSkill.skillName;
            }
        }
    }

    // Display the base stats from HeavyStats in the TextMeshPro objects
    private void DisplayBaseStats()
    {
        if (heavyStats == null) return;

        if (healthText != null)
        {
            healthText.text = $"{heavyStats.baseHealth}";
        }

        if (powerText != null)
        {
            powerText.text = $"{heavyStats.basePower}";
        }

        if (speedText != null)
        {
            speedText.text = $"{heavyStats.baseSpeed}";
        }

        if (defenseText != null)
        {
            defenseText.text = $"{heavyStats.baseDefense}";
        }
    }

    // Called when a button in the DefaultSkillContainer is clicked
    private void OnDefaultSkillButtonClick(int buttonIndex)
    {
        Debug.Log($"Default skill button {buttonIndex} clicked.");

        // Track the selected button index
        selectedButtonIndex = buttonIndex;

        // Add any visual feedback or functionality for the selected button if needed
    }

    // Public method to replace the skill on the selected button with a new skill
    public void ReplaceSkillInButton(Skill newSkill)
    {
        if (selectedButtonIndex == -1)
        {
            Debug.LogWarning("No skill button is selected in the DefaultSkillContainer!");
            return;
        }

        // Replace the skill in HeavyStats with the new skill
        heavyStats.defaultSkills[selectedButtonIndex] = newSkill;

        // Update the button's display with the new skill name
        TextMeshProUGUI buttonText = skillButtons[selectedButtonIndex].GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = newSkill.skillName;
        }

        Debug.Log($"Skill replaced on button {selectedButtonIndex} with {newSkill.skillName}.");
    }
}
