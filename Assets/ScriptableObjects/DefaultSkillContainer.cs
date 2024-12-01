using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DefaultSkillContainer : MonoBehaviour
{
    [Header("Heavy Stats Asset")]
    public CharacterStats heavyStats; // The HeavyStats asset containing the character's skills

    [Header("Skill Buttons")]
    public Button[] skillButtons; // Drag the 4 skill buttons here in the inspector

    [Header("Passive Skill Button (Display Only)")]
    public Button passiveSkillButton; // 

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
        if (heavyStats == null || skillButtons.Length == 0) return;

        for (int i = 0; i < heavyStats.defaultSkills.Length; i++)
        {
            if (i < skillButtons.Length && heavyStats.defaultSkills[i] != null)
            {
                TextMeshProUGUI buttonText = skillButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = heavyStats.defaultSkills[i].skillName;
                }

                int buttonIndex = i; // Capture the current index
                skillButtons[i].onClick.AddListener(() => OnDefaultSkillButtonClick(buttonIndex));
            }
        }

        if (passiveSkillButton != null && heavyStats.passiveSkill != null)
        {
            TextMeshProUGUI passiveButtonText = passiveSkillButton.GetComponentInChildren<TextMeshProUGUI>();
            if (passiveButtonText != null)
            {
                passiveButtonText.text = heavyStats.passiveSkill.skillName;
            }
        }
    }

    private void DisplayBaseStats()
    {
        if (heavyStats == null) return;

        if (healthText != null)
            healthText.text = $"{heavyStats.baseHealth}";

        if (powerText != null)
            powerText.text = $"{heavyStats.basePower}";

        if (speedText != null)
            speedText.text = $"{heavyStats.baseSpeed}";

        if (defenseText != null)
            defenseText.text = $"{heavyStats.baseDefense}";
    }

    private void OnDefaultSkillButtonClick(int buttonIndex)
    {
        Debug.Log($"Default skill button {buttonIndex} clicked.");
        selectedButtonIndex = buttonIndex;
    }

    public void ReplaceSkillInButton(Skill newSkill)
    {
        if (selectedButtonIndex == -1)
        {
            Debug.LogWarning("No skill button is selected in the DefaultSkillContainer!");
            return;
        }

        heavyStats.defaultSkills[selectedButtonIndex] = newSkill;

        TextMeshProUGUI buttonText = skillButtons[selectedButtonIndex].GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
            buttonText.text = newSkill.skillName;

        Debug.Log($"Skill replaced on button {selectedButtonIndex} with {newSkill.skillName}.");
    }


public void ReplacePassiveSkill(PassiveSkill newPassiveSkill)
{
    if (newPassiveSkill == null)
    {
        Debug.LogWarning("No passive skill selected!");
        return;
    }

    // Replace the passive skill in HeavyStats
    heavyStats.passiveSkill = newPassiveSkill;

    // Update the button's display with the new passive skill name
    if (passiveSkillButton != null)
    {
        TextMeshProUGUI passiveButtonText = passiveSkillButton.GetComponentInChildren<TextMeshProUGUI>();
        if (passiveButtonText != null)
        {
            passiveButtonText.text = newPassiveSkill.skillName;
        }
    }

    Debug.Log($"Passive skill replaced with {newPassiveSkill.skillName}.");
}



    public void ReplacePassiveSkillButton(PassiveSkill newPassiveSkill)
    {
        if (heavyStats == null)
        {
            Debug.LogWarning("HeavyStats is not assigned!");
            return;
        }

        heavyStats.passiveSkill = newPassiveSkill;

        if (passiveSkillButton != null)
        {
            TextMeshProUGUI buttonText = passiveSkillButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
                buttonText.text = newPassiveSkill.skillName;

            Debug.Log($"Passive skill replaced with {newPassiveSkill.skillName}.");
        }
    }
}
