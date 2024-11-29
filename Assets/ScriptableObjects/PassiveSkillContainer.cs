using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.UI; // For Button

public class PassiveSkillContainer : MonoBehaviour
{
    [Header("Passive Skill Buttons")]
    public Button[] passiveSkillButtons; // Array of buttons for passive skills

    [Header("Passive Skill Assets")]
    public PassiveSkill[] passiveSkills; // Array of PassiveSkill assets to be assigned to buttons

    [Header("Skill Description UI")]
    public TMP_Text descriptionText; // Reference to the description container's text element

    void Start()
    {
        // Initialize the Passive Skill buttons and assign the passive skills
        InitializePassiveSkillButtons();
    }

    void InitializePassiveSkillButtons()
    {
        // Ensure there are enough buttons and passive skills
        if (passiveSkillButtons.Length != passiveSkills.Length)
        {
            Debug.LogError("Number of passive skill buttons and passive skills do not match!");
            return;
        }

        // Loop through each button and assign the corresponding passive skill
        for (int i = 0; i < passiveSkillButtons.Length; i++)
        {
            SetButtonText(passiveSkillButtons[i], passiveSkills[i]);

            // Add hover functionality for showing descriptions
            AddHoverHandler(passiveSkillButtons[i], passiveSkills[i]);
        }
    }

    // Set the button text based on the passive skill name
    void SetButtonText(Button button, PassiveSkill passiveSkill)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText != null && passiveSkill != null)
        {
            buttonText.text = passiveSkill.skillName; // Set the button's text to the passive skill's name
        }
    }

    // Add the hover handler for passive skill buttons
    void AddHoverHandler(Button button, PassiveSkill passiveSkill)
    {
        // Add the SkillButtonHoverHandler component to the button
        SkillButtonHoverHandler hoverHandler = button.gameObject.AddComponent<SkillButtonHoverHandler>();

        // Set the passive skill and description text
        hoverHandler.SetPassiveSkill(passiveSkill, descriptionText);
    }

    // Show the description for the specified passive skill
    public void ShowPassiveSkillDescription(PassiveSkill passiveSkill)
    {
        if (descriptionText != null && passiveSkill != null)
        {
            descriptionText.text = passiveSkill.skillDescription; // Update the description text
        }
    }

    // Clear the passive skill description
    public void ClearPassiveSkillDescription()
    {
        if (descriptionText != null)
        {
            descriptionText.text = ""; // Clear the description text
        }
    }
}
