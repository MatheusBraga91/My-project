using UnityEngine;
using UnityEngine.EventSystems; // For EventTrigger functionality
using TMPro; // For TextMeshPro

public class TrapSkillHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TrapSkill trapSkill; // The trap skill associated with this button
    private TMP_Text descriptionText; // Reference to the description text element

    /// <summary>
    /// Sets the trap skill and description text for the hover handler.
    /// </summary>
    /// <param name="trapSkill">The TrapSkill object to associate.</param>
    /// <param name="descriptionText">The text container for displaying descriptions.</param>
    public void SetTrapSkill(TrapSkill trapSkill, TMP_Text descriptionText)
    {
        this.trapSkill = trapSkill;
        this.descriptionText = descriptionText;
    }

    /// <summary>
    /// Called when the mouse enters the button.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (trapSkill != null && descriptionText != null)
        {
            descriptionText.text = trapSkill.description; // Show the trap skill description
        }
    }

    /// <summary>
    /// Called when the mouse exits the button.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (descriptionText != null)
        {
            descriptionText.text = ""; // Clear the description text
        }
    }
}
