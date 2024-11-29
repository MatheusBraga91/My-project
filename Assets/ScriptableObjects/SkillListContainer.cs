using UnityEngine;
using TMPro;  // For TextMeshPro
using UnityEngine.UI;  // For Button

public class SkillListContainer : MonoBehaviour
{
    [Header("Skill Buttons")]
    public Button[] skillButtons;  // Array of buttons in the Skill List Panel

    [Header("Skill Assets")]
    public Skill[] skills;  // Array of Skill assets to be assigned to buttons

    [Header("Skill Description UI")]
    public TMP_Text descriptionText; // Reference to the description container's text element
    public Image categoryIcon; // Reference to the category icon Image in the description container (for Attack/Special)

    public Sprite attackIcon; // Sprite for Attack category
    public Sprite specialIcon; // Sprite for Special category

    private DefaultSkillContainer defaultSkillContainer; // Reference to Default Skill Container

    void Start()
    {
        // Find the DefaultSkillContainer in the scene (or assign it via Inspector)
        defaultSkillContainer = FindObjectOfType<DefaultSkillContainer>();

        // Initialize the Skill List container and assign skills to the buttons
        InitializeSkillButtons();
    }

    void InitializeSkillButtons()
    {
        // Ensure there are the same number of buttons and skills
        if (skillButtons.Length != skills.Length)
        {
            Debug.LogError("Number of buttons and skills do not match!");
            return;
        }

        // Loop through each button and assign the corresponding skill
        for (int i = 0; i < skillButtons.Length; i++)
        {
            SetButtonText(skillButtons[i], skills[i]);
            int index = i;  // Capture the correct index in the closure

            // Add click listener for skill button functionality
            skillButtons[i].onClick.AddListener(() => OnSkillButtonClick(index));

            // Add the hover handler component and assign the skill
            SkillButtonHoverHandler hoverHandler = skillButtons[i].gameObject.AddComponent<SkillButtonHoverHandler>();
            hoverHandler.skill = skills[index]; // Assign the correct skill to the hover handler
        }
    }

    // Set the button text based on the skill name
    void SetButtonText(Button button, Skill skill)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText != null && skill != null)
        {
            buttonText.text = skill.skillName;  // Set the text of the button to the skill's name
        }
    }

    // Called when a skill button is clicked
    void OnSkillButtonClick(int index)
    {
        // Get the selected skill
        Skill selectedSkill = skills[index];

        // Replace the skill in the Default Skill Containe
        if (defaultSkillContainer != null)
        {
            defaultSkillContainer.ReplaceSkillInButton(selectedSkill);
        }
    }

    // Show skill description along with the category icon
    public void ShowSkillDescription(Skill skill)
    {
        if (descriptionText != null && skill != null)
        {
            descriptionText.text = skill.description; // Update the description text

            // Update the category icon based on the skill's category
            if (skill.skillCategory == SkillCategory.Attack)
            {
                categoryIcon.sprite = attackIcon;
                categoryIcon.enabled = true; // Ensure the icon is visible
            }
            else if (skill.skillCategory == SkillCategory.Special)
            {
                categoryIcon.sprite = specialIcon;
                categoryIcon.enabled = true; // Ensure the icon is visible
            }
            else
            {
                categoryIcon.enabled = false; // Hide the icon if the category doesn't match
            }
        }
    }

    // Clear the skill description and hide the category icon
    public void ClearSkillDescription()
    {
        if (descriptionText != null)
        {
            descriptionText.text = ""; // Clear the description text
        }

        if (categoryIcon != null)
        {
            categoryIcon.enabled = false; // Hide the category icon
        }
    }
}
