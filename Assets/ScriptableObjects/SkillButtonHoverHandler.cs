using UnityEngine;
using UnityEngine.EventSystems; // For EventTrigger functionality
using TMPro; // For TextMeshPro
using UnityEngine.UI; // For accessing Image components

public class SkillButtonHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Skill skill; // The skill associated with this button
    public PassiveSkill passiveSkill; // The passive skill associated with this button
    private SkillListContainer skillListContainer; // Reference to the SkillListContainer for description updates
    private PassiveSkillContainer passiveSkillContainer; // Reference to the PassiveSkillContainer for description updates
    private TMP_Text descriptionText; // Reference to the description text element
    private GameObject categoryIcon; // Reference to the category icon (attack or special)

    [Header("Category Icons")]
    public Sprite attackIcon; // Icon for attack skills
    public Sprite specialIcon; // Icon for special skills

    void Start()
    {
        // Find the appropriate container (SkillListContainer or PassiveSkillContainer) in the scene
        skillListContainer = FindObjectOfType<SkillListContainer>();
        passiveSkillContainer = FindObjectOfType<PassiveSkillContainer>();

        // Find the category icon in this button (assumed to be part of the button)
        categoryIcon = transform.Find("CategoryIcon")?.gameObject; // Assuming CategoryIcon is a child of the button

        if (categoryIcon == null)
        {
            Debug.LogWarning("CategoryIcon not found in the button.");
        }
    }

    // Called when the mouse enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (skillListContainer != null && skill != null)
        {
            // Show both description and category icon for regular skills
            skillListContainer.ShowSkillDescription(skill);
            ShowCategoryIcon(skill.skillCategory); // Corrected: Use skillCategory from Skill class
        }
        else if (passiveSkillContainer != null && passiveSkill != null)
        {
            // Show the passive skill description
            passiveSkillContainer.ShowPassiveSkillDescription(passiveSkill);
            HideCategoryIcon(); // Hide category icon for passive skills
        }
    }

    // Called when the mouse exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        if (skillListContainer != null)
        {
            // Clear both description and category icon for regular skills
            skillListContainer.ClearSkillDescription();
            HideCategoryIcon(); // Hide category icon when the pointer exits
        }
        else if (passiveSkillContainer != null)
        {
            // Clear the passive skill description
            passiveSkillContainer.ClearPassiveSkillDescription();
        }
    }

    // Method to set a passive skill and its description container
    public void SetPassiveSkill(PassiveSkill passiveSkill, TMP_Text descriptionText)
    {
        this.passiveSkill = passiveSkill;
        this.descriptionText = descriptionText; // Store the reference to the description text element
    }

// Method to show the category icon based on the skill's category
private void ShowCategoryIcon(SkillCategory category)
{
    if (categoryIcon != null)
    {
        // Call ShowSkillDescription on SkillListContainer instead of directly handling icon here
        if (skillListContainer != null && skill != null)
        {
            // This will now update the description and category icon as intended
            skillListContainer.ShowSkillDescription(skill);
        }
    }
    else
    {
        Debug.LogWarning("CategoryIcon is not assigned or missing in the button.");
    }
}


    // Hide the category icon (for passive skills)
    private void HideCategoryIcon()
    {
        if (categoryIcon != null)
        {
            categoryIcon.SetActive(false); // Disable the category icon for passive skills
        }
    }
}
