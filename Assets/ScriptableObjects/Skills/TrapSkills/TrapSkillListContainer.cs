using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.UI; // For Button

public class TrapSkillListContainer : MonoBehaviour
{
    [Header("Trap Skill Buttons")]
    public Button[] trapSkillButtons; // Array of buttons for trap skills

    [Header("Trap Skill Assets")]
    public TrapSkill[] trapSkills; // Array of TrapSkill assets to be assigned to buttons

    [Header("Skill Description UI")]
    public TMP_Text descriptionText; // Reference to the description container's text element

    [Header("Trap Skill Container Reference")]
    public TrapSkillContainer trapSkillContainer; // Reference to the TrapSkillContainer

    void Start()
    {
        // Initialize the Trap Skill buttons and assign the trap skills
        InitializeTrapSkillButtons();
    }

    void InitializeTrapSkillButtons()
    {
        // Ensure there are enough buttons and trap skills
        if (trapSkillButtons.Length != trapSkills.Length)
        {
            Debug.LogError("Number of trap skill buttons and trap skills do not match!");
            return;
        }

        // Loop through each button and assign the corresponding trap skill
        for (int i = 0; i < trapSkillButtons.Length; i++)
        {
            SetButtonText(trapSkillButtons[i], trapSkills[i]);

            // Add hover functionality for showing descriptions
            AddHoverHandler(trapSkillButtons[i], trapSkills[i]);

            // Add click functionality to add the TrapSkill to the TrapSkillContainer
            int index = i; // Capture the correct index for the click listener
            trapSkillButtons[i].onClick.AddListener(() => OnTrapSkillButtonClick(index));
        }
    }

    // Set the button text based on the trap skill name
    void SetButtonText(Button button, TrapSkill trapSkill)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText != null && trapSkill != null)
        {
            buttonText.text = trapSkill.skillName; // Set the button's text to the trap skill's name
        }
    }

    // Add the hover handler for trap skill buttons
    void AddHoverHandler(Button button, TrapSkill trapSkill)
    {
        // Add the TrapSkillHoverHandler component to the button
        TrapSkillHoverHandler hoverHandler = button.gameObject.AddComponent<TrapSkillHoverHandler>();

        // Set the trap skill and description text
        hoverHandler.SetTrapSkill(trapSkill, descriptionText);
    }

    // Called when a trap skill button is clicked
    void OnTrapSkillButtonClick(int index)
    {
        // Ensure the TrapSkillContainer reference is set
        if (trapSkillContainer == null)
        {
            Debug.LogError("TrapSkillContainer reference is not set in TrapSkillListContainer!");
            return;
        }

        // Get the selected trap skill
        TrapSkill selectedTrapSkill = trapSkills[index];

        // Attempt to add the trap skill to the TrapSkillContainer
        trapSkillContainer.AddTrapSkill(selectedTrapSkill);
    }
}